using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1911205_Lab09_NguyenHuuDucThanh.Models
{
    public class Food
    {
        public int Id {  get; set; }
        public string Name {  get; set; }
        public string Unit {  get; set; }
        public int FoodCategoryId {  get; set; }
        public int Price {  get; set; }
        public string Note {  get; set; }
        public Category category {  get; set; }

    }
}
