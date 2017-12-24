namespace DreamHouse
{
    using DreamHouse.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using DreamHouse.DataReader;
    using System.Data.Entity.Infrastructure;

    public class DreamHousseBd : DbContext
    {
        public DreamHousseBd()
            : base("name=DreamHousseBd")
        {

        }
        public DbSet<FloorModel> Floors { get; set; }
        public DbSet<PavilionModel> Pavilions { get; set; }
        public DbSet<ShopModel> Shops { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<AnnouncementsModel> Announs { get; set; }
        public DbSet<CarouselItemModel> CarouselItems { get; set; }
        public DbSet<Counter> Counts { get; set; }

    }
}