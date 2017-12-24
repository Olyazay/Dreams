namespace Admin
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DreamHouseAdminBd : DbContext
    {
        public DreamHouseAdminBd()
            : base("name=DreamHouseAdminBd")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AnnouncementsModels> AnnouncementsModels { get; set; }
        public virtual DbSet<CarouselItemModels> CarouselItemModels { get; set; }
        public virtual DbSet<CategoryModels> CategoryModels { get; set; }
        public virtual DbSet<Counters> Counters { get; set; }
        public virtual DbSet<FloorModels> FloorModels { get; set; }
        public virtual DbSet<PavilionModels> PavilionModels { get; set; }
        public virtual DbSet<ShopModels> ShopModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryModels>()
                .HasMany(e => e.ShopModels)
                .WithOptional(e => e.CategoryModels)
                .HasForeignKey(e => e.CategoryModelId);

            modelBuilder.Entity<FloorModels>()
                .HasMany(e => e.PavilionModels)
                .WithOptional(e => e.FloorModels)
                .HasForeignKey(e => e.FloorModelId);

            modelBuilder.Entity<ShopModels>()
                .HasMany(e => e.PavilionModels)
                .WithOptional(e => e.ShopModels)
                .HasForeignKey(e => e.Shop_Id);
        }
    }
}
