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
    public partial class StaffAdd : SampleAddCf
    {
        public StaffAdd()
        {
            InitializeComponent();
        }

        private void StaffAdd_Load(object sender, EventArgs e)
        {

        }

        public int id = 0;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public override void btnSave_Click(object sender, EventArgs e)
        {
            string qry = "";

            if (id == 0)
            {
                qry = "Insert Into staff Values(@Name, @phone, @role)";

            }
            else
            {
                qry = "Update staff Set staffName = @Name, staffPhone = @phone, staffRole = @role where staffID = @id";
            }

            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
            ht.Add("@phone", txtPhone.Text);
            ht.Add("@role", cbRole.Text);

            if (MainClass.SQL(qry, ht) > 0)
            {
                MessageBox.Show("Saved successfully");
                id = 0;
                txtName.Text = "";

                txtPhone.Text = "";
                cbRole.SelectedIndex = -1;
                txtName.Focus();
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }
    }
}
