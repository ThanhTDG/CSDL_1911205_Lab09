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
    public partial class UpdateCategoryForm : Form
    {
        private RestaurantContext _dbcontext;
        private int _categoryId;

        public UpdateCategoryForm(int? categoryId = null)
        {
            InitializeComponent();
            _dbcontext = new RestaurantContext();
            _categoryId = categoryId ?? 0;
        }
        private Category GetCategoryById(int categoryId)
        {
            return categoryId > 0 ? _dbcontext.Category.Find(categoryId) : null;
        }
        private void ShowCategory()
        {
            var category = GetCategoryById(_categoryId);
            if (category == null)
                return;
            txtCategoryId.Text = category.Id.ToString();
            txtCategoryName.Text = category.Name;
            cbbCategoryType.SelectedIndex = (int)category.type;
        }
        private Category GetUpdateCategory()
        {
            var category = new Category()
            {
                Name = txtCategoryName.Text.Trim(),
                type = (CategoryType)cbbCategoryType.SelectedIndex
            };
            if (_categoryId > 0)
            {
                category.Id = _categoryId;
            }
            return category;
        }
        private bool CheckUserInput()
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Tên nhóm thức ăn không được để trống");
                return false;
            }
            if(cbbCategoryType.SelectedIndex<0)
            {
                MessageBox.Show("Bạn chưa lựa chọn nhóm thức ăn", "thông báo");
                return false;
            }
            return true;
        }

        private void UpdateCategoryForm_Load(object sender, EventArgs e)
        {
            ShowCategory();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckUserInput())
            {
                var category = GetUpdateCategory();
                var old = GetCategoryById(_categoryId);
                if(old == null)
                {
                    _dbcontext.Category.Add(category);
                }
                else
                {
                    old.Name = category.Name;
                    old.type = category.type;
                }
                _dbcontext.SaveChanges();
                DialogResult = DialogResult.OK;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {  DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
