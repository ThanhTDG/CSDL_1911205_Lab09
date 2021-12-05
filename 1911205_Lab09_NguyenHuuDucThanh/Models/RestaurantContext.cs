using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace _1911205_Lab09_NguyenHuuDucThanh.Models
{
    public class RestaurantContext:DbContext
    {
        public RestaurantContext() : base("RestaurantContext")
        {
        }
        public DbSet<Category> Category {  get; set; }
        public DbSet<Food> foods{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Food>()
                .HasRequired(x=> x.category)
                .WithMany()
                .HasForeignKey(x=>x.FoodCategoryId)
                .WillCascadeOnDelete(true);
        }
    }
}
