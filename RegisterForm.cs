using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Quan_Ly_Quan_Coffee
{
    public partial class RegisterForm : Form
    {

        SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-T4349CGK\SQLEXPRESS;Initial Catalog=NewCafe;Persist Security Info=True;User ID=sa;Password=sqlsa123;Encrypt=True");
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void register_loginBtn_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();

            this.Hide();
        }

        private void register_showPass_CheckedChanged(object sender, EventArgs e)
        {
            register_password.PasswordChar = register_showPass.Checked ? '\0' : '*';
            register_cPassword.PasswordChar = register_showPass.Checked ? '\0' : '*';
        }

        public bool emptyFields()
        {
            if(register_username.Text == "" || register_password.Text == "" || register_cPassword.Text == "")
            {
                return true;
            } else
            {
                return false;
            }
        }
        string connectStr = @"Data Source=LAPTOP-T4349CGK\SQLEXPRESS;Initial Catalog=NewCafe;Persist Security Info=True;User ID=sa;Password=sqlsa123;Encrypt=True";

        private void button2_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if(connect.State == ConnectionState.Closed)
                {

                    try
                    {
                        connect.Open();

                        string selectUsername = "SELECT * FROM users WHERE username = @usern";

                        using (SqlCommand checkUsername = new SqlCommand(selectUsername, connect))
                        {
                            checkUsername.Parameters.AddWithValue("@usern", register_username.Text.Trim());

                            SqlDataAdapter adapter = new SqlDataAdapter(checkUsername);
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if (table.Rows.Count >= 1)
                            {
                                string usern = register_username.Text.Substring(0, 1).ToUpper() + register_username.Text.Substring(1);
                                MessageBox.Show(usern + "is already taken.", "Error Message" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if(register_password.Text != register_cPassword.Text)
                            {
                                MessageBox.Show("Password does not match", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            else if(register_password.Text.Length < 8)
                            {
                                MessageBox.Show("Invalid password, at least 8 characters are needed", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            } else
                            {
                                string insertData = "INSERT INTO users (username, password, profile_image, role, status, date_reg)" + 
                                      " VALUES(@usern, @pass, @image, @role, @status, @date)";

                                DateTime today = DateTime.Today;

                                using (SqlConnection connection = new SqlConnection(connectStr))
                                {
                                    using (SqlCommand command = new SqlCommand(insertData, connection))
                                    {
                                        command.Parameters.AddWithValue("@usern", register_username.Text.Trim());
                                        command.Parameters.AddWithValue("@pass", register_password.Text.Trim());
                                        command.Parameters.AddWithValue("@image", "");
                                        command.Parameters.AddWithValue("@role", "Cashier");
                                        command.Parameters.AddWithValue("@status", "Approval");
                                        command.Parameters.AddWithValue("@date", today);

                                        try
                                        {
                                            connection.Open();
                                            int result = command.ExecuteNonQuery();

                                            // Kiểm tra xem dữ liệu đã được lưu thành công chưa
                                            if (result > 0)
                                            {
                                                MessageBox.Show("Registration successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Registration failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                                //using (SqlCommand cmd = new SqlCommand(insertData, connect))
                                //{
                                //    cmd.Parameters.AddWithValue("@usern", register_username.Text.Trim());
                                //    cmd.Parameters.AddWithValue("@pass", register_password.Text.Trim());
                                //    cmd.Parameters.AddWithValue("@image", "");
                                //    cmd.Parameters.AddWithValue("@role", "Cashier");
                                //    cmd.Parameters.AddWithValue("@status", "Approval");
                                //    cmd.Parameters.AddWithValue("@date", today);

                                //    cmd.ExecuteNonQuery();

                                //    MessageBox.Show("Register successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                //    // SWITCH FORM INTO LOGIN FORM
                                //    Form1 loginForm = new Form1();
                                //    loginForm.Show();

                                //    this.Hide();

                                //}

                            }
                        }

                    } catch(Exception ex)
                    {
                        MessageBox.Show("Connection failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    finally
                    {
                        connect.Close();
                    }
                }

           
            }
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
