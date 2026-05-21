using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLYOTO1
{
    public partial class QLOTO : Form
    {
        // Chuỗi kết nối trực tiếp tới CSDL của bro
        private string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=New Database;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public QLOTO()
        {
            InitializeComponent();
        }

        // Sự kiện chạy khi mở Form lên
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // 1. CHỨC NĂNG TẢI DỮ LIỆU LÊN GRIDVIEW
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaXe, TenXe, HangXe, GiaXe, NamSX FROM Oto";
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        // 2. CHỨC NĂNG THÊM XE
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Oto (MaXe, TenXe, HangXe, GiaXe, NamSX) VALUES (@MaXe, @TenXe, @HangXe, @GiaXe, @NamSX)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Gán trực tiếp giá trị từ các ô nhập liệu vào tham số
                    cmd.Parameters.AddWithValue("@MaXe", textBox1.Text);
                    cmd.Parameters.AddWithValue("@TenXe", textBox2.Text);
                    cmd.Parameters.AddWithValue("@HangXe", textBox5.Text);
                    cmd.Parameters.AddWithValue("@GiaXe", textBox4.Text);
                    cmd.Parameters.AddWithValue("@NamSX", dateTimePicker1.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery(); // Thực thi lệnh SQL
                }
            }
            LoadData(); // Nạp lại bảng để thấy dữ liệu mới thay đổi
        }

        // 3. CHỨC NĂNG SỬA XE
        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Oto SET TenXe = @TenXe, HangXe = @HangXe, GiaXe = @GiaXe, NamSX = @NamSX WHERE MaXe = @MaXe";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaXe", textBox1.Text);
                    cmd.Parameters.AddWithValue("@TenXe", textBox2.Text);
                    cmd.Parameters.AddWithValue("@HangXe", textBox5.Text);
                    cmd.Parameters.AddWithValue("@GiaXe", textBox4.Text);
                    cmd.Parameters.AddWithValue("@NamSX", dateTimePicker1.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
        }

        // 4. CHỨC NĂNG XÓA XE
        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Oto WHERE MaXe = @MaXe";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaXe", textBox1.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
        }

        // 5. CHỨC NĂNG CLICK DÒNG ĐỂ ĐẨY DỮ LIỆU LÊN CÁC TEXTBOX
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["MaXe"].Value?.ToString();
                textBox2.Text = row.Cells["TenXe"].Value?.ToString();
                textBox5.Text = row.Cells["HangXe"].Value?.ToString();
                textBox4.Text = row.Cells["GiaXe"].Value?.ToString();
                if (row.Cells["NamSX"].Value != null && row.Cells["NamSX"].Value != DBNull.Value)
                {
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["NamSX"].Value);
                }
                else
                {
                    dateTimePicker1.Value = DateTime.Now; // Nếu dòng trống thì mặc định hiển thị ngày hôm nay
                }
            }
        }

        // Các hàm sự kiện mặc định trống, giữ lại để không lỗi file designer
        private void label3_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}