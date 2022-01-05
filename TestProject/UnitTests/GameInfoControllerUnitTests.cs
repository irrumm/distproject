using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App;
using BLL.App.DTO;
using BLL.App.DTO.MappingProfiles;
using Contracts.BLL.App;
using DAL.App.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.Controllers;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.UnitTests
{
    public class GameInfoControllerUnitTests
    {
        private readonly ITestOutputHelper _helper;
        private readonly IAppBLL _bll;
        private readonly AppDbContext _ctx;

        public GameInfoControllerUnitTests(ITestOutputHelper helper)
        {
            _helper = helper;
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DAL.App.DTO.MappingProfiles.AutoMapperProfile>();
            });
            var dalMapper = mockMapper.CreateMapper();
            mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            var bllMapper = mockMapper.CreateMapper();

            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            _ctx = new AppDbContext(optionBuilder.Options);
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<GameInfosController>();

            var unit = new AppUnitOfWork(_ctx, dalMapper);
            _bll = new AppBLL(unit, bllMapper);
        }
        
        // GET ALL
        [Fact]
        public async Task GameInfoGetAllEmpty()
        {
            var gameInfos = await _bll.GameInfos.GetAllAsync();
            Assert.NotNull(gameInfos);
            Assert.IsAssignableFrom<IEnumerable<GameInfo>>(gameInfos);
            Assert.Empty(gameInfos);
        }
        
        [Fact]
        public async Task GameInfoGetAllNotEmpty()
        {
            await SeedData();
            
            var gameInfos = await _bll.GameInfos.GetAllAsync();
            Assert.NotNull(gameInfos);
            
            Assert.IsAssignableFrom<IEnumerable<GameInfo>>(gameInfos);
            Assert.NotEmpty(gameInfos);
        }
        
        // GET ALL BY TITLE
        [Fact]
        public async Task GameInfoGetAllByTitleEmpty()
        {
            var result = await _bll.GameInfos.GetAllByTitleApiAsync("unexistent");
            Assert.NotNull(result);
            Assert.Empty(result);
        }
        
        [Fact]
        public async Task GameInfoGetAllByTitleNoMatches()
        {
            await SeedData();
            var result = await _bll.GameInfos.GetAllByTitleApiAsync("no matches");
            Assert.NotNull(result);
            Assert.Empty(result);
        }
        
        [Fact]
        public async Task GameInfoGetAllByTitleSucceed()
        {
            await SeedData();
            var result = await _bll.GameInfos.GetAllByTitleApiAsync("Alias");
            Assert.NotNull(result);
            var gameInfos = result.ToList();
            Assert.NotEmpty(gameInfos);
            Assert.Single(gameInfos);

            Assert.Contains(gameInfos, gameInfo => gameInfo.Title.Equals("Alias"));
        }
        
        [Fact]
        public async Task GameInfoGetAllByTitleCaseInsensitive()
        {
            await SeedData();
            var result = await _bll.GameInfos.GetAllByTitleApiAsync("aLiAs");
            Assert.NotNull(result);
            var gameInfos = result.ToList();
            Assert.NotEmpty(gameInfos);
            Assert.Single(gameInfos);

            Assert.Contains(gameInfos, gameInfo => gameInfo.Title.Equals("Alias"));
        }

        [Fact]
        public async Task GameInfoGetAllByTitleReturnsCorrectEntitiesOne()
        {
            await SeedData();
            
            var result = await _bll.GameInfos.GetAllByTitleApiAsync("Uno");
            _helper.WriteLine($"result: {result}");
            Assert.NotNull(result);
            
            var gameInfos = result.ToList();
            Assert.NotEmpty(gameInfos);
            Assert.Equal(2, gameInfos.Count);

            Assert.Contains(gameInfos, gameInfo => gameInfo.Title.Equals("Uno"));
            Assert.Contains(gameInfos, gameInfo => gameInfo.Title.Equals("Uno Flip"));
        }
        
        [Fact]
        public async Task GameInfoGetAllByTitleReturnsCorrectEntitiesTwo()
        {
            await SeedData();
            
            // Alias and Uno flip
            var result = await _bll.GameInfos.GetAllByTitleApiAsync("li");
            Assert.NotNull(result);
            
            var gameInfos = result.ToList();
            Assert.NotEmpty(gameInfos);
            Assert.Equal(2, gameInfos.Count);

            Assert.Contains(gameInfos, gameInfo => gameInfo.Title.Equals("Alias"));
            Assert.Contains(gameInfos, gameInfo => gameInfo.Title.Equals("Uno Flip"));
        }
        
        // GET ONE
        [Fact]
        public async Task GameInfoGetOneNotFound()
        {
            var result = await _bll.GameInfos.FirstOrDefaultAsync(Guid.Empty);
            Assert.Null(result);
        }

        [Fact]
        public async Task GameInfoGetOneSucceed()
        {
            await SeedData();
            
            var games = await _bll.GameInfos.GetAllAsync();
            Assert.NotNull(games);
            Assert.IsAssignableFrom<IEnumerable<BLL.App.DTO.GameInfo>>(games);
            
            var gameInfos = games.ToList();
            Assert.NotEmpty(gameInfos);
            
            var game = gameInfos!.First();
            Assert.IsType<GameInfo>(game);
            
            var result = await _bll.GameInfos.FirstOrDefaultAsync(game.Id);
            Assert.NotNull(result);
            Assert.IsType<GameInfo>(result);
        }

        [Fact]
        public async Task GameInfoGetOneDataMatches()
        {
            await SeedData();
            
            var games = await _bll.GameInfos.GetAllAsync();
            Assert.NotNull(games);
            
            var gameInfos = games.ToList();
            Assert.NotEmpty(gameInfos);
            
            var game = gameInfos!.First();
            var result = await _bll.GameInfos.FirstOrDefaultAsync(game.Id);
            Assert.NotNull(result);
            
            Assert.Equal(game.Id, result!.Id);
            Assert.Equal(game.Title, result.Title);
            Assert.Equal(game.Description, result.Description);
            Assert.Equal(game.RentalCost, result.RentalCost);
            Assert.Equal(game.ReplacementCost, result.ReplacementCost);
            Assert.Equal(game.ProductCode, result.ProductCode);
            Assert.Equal(game.PublisherId, result.PublisherId);
            Assert.Equal(game.LanguageId, result.LanguageId);
        }
        
        // GET FILTERED
        [Fact]
        public async Task GameInfoNoFilters()
        {
            await SeedData();
            List<Guid> categories = new List<Guid>();
            List<Guid> publishers = new List<Guid>();
            List<Guid> languages = new List<Guid>();
            
            var result = await _bll.GameInfos.GetAllFiltered(categories, languages, publishers, 0, 0);
            Assert.NotNull(result);
            
            var gameInfos = result.ToList();
            Assert.Empty(gameInfos);
        }
        
        [Fact]
        public async Task GameInfoFilterByPrice0To20()
        {
            await SeedData();
            List<Guid> categories = new List<Guid>();
            List<Guid> publishers = new List<Guid>();
            List<Guid> languages = new List<Guid>();
            
            // min 0, max 20
            var result = await _bll.GameInfos.GetAllFiltered(categories, languages, publishers, 0, 20);
            Assert.NotNull(result);
            var gameInfos = result.ToList();
            
            Assert.Equal(3, gameInfos.Count());

            foreach (var game in gameInfos)
            {
                Assert.True(game.RentalCost >= 0 && game.RentalCost <= 20);
            }
            
        }
        
        [Fact]
        public async Task GameInfoFilterByPrice0To5()
        {
            await SeedData();
            List<Guid> categories = new List<Guid>();
            List<Guid> publishers = new List<Guid>();
            List<Guid> languages = new List<Guid>();
            
            // min 5, max 15
            var result = await _bll.GameInfos.GetAllFiltered(categories, languages, publishers, 0, 5);
            Assert.NotNull(result);
            var gameInfos = result.ToList();
            
            Assert.Equal(2, gameInfos.Count());
            foreach (var game in gameInfos)
            {
                Assert.True(game.RentalCost >= 0 && game.RentalCost <= 5);
            }
        }
        
        [Fact]
        public async Task GameInfoFilterByPrice20()
        {
            await SeedData();
            List<Guid> categories = new List<Guid>();
            List<Guid> publishers = new List<Guid>();
            List<Guid> languages = new List<Guid>();
            
            // min 5, max 15
            var result = await _bll.GameInfos.GetAllFiltered(categories, languages, publishers, 10, 10);
            Assert.NotNull(result);
            var gameInfos = result.ToList();

            Assert.Single(gameInfos);
            foreach (var game in gameInfos)
            {
                Assert.True(game.RentalCost == 10);
            }
        }
        
        [Fact]
        public async Task GameInfoFilterByCategory()
        {
            await SeedData();
            List<Guid> categories = new List<Guid>();
            List<Guid> publishers = new List<Guid>();
            List<Guid> languages = new List<Guid>();

            var allCategories = await _bll.Categories.GetAllAsync();
            Assert.NotNull(allCategories);
            
            var categoryList = allCategories.ToList();
            Assert.NotEmpty(categoryList);

            var category = categoryList.First();
            categories.Add(category.Id);

            var result = await _bll.GameInfos.GetAllFiltered(categories, languages, publishers, 0, 20);
            Assert.NotNull(result);
            var gameInfos = result.ToList();
            Assert.Equal(2, gameInfos.Count);
            
            foreach (var game in gameInfos)
            {
                var gameCategories = await _bll.GameCategories.GetAllByGameApiAsync(game.Id);
                Assert.Contains(gameCategories, gameCategory => gameCategory.CategoryId == category.Id);
            }
        }
        
        [Fact]
        public async Task GameInfoFilterByPublisher()
        {
            await SeedData();
            List<Guid> categories = new List<Guid>();
            List<Guid> publishers = new List<Guid>();
            List<Guid> languages = new List<Guid>();

            var allPublishers = await _bll.Publishers.GetAllAsync();
            Assert.NotNull(allPublishers);
            var collection = allPublishers.ToList();
            Assert.NotEmpty(collection);
            var publisherId = collection.First().Id;
            publishers.Add(publisherId);

            var result = await _bll.GameInfos.GetAllFiltered(categories, languages, publishers, 0, 20);
            Assert.NotNull(result);
            
            var gameInfos = result.ToList();
            Assert.NotEmpty(gameInfos);
            
            foreach (var game in gameInfos)
            {
                Assert.True(game.PublisherId == publisherId);
            }
        }
        
        [Fact]
        public async Task GameInfoFilterByLanguage()
        {
            await SeedData();
            List<Guid> categories = new List<Guid>();
            List<Guid> publishers = new List<Guid>();
            List<Guid> languages = new List<Guid>();

            var allLanguages = await _bll.Languages.GetAllAsync();
            Assert.NotNull(allLanguages);
            var collection = allLanguages.ToList();
            Assert.NotEmpty(collection);
            var languageId = collection.First().Id;
            languages.Add(languageId);

            var result = await _bll.GameInfos.GetAllFiltered(categories, languages, publishers, 0, 20);
            Assert.NotNull(result);
            
            var gameInfos = result.ToList();
            Assert.NotEmpty(gameInfos);
            
            foreach (var game in gameInfos)
            {
                Assert.True(game.LanguageId == languageId);
            }
        }
        
        // CREATE
        [Fact]
        public async Task GameInfoCreateReturnsCorrectEntity()
        {
            var game = _bll.GameInfos.Add(new GameInfo()
            {
                Title = "Uno",
                Description = "Uno description",
                DateAdded = DateTime.Today,
                MainPictureUrl = "Uno someUrl",
                RentalCost = 5,
                ReplacementCost = 15,
                ProductCode = "123456",
                PublisherId = Guid.NewGuid(),
                LanguageId = Guid.NewGuid()
            });
            await _bll.SaveChangesAsync();
            
            var result = await _bll.GameInfos.FirstOrDefaultAsync(game.Id);
            Assert.NotNull(result);
            Assert.IsType<BLL.App.DTO.GameInfo>(result);
        }
        
        [Fact]
        public async Task GameInfoCreateDataMatches()
        {
            var game = _bll.GameInfos.Add(new GameInfo()
            {
                Title = "Uno",
                Description = "Uno description",
                DateAdded = DateTime.Today,
                MainPictureUrl = "Uno someUrl",
                RentalCost = 5,
                ReplacementCost = 15,
                ProductCode = "123456",
                PublisherId = Guid.NewGuid(),
                LanguageId = Guid.NewGuid()
            });
            await _bll.SaveChangesAsync();
            
            var result = await _bll.GameInfos.FirstOrDefaultAsync(game.Id);
            Assert.NotNull(result);
            Assert.IsType<BLL.App.DTO.GameInfo>(result);
            
            Assert.Equal(game.Id, result!.Id);
            Assert.Equal(game.Title, result.Title);
            Assert.Equal(game.Description, result.Description);
            Assert.Equal(game.RentalCost, result.RentalCost);
            Assert.Equal(game.ReplacementCost, result.ReplacementCost);
            Assert.Equal(game.ProductCode, result.ProductCode);
            Assert.Equal(game.PublisherId, result.PublisherId);
            Assert.Equal(game.LanguageId, result.LanguageId);
        }

        // UPDATE
        [Fact]
        public async Task UpdateGameInfoSuccess()
        {
            await _ctx.GameInfos.AddAsync(new Domain.App.GameInfo()
            {
                Title = "Old title",
                Description = "description",
                DateAdded = DateTime.Today,
                MainPictureUrl = "someUrl",
                RentalCost = 5,
                ReplacementCost = 15,
                ProductCode = "123456",
                PublisherId = Guid.NewGuid(),
                LanguageId = Guid.NewGuid()
            });
            await _bll.SaveChangesAsync();
            
            var games = _ctx.GameInfos.AsQueryable();
            Assert.Single(games);
            
            var game = games.First();
            
            Assert.NotNull(game);

            game.Title = "New title";
            _ctx.Entry(game).State = EntityState.Modified;
            await _bll.SaveChangesAsync();

            var gameInDB = await _bll.GameInfos.FirstOrDefaultAsync(game.Id);
            Assert.NotNull(gameInDB);
            Assert.Equal(game.Id, gameInDB!.Id);
            Assert.True(gameInDB.Title.Equals("New title"));
        }
        
        // DELETE
        [Fact]
        public async Task DeleteGameInfoSucceed()
        {
            await _ctx.GameInfos.AddAsync(new Domain.App.GameInfo()
            {
                Title = "Old title",
                Description = "description",
                DateAdded = DateTime.Today,
                MainPictureUrl = "someUrl",
                RentalCost = 5,
                ReplacementCost = 15,
                ProductCode = "123456",
                PublisherId = Guid.NewGuid(),
                LanguageId = Guid.NewGuid()
            });
            await _bll.SaveChangesAsync();
            
            var games = _ctx.GameInfos.AsQueryable();
            Assert.Single(games);
            
            var game = games.First();
            Assert.NotNull(game);
            
            _ctx.Entry(game).State = EntityState.Deleted;
            await _bll.SaveChangesAsync();

            var gameInDB = await _bll.GameInfos.FirstOrDefaultAsync(game.Id);
            Assert.Null(gameInDB);
            
            var allGames = await _bll.GameInfos.GetAllAsync();
            Assert.NotNull(allGames);
            Assert.Empty(allGames);
        }
        
        [Fact]
        public async Task DeleteGameInfoNotFound()
        {
            await Assert.ThrowsAsync<NullReferenceException>(() => _bll.GameInfos.RemoveAsync(Guid.Empty));
        }
        
        // EXISTS
        [Fact]
        private async Task GameInfoDoesNotExist()
        {
            var result = await _bll.GameInfos.ExistsAsync(Guid.Empty);
            Assert.False(result);
        }
        
        [Fact]
        private async Task GameInfoExists()
        {
            var game = _bll.GameInfos.Add(new GameInfo()
            {
                Title = "Uno",
                Description = "Uno description",
                DateAdded = DateTime.Today,
                MainPictureUrl = "Uno someUrl",
                RentalCost = 5,
                ReplacementCost = 15,
                ProductCode = "123456",
                PublisherId = Guid.NewGuid(),
                LanguageId = Guid.NewGuid()
            });
            await _bll.SaveChangesAsync();
            
            var result = await _bll.GameInfos.ExistsAsync(game.Id);
            Assert.True(result);
        }
        
        // SEED DATA
        private async Task SeedData()
        {
            // Categories
            var categoryFamily = _bll.Categories.Add(new Category()
            {
                Name = "Family"
            });
            var categoryCard = _bll.Categories.Add(new Category()
            {
                Name = "Card"
            });

            // Languages
            var languageEst = _bll.Languages.Add(new Language()
            {
                Name = "est"
            });
            var languageEng = _bll.Languages.Add(new Language()
            {
                Name = "eng"
            });

            // Publishers
            var publisherHasbro = _bll.Publishers.Add(new Publisher()
            {
                Name = "Hasbro"
            });
            var publisherTactic = _bll.Publishers.Add(new Publisher()
            {
                Name = "Tactic"
            });

            await _bll.SaveChangesAsync();
            
            //GameInfos
            var uno = _bll.GameInfos.Add(new GameInfo()
            {
                Title = "Uno",
                Description = "Uno description",
                DateAdded = DateTime.Today,
                MainPictureUrl = "Uno someUrl",
                RentalCost = 5,
                ReplacementCost = 15,
                ProductCode = "123456",
                PublisherId = publisherHasbro.Id,
                LanguageId = languageEst.Id
            });
            
            var unoFlip = _bll.GameInfos.Add(new GameInfo()
            {
                Title = "Uno Flip",
                Description = "Uno Flip description",
                DateAdded = DateTime.Today,
                MainPictureUrl = "Uno Flip someUrl",
                RentalCost = 5,
                ReplacementCost = 15,
                ProductCode = "123789",
                PublisherId = publisherHasbro.Id,
                LanguageId = languageEng.Id
            });
            
            var alias = _bll.GameInfos.Add(new GameInfo()
            {
                Title = "Alias",
                Description = "Alias description",
                DateAdded = DateTime.Today,
                MainPictureUrl = "Alias someUrl",
                RentalCost = 10,
                ReplacementCost = 20,
                ProductCode = "654321",
                PublisherId = publisherTactic.Id,
                LanguageId = languageEst.Id
            });
            
            await _bll.SaveChangesAsync();
            
            // Game Categories
            _bll.GameCategories.Add(new GameCategory()
            {
                CategoryId = categoryCard.Id,
                GameInfoId = uno.Id
            });
            _bll.GameCategories.Add(new GameCategory()
            {
                CategoryId = categoryFamily.Id,
                GameInfoId = uno.Id
            });
            
            _bll.GameCategories.Add(new GameCategory()
            {
                CategoryId = categoryCard.Id,
                GameInfoId = unoFlip.Id
            });
            
            _bll.GameCategories.Add(new GameCategory()
            {
                CategoryId = categoryFamily.Id,
                GameInfoId = alias.Id
            });
            await _bll.SaveChangesAsync();
        }
    }
}