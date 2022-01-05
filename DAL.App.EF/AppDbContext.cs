using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public DbSet<Address> Addresses { get; set; } = default!;
        public DbSet<Award> Awards { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Game> Games { get; set; } = default!;
        public DbSet<GameAward> GameAwards { get; set; } = default!;
        public DbSet<GameCategory> GameCategories { get; set; } = default!;
        public DbSet<GameInfo> GameInfos { get; set; } = default!;
        public DbSet<Language> Languages { get; set; } = default!;
        public DbSet<OrderLine> OrderLines { get; set; } = default!;
        public DbSet<Orders> Orders { get; set; } = default!;
        public DbSet<PaymentMethod> PaymentMethods { get; set; } = default!;
        public DbSet<Publisher> Publishers { get; set; } = default!;
        public DbSet<Feedback> Feedbacks { get; set; } = default!;
        public DbSet<GamePicture> GamePictures { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // disable cascade delete for everything
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}