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
    public partial class StaffView : SampleViewCf
    {
        public StaffView()
        {
            InitializeComponent();
        }

        private void StaffView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        public void GetData()
        {
            string qry = "Select * From staff where staffName like '%" + txtSearch.Text + "%' ";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvPhone);
            lb.Items.Add(dgvRole);

            MainClass.LoadData(qry, dataGridView, lb);
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            StaffAdd frm = new StaffAdd();
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
                StaffAdd frm = new StaffAdd();
                frm.id = Convert.ToInt32(dataGridView.CurrentRow.Cells["dgvid"].Value);
                frm.txtName.Text = Convert.ToString(dataGridView.CurrentRow.Cells["dgvName"].Value);
                frm.txtPhone.Text = Convert.ToString(dataGridView.CurrentRow.Cells["dgvPhone"].Value);
                frm.cbRole.Text = Convert.ToString(dataGridView.CurrentRow.Cells["dgvRole"].Value);
                frm.ShowDialog();
                GetData();
            }

            if (dataGridView.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                int id = Convert.ToInt32(dataGridView.CurrentRow.Cells["dgvid"].Value);
                string qry = "Delete from staff where staffID =" + id + "";
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
