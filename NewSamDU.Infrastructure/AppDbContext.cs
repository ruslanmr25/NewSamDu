using System;
using Microsoft.EntityFrameworkCore;
using NewSamDU.Domain.Entities;
using NewSamDU.Domain.Enums;

namespace NewSamDU.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<Slide> Slides { get; set; }

    public DbSet<Menu> Menus { get; set; }

    public DbSet<Page> Pages { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<Announcement> Announcements { get; set; }

    public DbSet<News> News { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSeeding(
                (context, _) =>
                {
                    var superAdmin = context
                        .Set<User>()
                        .FirstOrDefault(c => c.FullName == "Super Admin");
                    superAdmin ??= context
                        .Set<User>()
                        .Add(
                            new User
                            {
                                FullName = "Admin",
                                Username = "admin",
                                Role = Domain.Enums.Role.Admin,
                                Password =
                                    "$2a$11$QbiQHQTv47aIbaXNhX.G3.QW3OzCU/AnTkK6EUmqFbxAzBN4J74V.",
                            }
                        )
                        .Entity;

                    context.SaveChanges();
                }
            )
            .UseAsyncSeeding(
                async (context, _, cancellationToken) =>
                {
                    var superAdmin = await context
                        .Set<User>()
                        .FirstOrDefaultAsync(c => c.FullName == "Admin");

                    await context
                        .Set<User>()
                        .AddAsync(
                            new User
                            {
                                FullName = "Admin",
                                Username = "admin",
                                Role = Role.Admin,
                                Password =
                                    "$2a$11$QbiQHQTv47aIbaXNhX.G3.QW3OzCU/AnTkK6EUmqFbxAzBN4J74V.",
                            }
                        );

                    await context.SaveChangesAsync();
                }
            );
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Qo‘shimcha konfiguratsiyalar (optional)
    }
}
