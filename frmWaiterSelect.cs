using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Quan_Coffee
{
    public partial class frmWaiterSelect : Form
    {
        public frmWaiterSelect()
        {
            InitializeComponent();
        }

        public string waiterName;

        public void b_Click(object sender, EventArgs e)
        {
            waiterName = (sender as System.Windows.Forms.Button).Text.ToString();
            this.Close();
        }

        public void frmWaiterSelect_Load(object sender, EventArgs e)
        {
            string qry = "Select * from staff where staffRole like 'Waiter' ";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                System.Windows.Forms.Button b = new System.Windows.Forms.Button();
                b.Text = row["staffName"].ToString();
                b.Width = 150;
                b.Height = 50;
                b.BackColor = Color.FromArgb(241, 85, 126);
                flowLayoutPanel1.Controls.Add(b);
                b.Click += new EventHandler(b_Click); 
            }
        }

        

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }
    }
}
