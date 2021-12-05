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
    public partial class UpdateFoodForm : Form
    {
        private RestaurantContext _dbContext;
        private int _foodId;
        public UpdateFoodForm(int? foodId=null)
        {
            InitializeComponent();
            _dbContext = new RestaurantContext();
            _foodId = foodId ?? 0;
        }

        private void LoadCategoriesToCommbox()
        {
            var categories = _dbContext.Category
                .OrderBy(x => x.Name).ToList();
            cbbCategory.DisplayMember = "Name";
            cbbCategory.ValueMember = "Id";
            cbbCategory.DataSource = categories;
        }
        private Food GetFoodByID(int foodId)
        {
            return foodId > 0?_dbContext.foods.Find(foodId) : null;
        }
      
        private void ShowFoodInformation()
        {
            var food= GetFoodByID(_foodId);
            if (food == null)
                return;
            txFoodId.Text = food.Id.ToString();
            txtFoodName.Text = food.Name;
            cbbCategory.SelectedValue = food.FoodCategoryId;
            txtFoodUnit.Text = food.Unit;
            nudFoodPrice.Value = food.Price;
            txtFoodNotes.Text = food.Note;
        }
        private bool CheckUserInput()
        {
            if (string.IsNullOrWhiteSpace(txtFoodName.Text))
            {
                MessageBox.Show("Tên món ăn,đồ uống không được để trống", "thông báo");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtFoodUnit.Text))
            {
                MessageBox.Show("Đơn vị tính không được để trống","thông báo");
                return false;
            }
            if (nudFoodPrice.Value.Equals(0))
            {
                MessageBox.Show("Giá  của thức ăn phải lớn hiwn 0", "thông báo");
                return false;
            }
            if (cbbCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn chưa lựa chọn nhóm thức ăn", "thông báo");
                return false;
            }
            return true;
        }
        private Food GetUpdateFood()
        {
            var food = new Food()
            {
                Name = txtFoodName.Text.Trim(),
                FoodCategoryId = (int)cbbCategory.SelectedValue,
                Unit = txtFoodUnit.Text,
                Price = (int)nudFoodPrice.Value,
                Note = txtFoodNotes.Text
            };
            if(_foodId > 0)
            {
                food.Id = _foodId;
            }
            return food;
        }

        private void UpdateFoodForm_Load(object sender, EventArgs e)
        {
            LoadCategoriesToCommbox();
            ShowFoodInformation();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckUserInput())
            {
                var newFood = GetUpdateFood();
                var oldFood = GetFoodByID(_foodId);
                if(oldFood == null)
                {
                    _dbContext.foods.Add(newFood);
                }
                else{
                    oldFood.Name = newFood.Name;
                    oldFood.FoodCategoryId = newFood.FoodCategoryId;
                    oldFood.Unit = newFood.Unit;
                    oldFood.Price = newFood.Price;
                    oldFood.Note = newFood.Note;
                }
                _dbContext.SaveChanges();
                DialogResult = DialogResult.OK;
            }
        }
    }
}
