using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Quan_Coffee
{
    public partial class ProductView : SampleViewCf
    {
        public ProductView()
        {
            InitializeComponent();
        }

        private void ProductView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        public void GetData()
        {
            string qry = "Select prID,prName,prPrice, CategoryID, c.catName from products p inner join category c on c.catID = p.CategoryID where prName like '%" + txtSearch.Text + "%' ";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvPrice);
            lb.Items.Add(dgvcatID);
            lb.Items.Add(dgvcat);

            MainClass.LoadData(qry, dataGridView, lb);
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            ProductAdd frm = new ProductAdd();
            frm.ShowDialog();
            GetData();
        }

        public override void textBox1_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }


        public override void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                ProductAdd frm = new ProductAdd();
                frm.id = Convert.ToInt32(dataGridView.CurrentRow.Cells["dgvid"].Value);
                frm.cID = Convert.ToInt32(dataGridView.CurrentRow.Cells["dgvcatID"].Value);
                //frm.txtPrice.Text = Convert.ToString(dataGridView.CurrentRow.Cells["dgvPrice"].Value);
                //frm.cbCat.Text = Convert.ToString(dataGridView.CurrentRow.Cells["dgvcat"].Value);
                frm.ShowDialog();
                GetData();
            }

            if (dataGridView.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                int id = Convert.ToInt32(dataGridView.CurrentRow.Cells["dgvid"].Value);
                string qry = "Delete from products where prID =" + id + "";
                Hashtable ht = new Hashtable();
                MainClass.SQL(qry, ht);
                MessageBox.Show("Deleted successfully");
                GetData();
            }
        }

        private void dataGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
