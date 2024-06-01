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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Collections;

namespace Quan_Ly_Quan_Coffee
{
    public partial class Form1 : Form
    {
        // SqlConnection là một lớp trong namespace
        // SqlConnection để thiết lập kết nối tới một cơ sở dữ liệu SQL Server.
        // Cung cấp các thông tin cụ thể để kết nối tới cơ sở dữ liệu.
        SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-T4349CGK\SQLEXPRESS;Initial Catalog=NewCafe;Persist Security Info=True;User ID=sa;Password=sqlsa123;Encrypt=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void login_registerBtn_Click(object sender, EventArgs e)
        {
            RegisterForm regForm = new RegisterForm();
            regForm.ShowDialog();
            this.Hide();
        }

        // phương thức công cộng (public) trả về một giá trị kiểu boolean (bool).
        public bool emptyFields()
        {
            // lấy giá trị của trường dữ liệu tên người dùng.
            // lấy giá trị của trường dữ liệu mật khẩu.
            if (login_username.Text == "" || login_password.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        string connectStr = @"Data Source=LAPTOP-T4349CGK\SQLEXPRESS;Initial Catalog=NewCafe;Persist Security Info=True;User ID=sa;Password=sqlsa123;Encrypt=True";
        private void login_btn_Click(object sender, EventArgs e)
        {
            // Nếu rỗng 
            if(emptyFields())
            {
                // hộp thoại thông báo lỗi sẽ được hiển thị
                // một nút "OK" để đóng hộp thoại.
                MessageBox.Show("All fields are required to be filled.", "Error Message", MessageBoxButtons.OK);

            } else
            {
                // kiểm tra xem kết nối đến cơ sở dữ liệu có đang đóng không. Nếu kết nối đã đóng, tiến hành mở kết nối.
                if (connect.State == ConnectionState.Closed) {
                    try
                    {
                        connect.Open();

                        string selectAccount = "SELECT COUNT(*) FROM users WHERE username = @usern AND password = @pass AND profile_image = @image AND role = @role AND status = 'Approval' AND date_reg = @date ";

                        using (SqlConnection connection = new SqlConnection(connectStr))
                        {
                            using (SqlCommand command = new SqlCommand(selectAccount, connection))
                            {
                                DateTime today = DateTime.Today;
                                command.Parameters.AddWithValue("@usern", login_username.Text.Trim());
                                command.Parameters.AddWithValue("@pass", login_password.Text.Trim());
                                command.Parameters.AddWithValue("@image", "");
                                command.Parameters.AddWithValue("@role", "Cashier");
                                command.Parameters.AddWithValue("@status", "Active");
                                command.Parameters.AddWithValue("@date", today);

                                try
                                {
                                    connection.Open();
                                    object result = command.ExecuteScalar();

                                    if (result != null && Convert.ToInt32(result) > 0)
                                    {
                                        // Đăng nhập thành công
                                        MessageBox.Show("Login successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        // Chuyển sang form chính
                                        frmMain mainForm = new frmMain();
                                        mainForm.Show();
                                        this.Hide();
                                    }
                                    else
                                    {
                                        // Đăng nhập thất bại
                                        MessageBox.Show("Incorrect Username/Password or the account is not Active.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        //using(SqlCommand cmd = new SqlCommand(selectAccount, connect))
                        //{
                        //    DateTime today = DateTime.Today;
                        //    cmd.Parameters.AddWithValue("@usern", login_username.Text.Trim());
                        //    cmd.Parameters.AddWithValue("@pass", login_password.Text.Trim());
                        //    cmd.Parameters.AddWithValue("@image", "");
                        //    cmd.Parameters.AddWithValue("@role", "Cashier");
                        //    cmd.Parameters.AddWithValue("@status", "Active");
                        //    cmd.Parameters.AddWithValue("@date", today);


                        //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        //    DataTable table = new DataTable();

                        //    adapter.Fill(table);


                        //    object userCount = cmd.ExecuteScalar();
                        //    if (userCount != null && Convert.ToInt32(userCount) > 0)
                        //    {
                        //        MessageBox.Show("Login successfully! ", "Infomation Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //        frmMain adminForm = new frmMain();
                        //        adminForm.Show();

                        //        this.Hide();
                        //    } 
                        //    else
                        //    {
                        //        MessageBox.Show("Incorrect Username/Password or there's no Admin's approval.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        //    }

                        //}
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connect failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    finally
                    {
                        connect.Close();
                    }

                }
                
            }
        }

        private void login_showPass_CheckedChanged(object sender, EventArgs e)
        {
            login_password.PasswordChar = login_showPass.Checked ? '\0' : '*';
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
