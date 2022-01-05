using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using PublicApi.DTO.v1;
using TestProject.Helpers;
using WebApp;
using Xunit;
using Xunit.Abstractions;
using Orders = PublicApi.DTO.v1.Orders;

namespace TestProject.IntegrationTests
{
    public class GameInfoApiControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;


        public GameInfoApiControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory,
            ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("test_database_name", Guid.NewGuid().ToString());
                })
                .CreateClient(new WebApplicationFactoryClientOptions()
                    {
                        // dont follow redirects
                        AllowAutoRedirect = false
                    }
                );
        }

        [Fact]
        public async Task Api_Test_Main_Flow()
        {
            // REGISTER AN ACCOUNT
            var uri = "/api/v1/Account/Register";

            var userToBeRegistered = new Register()
            {
                Email = "user@mail.com",
                Firstname = "User",
                Lastname = "U",
                Password = "password1"
            };

            var serializedUserToBeRegistered = JsonSerializer.Serialize(userToBeRegistered);
            var httpContent = new StringContent(serializedUserToBeRegistered, Encoding.UTF8, "application/json");
            
            var getTestResponse = await _client.PostAsync(uri, httpContent);
            getTestResponse.EnsureSuccessStatusCode();
            
            var body = await getTestResponse.Content.ReadAsStringAsync();
            var user = JsonHelper.DeserializeWithWebDefaults<JwtResponse>(body);
            
            User_Registered_Correctly(userToBeRegistered, user);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user!.Token);
            
            // GET ALL GAMES
            uri = "/api/v1/GameInfos";
            getTestResponse = await _client.GetAsync(uri);
            
            getTestResponse.EnsureSuccessStatusCode();
            body = await getTestResponse.Content.ReadAsStringAsync();

            var games = JsonHelper.DeserializeWithWebDefaults<List<PublicApi.DTO.v1.GameInfo>>(body);
            
            Get_GameInfos_Not_Empty(games!);

            // CHOOSE A GAME
            var chosenGame = games!.First();
            uri = "/api/v1/GameInfos/" + chosenGame.Id;
            
            getTestResponse = await _client.GetAsync(uri);
            getTestResponse.EnsureSuccessStatusCode();
            
            body = await getTestResponse.Content.ReadAsStringAsync();
            
            var game = JsonHelper.DeserializeWithWebDefaults<PublicApi.DTO.v1.GameInfo>(body);
            
            Get_GameInfo_Returns_Correct_Entity(chosenGame, game);
            
            // CREATE AN ORDER
            // Choose the delivery location
            uri = "/api/v1/Addresses";
            getTestResponse = await _client.GetAsync(uri);
            
            getTestResponse.EnsureSuccessStatusCode();
            body = await getTestResponse.Content.ReadAsStringAsync();

            var addresses = JsonHelper.DeserializeWithWebDefaults<List<PublicApi.DTO.v1.Address>>(body);
            
            Assert.NotNull(addresses);
            Assert.NotEmpty(addresses!);
            
            var addressId = addresses!.First().Id;
            
            // Choose the payment method
            uri = "/api/v1/PaymentMethods";
            
            getTestResponse = await _client.GetAsync(uri);
            getTestResponse.EnsureSuccessStatusCode();
            
            body = await getTestResponse.Content.ReadAsStringAsync();

            var paymentMethods = JsonHelper.DeserializeWithWebDefaults<List<PublicApi.DTO.v1.PaymentMethod>>(body);
            
            Assert.NotNull(paymentMethods);
            Assert.NotEmpty(paymentMethods!);
            
            var paymentMethodId = paymentMethods!.First().Id;
            
            // Place an order
            uri = "/api/v1/Orders";

            var orderToBePlaced = new OrdersAdd()
            {
                PaymentMethodId = paymentMethodId,
                AddressId = addressId,
                GameIds = new List<Guid>()
                {
                    game!.Id
                }
            };

            var serializedOrderToBePlaced = JsonSerializer.Serialize(orderToBePlaced);
            httpContent = new StringContent(serializedOrderToBePlaced, Encoding.UTF8, "application/json");
            
            getTestResponse = await _client.PostAsync(uri, httpContent);
            getTestResponse.EnsureSuccessStatusCode();
            
            body = await getTestResponse.Content.ReadAsStringAsync();

            var returnedOrderId = JsonHelper.DeserializeWithWebDefaults<PublicApi.DTO.v1.Orders>(body)!.Id;
            
            Game_Availability_Changes();
            
            // VIEW ORDER
            uri = "/api/v1/Orders/" + returnedOrderId;
            getTestResponse = await _client.GetAsync(uri);
            
            getTestResponse.EnsureSuccessStatusCode();
            body = await getTestResponse.Content.ReadAsStringAsync();

            var order = JsonHelper.DeserializeWithWebDefaults<Orders>(body);
            Assert.NotNull(order);
            
            Added_Order_Contains_All_Games(orderToBePlaced, order!);
        }

        private static void User_Registered_Correctly(Register userToBeRegistered, JwtResponse? jwtResponse)
        {
            Assert.NotNull(jwtResponse);
            Assert.NotNull(jwtResponse!.Token);
            Assert.Equal(userToBeRegistered.Firstname, jwtResponse.Firstname);
            Assert.Equal(userToBeRegistered.Lastname, jwtResponse.Lastname);
            
        }

        private static void Get_GameInfos_Not_Empty(List<PublicApi.DTO.v1.GameInfo> gameInfos)
        {
            Assert.NotEmpty(gameInfos);
        }

        private static void Get_GameInfo_Returns_Correct_Entity(PublicApi.DTO.v1.GameInfo chosenGame, PublicApi.DTO.v1.GameInfo? returnedGame)
        {
            Assert.NotNull(returnedGame);
            Assert.IsType<PublicApi.DTO.v1.GameInfo>(returnedGame);
            Assert.Equal(chosenGame.Id, returnedGame!.Id);
            Assert.Equal(chosenGame.Title, returnedGame.Title);
            Assert.Equal(chosenGame.Description, returnedGame.Description);
            Assert.Equal(chosenGame.RentalCost, returnedGame.RentalCost);
            Assert.Equal(chosenGame.ReplacementCost, returnedGame.ReplacementCost);
            Assert.Equal(chosenGame.ProductCode, returnedGame.ProductCode);
            Assert.Equal(chosenGame.PublisherId, returnedGame.PublisherId);
            Assert.Equal(chosenGame.LanguageId, returnedGame.LanguageId);
        }

        private async void Added_Order_Contains_All_Games(OrdersAdd orderToBePlaced, Orders orderId)
        {
            var uri = "/api/v1/OrderLines/Order/" + orderId;

            var getTestResponse = await _client.GetAsync(uri);
            
            getTestResponse.EnsureSuccessStatusCode();
            var body = await getTestResponse.Content.ReadAsStringAsync();

            var orderLines = JsonHelper.DeserializeWithWebDefaults<List<PublicApi.DTO.v1.OrderLine>>(body);
            
            Assert.Equal(orderToBePlaced.GameIds.Count, orderLines!.Count);
            foreach (var game in orderToBePlaced.GameIds)
            {
                Assert.Contains(orderLines, o => o.Id == game);
            }
        }
        
        private async void Game_Availability_Changes()
        {
            var uri = "/api/v1/Games";

            var getTestResponse = await _client.GetAsync(uri);
            
            getTestResponse.EnsureSuccessStatusCode();
            var body = await getTestResponse.Content.ReadAsStringAsync();

            var games = JsonHelper.DeserializeWithWebDefaults<List<Game>>(body);
            var game = games!.First();
            
            Assert.False(game!.Available);
        }
        
    }

}