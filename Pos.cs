using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Quan_Coffee
{
    
    public partial class Pos : Form
    {
        public Pos()
        {
            InitializeComponent();
        }



        public int MainID = 0;
        public string OrderType;
        
        
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void Pos_Load(object sender, EventArgs e)
        {
            dataGridView.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();

            ProductPanel.Controls.Clear();
            LoadProducts();
        }

        private void AddCategory()
        {
            string qry = "Select * from Category";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            cmd.CommandTimeout = 150;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            CategoryPanel.Controls.Clear();

            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    System.Windows.Forms.Button b = new System.Windows.Forms.Button();
                    b.BackColor = Color.FromArgb(50, 55, 89);

                    b.Size = new Size(134, 45);
                    b.Text = row["catName"].ToString();
                    b.ForeColor = Color.FromArgb(255, 255, 255);

                    b.Click += new EventHandler(b_Click);

                    CategoryPanel.Controls.Add(b);
                }
            }

        }

        private void b_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button b = (System.Windows.Forms.Button)sender;

            if(b.Text == "All Categories")
            {
                txtSearch.Text = "1";
                txtSearch.Text = "";
                return;
            }

            foreach (var item in ProductPanel.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PCategory.ToLower().Contains(b.Text.Trim().ToLower());
            }
        }

        private void AddItems(string id, String proID, string name, string cat, string price, Image pimage)
        {
            var w = new ucProduct()
            {
                PName = name,
                PPrice = price,
                PCategory = cat,
                PImage = pimage,
                id = Convert.ToInt32(proID)
            };

            ProductPanel.Controls.Add(w);

            w.onSelect += (ss, ee) =>
            {
                var wdg = (ucProduct)ss;
                foreach (DataGridViewRow item in dataGridView.Rows)
                {
                    if (Convert.ToInt32(item.Cells["dgvproID"].Value) == wdg.id)
                    {
                        item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;
                        item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) * 
                                                        double.Parse(item.Cells["dgvPrice"].Value.ToString());

                        return;
                    }


                }
                dataGridView.Rows.Add(new object[] {0, 0, wdg.id, wdg.PName, 1, wdg.PPrice, wdg.PPrice });
                GetTotal();
            };
        }

        private void LoadProducts()
        {
            string qry = "Select * from products inner join category on catID = CategoryID";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            cmd.CommandTimeout = 150;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {

                Byte[] imagearray = (byte[])item["prImage"];
                byte[] imagebytearray = imagearray;

                AddItems("0", item["prID"].ToString(), item["prName"].ToString(), item["catName"].ToString(), 
                        item["prPrice"].ToString(), Image.FromStream(new MemoryStream(imagearray)));
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in ProductPanel.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PName.ToLower().Contains(txtSearch.Text.Trim().ToLower());
            }
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach(DataGridViewRow row in dataGridView.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void GetTotal()
        {
            double tot = 0;
            lblTotal.Text = "";
            foreach (DataGridViewRow item in dataGridView.Rows)
            {
                tot += double.Parse(item.Cells["dgvAmount"].Value.ToString());
            }

            lblTotal.Text = tot.ToString("N2");
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            dataGridView.Rows.Clear();
            MainID = 0;
            lblTotal.Text = "0.00";
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderType = "Delivery";

        }

        private void btnTake_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderType = "Take Away";
        }

        private void btnDln_Click(object sender, EventArgs e)
        {

            frmTableSelect frm = new frmTableSelect();
            frm.ShowDialog();
            if (frm.TableName != "")
            {
                lblTable.Visible = true;
                lblTable.Text = frm.TableName;
            }
            else
            {
                lblTable.Visible = false;
                lblTable.Text = "";

            }

            frmWaiterSelect frm2 = new frmWaiterSelect();
            frm2.ShowDialog();
            
            if (frm2.waiterName != "")
            {
                lblWaiter.Visible = true;
                lblWaiter.Text = frm2.waiterName;
            }
            else
            {

                lblWaiter.Visible = false;
                lblWaiter.Text = "";
            }
        }

        private void lblTable_Click(object sender, EventArgs e)
        {

        }

        private void btnKot_Click(object sender, EventArgs e)
        {
            OrderType = "Din ln";
            string qry1 = "";
            string qry2 = "";

            int detailID = 0;

            if(MainID == 0)
            {
                qry1 = @"Insert into tblMain Values(@aDate,@aTime,@TableName,@WaiterName,@status,
                        @orderType,@total,@received,@change);
                Select SCOPE_IDENTITY()";

            } else
            {
                qry1 = @"Update tblMain Set status = @status,total = @total,received = @received,change = @change where MainID = @ID)";

            }

            ////Hashtable ht = new Hashtable();

            SqlCommand cmd = new SqlCommand(qry1, MainClass.con);
            cmd.Parameters.AddWithValue("@ID", MainID);
            cmd.Parameters.AddWithValue("@aDate",Convert.ToDateTime(DateTime.Now.Date));
            cmd.Parameters.AddWithValue("@aTime",DateTime.Now.ToShortTimeString());
            cmd.Parameters.AddWithValue("@TableName", lblTable.Text);
            cmd.Parameters.AddWithValue("@WaiterName", lblWaiter.Text);
            cmd.Parameters.AddWithValue("@status", "Pending");
            cmd.Parameters.AddWithValue("@orderType", OrderType);
            cmd.Parameters.AddWithValue("@total", Convert.ToDouble(lblTotal.Text));
            cmd.Parameters.AddWithValue("@received", Convert.ToDouble(0));
            cmd.Parameters.AddWithValue("@change", Convert.ToDouble(0));

            if(MainClass.con.State == ConnectionState.Closed)
            {
                MainClass.con.Open();
            }
            if (MainID == 0) {MainID = Convert.ToInt32(cmd.ExecuteScalar()); }
            else {  cmd.ExecuteNonQuery(); }
            if (MainClass.con.State == ConnectionState.Open)
            {
                MainClass.con.Close();
            }

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                detailID = Convert.ToInt32(row.Cells["dgvid"].Value);

                 if(detailID == 0)
                {
                    qry2 = @"Insert into tblDetails Values(@MainID,@proID,@qty,@price,@amount)";
                }

                else
                {
                    qry2 = @"Update tblDetails Set proID = @proID, qty = @qty, price = @price, amount = @amount
                                where DetailID = @ID";
                    

                }

                SqlCommand cmd2 = new SqlCommand(qry2, MainClass.con);
                cmd2.Parameters.AddWithValue("@ID", detailID);
                cmd2.Parameters.AddWithValue("@MainID", MainID);
                cmd2.Parameters.AddWithValue("@proID",Convert.ToInt32(row.Cells["dgvproID"].Value));
                cmd2.Parameters.AddWithValue("@qty", Convert.ToInt32(row.Cells["dgvQty"].Value));
                cmd2.Parameters.AddWithValue("@price", Convert.ToInt32(row.Cells["dgvPrice"].Value));
                cmd2.Parameters.AddWithValue("@amount", Convert.ToInt32(row.Cells["dgvAmount"].Value));

                if (MainClass.con.State == ConnectionState.Closed)
                {
                    MainClass.con.Open();
                }
                cmd2.ExecuteNonQuery();
                if (MainClass.con.State == ConnectionState.Open)
                {
                    MainClass.con.Close();
                }

                MessageBox.Show("Saved Successfully");
                MainID = 0;
                detailID = 0;
                dataGridView.Rows.Clear();
                lblTable.Text = "";
                lblWaiter.Text = "";
                lblTable.Visible = false;
                lblWaiter.Visible = false;
                lblTotal.Text = "0.00";
                
            }

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public int id = 0;

        private void btnBill_Click(object sender, EventArgs e)
        {
            frmBillList frm = new frmBillList();
            frm.Show();

            if (frm.MainID > 0)
            {
                id = frm.MainID;
                LoadEntries();
            }
        }

        private void LoadEntries()
        {
            string qry = @"Select * from tblMain m 
                                    inner join tblDetails d on m.MainID = d.MainID
                                    inner join products p on p.prID = d.proID
                                    Where m.MainID = " + id + "";
            SqlCommand cmd2 = new SqlCommand(qry, MainClass.con);
            cmd2.CommandTimeout = 150;
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);

            dataGridView.Rows.Clear();

            foreach (DataRow item in dt2.Rows)
            {
                string detailid = item["DetailID"].ToString();
                string proName = item["prName"].ToString();
                
                string proid = item["proID"].ToString();
                string qty = item["qty"].ToString();
                string price = item["price"].ToString();
                string amount = item["amount"].ToString();

                object[] obj = { 0, detailid, proid,proName ,qty, price, amount };
                dataGridView.Rows.Add(obj);
            }
        }
    }



}