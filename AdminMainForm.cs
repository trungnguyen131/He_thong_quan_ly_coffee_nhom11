using System;
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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }


        // nhúng một form con vào trong một form chính
        // tham số đầu vào: f
        // một đối tượng kiểu:  Form.

        public void AddControls(Form f)
        {
            // Xóa tất cả các control hiện tại trong ControlsPanel.
            // ControlsPanel là một Panel trên form chính
            // Controls là một tập hợp các control con của Panel đó.
            // Clear() đảm bảo rằng ControlsPanel sẽ trống trước khi thêm form mới vào.
            ControlsPanel.Controls.Clear();
            // Dock của form f bằng DockStyle.Fill. Điều này có nghĩa là form f sẽ tự động co giãn để lấp đầy toàn bộ không gian của ControlsPanel.
            f.Dock = DockStyle.Fill;
            // TopLevel xác định xem form này có phải là form cấp cao nhất (top-level) hay không
            // đặt false, form f sẽ không được xem là một cửa sổ độc lập mà sẽ là một control con của ControlsPanel.
            f.TopLevel = false;
            // nhúng form f vào ControlsPanel.
            ControlsPanel.Controls.Add(f);
            // hiển thị form f. Gọi Show() để chắc chắn rằng form con f sẽ hiển thị trong ControlsPanel.
            f.Show();

        }

        // Thoát hoàn toàn khỏi ứng dụng.
        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        // AddControls là phương thức để thêm một form con (như CategoryView) vào ControlsPanel.
        private void button3_Click(object sender, EventArgs e)
        {
            AddControls(new CategoryView());
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            AddControls(new ProductView());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AddControls(new TablesView());
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //_obj = this;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            AddControls(new frmHome());
        }

        private void ControlsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            AddControls(new StaffView());
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            Pos frm = new Pos();
            frm.Show();
        }

        /// object sender: object sender: Tham số này đại diện cho đối tượng kích hoạt sự kiện, trong trường hợp này là btnKitchen. 
        /// EventArgs e: Tham số này chứa các dữ liệu sự kiện.
        // Control là một thành phần giao diện người dùng mà người dùng có thể tương tác hoặc xem.
        // AddControls(): Là phương thức thêm control vào giao diện người dùng khi click 
        // new frmKitchenView(): Đây là một instance mới của lớp, là một đối tượng cụ thể được tạo ra từ lớp đó.
        private void btnKitchen_Click(object sender, EventArgs e)
        {
            AddControls(new frmKitchenView());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Chuyển người dùng về màn hình đăng nhập
            var loginForm = new Form1();
            loginForm.Show();
            /// this trỏ tới form chứa nút được click
            this.Close(); 
        }
    }
}
