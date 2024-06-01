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
    public partial class CategoryView : SampleViewCf
    {
        public CategoryView()
        {
            InitializeComponent();
        }


        public void GetData()
        {
            string qry = "Select * From category where catName like '%" + txtSearch.Text + "%' ";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);

            MainClass.LoadData(qry, dataGridView, lb);
        }
        private void CategoryView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        public virtual void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public override void button1_Click(object sender, EventArgs e)
        {
            frmCategoryAdd frm = new frmCategoryAdd();
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
                frmCategoryAdd frm = new frmCategoryAdd();
                frm.id = Convert.ToInt32(dataGridView.CurrentRow.Cells["dgvid"].Value);
                frm.txtName.Text = Convert.ToString(dataGridView.CurrentRow.Cells["dgvName"].Value);
                frm.ShowDialog();
                GetData();
            }

            if (dataGridView.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                int id = Convert.ToInt32(dataGridView.CurrentRow.Cells["dgvid"].Value);
                string qry = "Delete from category where catID =" + id + "";
                Hashtable ht = new Hashtable();
                MainClass.SQL(qry, ht);
                MessageBox.Show("Deleted successfully");
                GetData();
            }
        }

        private void dataGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
