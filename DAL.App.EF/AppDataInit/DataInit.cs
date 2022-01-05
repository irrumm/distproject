using System;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.AppDataInit
{
    public static class DataInit
    {
        public static void DropDatabase(AppDbContext ctx)
        {
            ctx.Database.EnsureDeleted();
        }

        public static void MigrateDatabase(AppDbContext ctx)
        {
            ctx.Database.Migrate();
        }

        public static void SeedAppData(AppDbContext ctx)
        {
            ctx.Awards.Add(new Award() {Host = "Golden Geek Awards", Name = "Best Family Board Game"});
            ctx.Awards.Add(new Award() {Host = "Golden Geek Awards", Name = "Best Card Game"});
            ctx.Awards.Add(new Award() {Host = "Spiel de Jahres", Name = "Game Of The Year"});
            ctx.Awards.Add(new Award() {Host = "American Tabletop Awards", Name = "Game Of The Year"});
            
            ctx.Languages.Add(new Language() {Name = "est"});
            ctx.Languages.Add(new Language() {Name = "rus"});
            
            ctx.Categories.Add(new Category() {Name = "Family"});
            
            ctx.PaymentMethods.Add(new PaymentMethod() {Description = "Paypal"});
            
            ctx.Publishers.Add(new Publisher() {Name = "Hasbro"});
            
            ctx.SaveChanges();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var userRole = new AppRole {Name = "User"};
            
            var res = roleManager.CreateAsync(userRole).Result;
            if (!res.Succeeded)
            {
                foreach (var identityError in res.Errors)
                {
                    Console.WriteLine("Cant create role! Error: " + identityError.Description);
                }
            }
            
            var adminRole = new AppRole {Name = "Admin"};
            
            var result = roleManager.CreateAsync(adminRole).Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant create role! Error: " + identityError.Description);
                }
            }

            var user = new AppUser {Email = "admin@mail.com", Firstname = "Admin", Lastname = "A"};
            user.UserName = user.Email;

            result = userManager.CreateAsync(user, "password1").Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant create user! Error: " + identityError.Description);
                }
            }

            result = userManager.AddToRoleAsync(user, "Admin").Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant add user to role! Error: " + identityError.Description);
                }
            }

        }
    }
}