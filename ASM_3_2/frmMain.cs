using ProductLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM_3_2
{
    public partial class frmMain : Form
    {

        ProductManager pm;
        DataTable dtProducts;

        public frmMain()
        {
            InitializeComponent();
            pm = new ProductManager();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public void LoadData()
        {
            dtProducts = pm.GetProducts();

            txtProductID.DataBindings.Clear();
            txtProductName.DataBindings.Clear();
            txtUnitPrice.DataBindings.Clear();
            txtQuantity.DataBindings.Clear();
            lblSubtotal.DataBindings.Clear();

            txtProductID.DataBindings.Add("text", dtProducts, "ID");
            txtProductName.DataBindings.Add("text", dtProducts, "ProductName");
            txtUnitPrice.DataBindings.Add("text", dtProducts, "UnitPrice");
            txtQuantity.DataBindings.Add("text", dtProducts, "Quantity");
            dtProducts.Columns.Add("SubTotal", typeof(int), "Quantity * UnitPrice");
            lblSubtotal.DataBindings.Add("text", dtProducts, "SubTotal");

            dgvProductList.DataSource = dtProducts;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int id, quantity;
            decimal price;
            string strID = txtProductID.Text;
            string strPrice = txtUnitPrice.Text;
            string strQuantity = txtQuantity.Text;
            if (Validation.isNumber(strID, "ID")
                    && Validation.isNumber(strPrice, "UnitPrice")
                    && Validation.isNumber(strQuantity, "Quantity"))
            {
                id = int.Parse(strID);
                quantity = int.Parse(strQuantity);
                price = decimal.Parse(strPrice);
            }
            else return;

            string name = txtProductName.Text;
            if (name.Length == 0)
            {
                MessageBox.Show("The name is not empty");
                return;
            }

            Product p = new Product
            {
                ID = id,
                Name = name,
                UnitPrice = price,
                Quantity = quantity
            };

            bool r = pm.AddNewProduct(p);
            if (r)
            {
                MessageBox.Show("Add successfully");
            }
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id, quantity;
            decimal price;
            string strID = txtProductID.Text;
            string strPrice = txtUnitPrice.Text;
            string strQuantity = txtQuantity.Text;
            if (Validation.isNumber(strID, "ID")
                    && Validation.isNumber(strPrice, "UnitPrice")
                    && Validation.isNumber(strQuantity, "Quantity"))
            {
                id = int.Parse(strID);
                quantity = int.Parse(strQuantity);
                price = decimal.Parse(strPrice);
            }
            else return;

            string name = txtProductName.Text;
            if (name.Length == 0)
            {
                MessageBox.Show("The name is not empty");
                return;
            }

            Product p = new Product
            {
                ID = id,
                Name = name,
                UnitPrice = price,
                Quantity = quantity
            };

            bool r = pm.UpdateProduct(p);
            string result = r ? "Successfully" : "Failed";
            MessageBox.Show("Update " + result);
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id;
            string strID = txtProductID.Text;
            if (Validation.isNumber(strID, "ID"))
            {
                id = int.Parse(strID);
            }
            else return;

            bool r = pm.DeleteProduct(id);
            string result = r ? "Successfully" : "Failed";
            MessageBox.Show("Delete " + result);
            LoadData();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int id;
            string strID = txtSearch.Text;
            if (Validation.isNumber(strID, "ID"))
            {
                id = int.Parse(strID);
            }
            else return;

            Product p = pm.FindProduct(id);
            if (p == null)
            {
                MessageBox.Show("You don't have that ID in your database");
            } else
            {
                frmPopup popup = new frmPopup(p);
                popup.ShowDialog();
            }
            
        }
    }
}
