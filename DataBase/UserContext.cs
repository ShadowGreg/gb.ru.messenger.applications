using DataBase.BD;
using Microsoft.EntityFrameworkCore;

namespace DataBase;

public class UserContext: DbContext {
    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .LogTo(Console.WriteLine)
            .UseNpgsql("Host=127.0.0.1;Port=5433;Database=UserLoginDb;Username=postgres;Password=example;");

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");
            entity.HasIndex(e => e.Name).IsUnique();

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Salt).HasColumnName("salt");

            entity.Property(e => e.RoleId).HasConversion<int>();
        });

        modelBuilder
            .Entity<Role>()
            .Property(e => e.RoleId)
            .HasConversion<int>();

        modelBuilder
            .Entity<Role>().HasData(
                Enum.GetValues(typeof(RoleId))
                    .Cast<RoleId>()
                    .Select(e => new Role() {
                        RoleId = e,
                        Name = e.ToString()
                    }));
    }
}