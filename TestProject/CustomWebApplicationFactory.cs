using System;
using System.Linq;
using DAL.App.EF;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // find the dbcontext
                var descriptor = services
                    .SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<AppDbContext>)
                    );
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<AppDbContext>(options =>
                {
                    // do we need unique db?
                    options.UseInMemoryDatabase(builder.GetSetting("test_database_name"));
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();

                db.Database.EnsureCreated();

                // data is already seeded
                if (db.Games.Any()) return;

                // seed data
                db.Addresses.Add(new Address()
                {
                    City = "Tallinn",
                    Region = "Mustamäe",
                    MachineLocation = "Mustamäe Keskus",
                    ServiceProvider = "Omniva"
                });

                db.PaymentMethods.Add(new PaymentMethod()
                {
                    Description = "Visa"
                });

                var publisherHasbro = db.Publishers.Add(new Publisher()
                {
                    Name = "Hasbro"
                });
                
                var languageEng = db.Languages.Add(new Language()
                {
                    Name = "eng"
                });
                
                var uno = db.GameInfos.Add(new GameInfo()
                {
                    Title = "Uno",
                    Description = "Uno description",
                    DateAdded = DateTime.Today,
                    MainPictureUrl = "Uno someUrl",
                    RentalCost = 5,
                    ReplacementCost = 15,
                    ProductCode = "123456",
                    PublisherId = publisherHasbro.Entity.Id,
                    LanguageId = languageEng.Entity.Id
                });
                db.SaveChanges();

                db.Games.Add(new Game()
                {
                    Available = true,
                    GameInfoId = uno.Entity.Id
                });

                db.SaveChanges();
            });
        }
    }
}