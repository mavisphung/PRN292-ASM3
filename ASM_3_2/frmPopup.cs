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
    public partial class frmPopup : Form
    {
        Product p;
        public frmPopup(Product p)
        {
            InitializeComponent();
            this.p = p;
            LoadData();
        }

        public void LoadData()
        {
            lblProductID.Text = p.ID + "";
            lblProductName.Text = p.Name;
            lblUnitPrice.Text = p.UnitPrice + "";
            lblQuantity.Text = p.Quantity + "";
            lblSubtotal.Text = p.SubTotal + "";
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
