namespace _1911205_Lab09_NguyenHuuDucThanh.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<_1911205_Lab09_NguyenHuuDucThanh.Models.RestaurantContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(_1911205_Lab09_NguyenHuuDucThanh.Models.RestaurantContext context)
        {
            context.Category.AddOrUpdate(x => x.Id, new Models.Category() { Name = "Gà", type = Models.CategoryType.Food });
            context.Category.AddOrUpdate(x => x.Id, new Models.Category() { Name = "Lẩu", type = Models.CategoryType.Food });
            context.Category.AddOrUpdate(x => x.Id, new Models.Category() { Name = "Cá", type = Models.CategoryType.Food });
            context.Category.AddOrUpdate(x => x.Id, new Models.Category() { Name = "Tráng miệng", type = Models.CategoryType.Food });
            context.Category.AddOrUpdate(x => x.Id, new Models.Category() { Name = "Nướng", type = Models.CategoryType.Food });
            context.Category.AddOrUpdate(x => x.Id, new Models.Category() { Name = "Nước ngọt", type = Models.CategoryType.Drink });
            context.Category.AddOrUpdate(x => x.Id, new Models.Category() { Name = "Bia", type = Models.CategoryType.Drink });
            // Food
            context.foods.AddOrUpdate(x => x.Id, new Models.Food() { Name = "Gà nấu hoa cải", FoodCategoryId = 1, Price = 15000, Unit = "Nồi" });
            context.foods.AddOrUpdate(x => x.Id, new Models.Food() { Name = "Lẩu hải sản", FoodCategoryId = 2, Price = 35000, Unit = "Nồi" });
            context.foods.AddOrUpdate(x => x.Id, new Models.Food() { Name = "Lẩu bò ", FoodCategoryId = 2, Price = 25000, Unit = "Nồi" });
            context.foods.AddOrUpdate(x => x.Id, new Models.Food() { Name = "Cá chép om dưa", FoodCategoryId = 3, Price = 10000, Unit = "dĩa" });
            context.foods.AddOrUpdate(x => x.Id, new Models.Food() { Name = "Cá bóng tối", FoodCategoryId = 3, Price = 12000, Unit = "dĩa" });
            context.foods.AddOrUpdate(x => x.Id, new Models.Food() { Name = "Heo nướng kì lạ", FoodCategoryId = 4, Price = 15000, Unit = "dĩa" });
            context.foods.AddOrUpdate(x => x.Id, new Models.Food() { Name = "Bò nướng bóng đêm", FoodCategoryId = 4, Price = 15000, Unit = "dĩa" });
            context.foods.AddOrUpdate(x => x.Id, new Models.Food() { Name = "String vị yomost", FoodCategoryId = 5, Price = 5000, Unit = "Chai" });
            context.foods.AddOrUpdate(x => x.Id, new Models.Food() { Name = "Bò húc ông thọ", FoodCategoryId = 5, Price = 8000, Unit = "Chai" });
            context.foods.AddOrUpdate(x => x.Id, new Models.Food() { Name = "Trà xanh có độ", FoodCategoryId = 5, Price = 7000, Unit = "Chai" });
            context.foods.AddOrUpdate(x => x.Id, new Models.Food() { Name = "Bia 999", FoodCategoryId = 6, Price = 7000, Unit = "Lon" });
            context.foods.AddOrUpdate(x => x.Id, new Models.Food() { Name = "Bia con cá", FoodCategoryId = 6, Price = 7000, Unit = "Lon" });
            context.foods.AddOrUpdate(x => x.Id, new Models.Food() { Name = "Bia thủ đô", FoodCategoryId = 6, Price = 7000, Unit = "Chai" });
        }
    }
}
