using MessageDataBase.BD;
using Microsoft.EntityFrameworkCore;

namespace MessageDataBase;

public class MessagesContext: DbContext {

    public virtual DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .LogTo(Console.WriteLine)
            .UseNpgsql("Host=127.0.0.1;Port=5433;Database=MessageDb;Username=postgres;Password=example;");

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("message_pkey");

            entity.ToTable("messages");

            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.IsReceived)
                .IsRequired();
        });
    }
}