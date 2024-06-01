﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Quan_Coffee
{
    public partial class ProductAdd : SampleAddCf
    {
        public ProductAdd()
        {
            InitializeComponent();
        }
        public int id = 0;
        public int cID = 0;

        private void ProductAdd_Load(object sender, EventArgs e)
        {
            // For cb fill

            string qry = "Select catID 'id', catName 'name' from category";
            MainClass.CBFILL(qry, cbCat);

            if(cID > 0)
            {
                cbCat.SelectedValue = cID;
            }

            if(id > 0)
            {
                ForUpdateLoadData(); 
            }
        }


        string filePath;
        Byte[] imageByteArray;

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd =  new OpenFileDialog();
            ofd.Filter = "Images(.jpg. .png)|* .png; *.jpg";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                txtImage.Image = new Bitmap(filePath);
            }
        }

        public override void btnSave_Click(object sender, EventArgs e)
        {
            string qry = "";

            if (id == 0)
            {
                qry = "Insert Into products Values(@Name, @price, @cat, @img)";

            }
            else
            {
                qry = "Update products Set prName = @Name, prPrice = @price, CategoryID = @cat, prImage = @img where prID = @id";
            }


            Image temp = new Bitmap(txtImage.Image);
            MemoryStream ms = new MemoryStream();
            temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            imageByteArray = ms.ToArray();

            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
            ht.Add("@price", txtPrice.Text);
            ht.Add("@cat", Convert.ToInt32(cbCat.SelectedValue));
            ht.Add("@img", imageByteArray);

            if (MainClass.SQL(qry, ht) > 0)
            {
                MessageBox.Show("Saved successfully");
                id = 0;
                cID = 0;
                txtName.Text = "";
                txtPrice.Text = "";
                cbCat.SelectedIndex = 0;
                cbCat.SelectedIndex = -1;
                txtName.Focus();
            }
        }

        private void ForUpdateLoadData()
        {
            string qry = @"Select * from products where prID = "+ id + "";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if(dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["prName"].ToString();
                txtPrice.Text = dt.Rows[0]["prPrice"].ToString();

                Byte[] imageArray = (byte[])(dt.Rows[0]["prImage"]);
                byte[] imageByteArray = imageArray;
                txtImage.Image = Image.FromStream(new MemoryStream(imageArray));
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }

        private void cbCat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
