using MessageDataBase.BD;
using Microsoft.EntityFrameworkCore;

namespace MessageDataBase;

public class MessagesContext: DbContext {
    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .LogTo(Console.WriteLine)
            .UseNpgsql("Host=127.0.0.1;Port=5433;Database=UserLoginDb;Username=postgres;Password=example;");

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("message_pkey");

            entity.ToTable("messages");

            entity.HasOne(e => e.Sender)
                .WithMany()
                .HasForeignKey(e => e.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Receiver)
                .WithMany()
                .HasForeignKey(e => e.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.IsReceived)
                .IsRequired();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.Property(e => e.Id).HasColumnName("id");
            
        });
    }
}