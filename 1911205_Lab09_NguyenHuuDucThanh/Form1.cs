using _1911205_Lab09_NguyenHuuDucThanh.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1911205_Lab09_NguyenHuuDucThanh
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<FoodModel> GetFoodByCategory(int? categoryID)
        {
            var dbContext = new RestaurantContext();
            var foods = dbContext.foods.AsQueryable();
            if(categoryID !=null  && categoryID>0)
            {
                foods = foods.Where(x=>x.FoodCategoryId==categoryID);
            }
            return foods
                .OrderBy(x => x.Name)
                .Select(x => new FoodModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Unit = x.Unit,
                    Price = x.Price,
                    Notes = x.Note,
                    CategoryName = x.category.Name
                }).ToList();
        }
        private List<FoodModel> GetFoodByCategoryType(CategoryType categoryType)
        {
            var dbContext = new RestaurantContext();
            return dbContext.foods
                .Where(x=>x.category.type == categoryType)
                .OrderBy(x => x.Name)
                .Select(x => new FoodModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Unit = x.Unit,
                    Price = x.Price,
                    Notes = x.Note,
                    CategoryName = x.category.Name
                }).ToList();
        }

        private void showFoodsForNode(TreeNode node)
        {
            lvwFood.Items.Clear();
            if (node == null)
                return;
            List<FoodModel> foodModels = null;
            if(node.Level == 1)
            {
                var categoryType = (CategoryType)node.Tag;
                foodModels = GetFoodByCategoryType(categoryType);
            }
            else
            {
                var category = node.Tag as Category;
                foodModels = GetFoodByCategory(category?.Id);
            }
            ShowFoodsOnListView(foodModels);
        }

        private void ShowFoodsOnListView(List<FoodModel> foodModels)
        {
            foreach (var foodModel in foodModels)
            {
                var item = lvwFood.Items.Add(foodModel.Id.ToString());
                item.SubItems.Add(foodModel.Name);
                item.SubItems.Add(foodModel.Unit);
                item.SubItems.Add(foodModel.Price.ToString("##,####"));
                item.SubItems.Add(foodModel.CategoryName);
                item.SubItems.Add(foodModel.Notes);
            }
        }

        List<Category> GetCategories()
        {
            var dbContext = new RestaurantContext();
            return dbContext.Category.OrderBy(x => x.Name).ToList();
        }
        private void ShowCategories()
        {
            tvwCategory.Nodes.Clear();
            var cateMap = new Dictionary<CategoryType, string>()
            {
                [CategoryType.Food] = "Đồ ăn",
                [CategoryType.Drink] = "Thức uống"
            };
            var rootNode = tvwCategory.Nodes.Add("Tất cả");
            var categories = GetCategories();
            foreach(var cateType in cateMap)
            {
                var childNode = rootNode.Nodes.Add(cateType.Key.ToString(), cateType.Value);
                childNode.Tag = cateType.Key;
                foreach(var category in categories)
                {
                    if (category.type != cateType.Key) continue;
                    var grantChildNode = childNode.Nodes.Add(category.Id.ToString(),category.Name);
                    grantChildNode.Tag = category;
                }
            }
            tvwCategory.ExpandAll();
            tvwCategory.SelectedNode = rootNode;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            ShowCategories();
        }
        private void btnReloadCategory_Click(object sender, EventArgs e)
        {
            showFoodsForNode(tvwCategory.SelectedNode);
        }

        private void tvwCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            showFoodsForNode(e.Node);
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            var dialog = new UpdateCategoryForm();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ShowCategories();
            }
        }

        private void tvwCategory_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null || e.Node.Level < 2 || e.Node.Tag == null)
                return;
            var category = e.Node.Tag as Category;
            var dialog = new UpdateCategoryForm(category?.Id);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ShowCategories();
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            var dialog = new UpdateFoodForm();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                showFoodsForNode(tvwCategory.SelectedNode);
            }
        }

        private void lvwFood_DoubleClick(object sender, EventArgs e)
        {
            if (lvwFood.SelectedItems.Count == 0)
                return;
            var foodid=int.Parse(lvwFood.SelectedItems[0].Text);    
            var dialog = new UpdateFoodForm(foodid);
            if(dialog.ShowDialog(this)== DialogResult.OK){
                showFoodsForNode(tvwCategory.SelectedNode);
            }
        }

        private void btnReloadFood_Click(object sender, EventArgs e)
        {
            showFoodsForNode(tvwCategory.SelectedNode);
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            if (lvwFood.SelectedItems.Count == 0)
                return;
           
            DialogResult dialog=MessageBox.Show("Bạn có muốn xóa đi thức ăn này ?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                RestaurantContext restaurantContext = new RestaurantContext();
                int foodId = int.Parse(lvwFood.SelectedItems[0].Text);
                var food = restaurantContext.foods.Find(foodId);
                restaurantContext.foods.Remove(food);
                restaurantContext.SaveChanges();    
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                showFoodsForNode(tvwCategory.SelectedNode);
            }

        }
    }
}
