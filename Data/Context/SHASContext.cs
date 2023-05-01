namespace Data.Context
{
    using Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public class SHASContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Appliance> Appliances { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=localhost;Database=Smart Home Automation System;User Id=Borislav;Password=bmw645; TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Device>()
                .HasMany<Event>(d => d.Events)
                .WithOne(e => e.Device);

            modelBuilder.Entity<Device>()
                .HasMany<Appliance>(d => d.Appliances)
                .WithMany(a => a.Devices);

            modelBuilder.Entity<Event>()
                .HasOne<Device>(e => e.Device)
                .WithMany(d => d.Events);

            modelBuilder.Entity<Appliance>()
                .HasMany<Device>(a => a.Devices)
                .WithMany(d => d.Appliances);
        }
    }
}
