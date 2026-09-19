using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace ToySmart
{
    public partial class DangNhap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // ==========================================
        // NÚT ĐĂNG NHẬP
        // ==========================================
        protected void btnDangNhap_Click(object sender, EventArgs e)
        {
            XoaLoi();

            bool hopLe = true;

            // ==========================================
            // 1. KIỂM TRA TÊN ĐĂNG NHẬP
            // ==========================================
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                errTenDangNhap.Text = "Không được để trống.";
                txtTenDangNhap.CssClass = "form-input input-error";
                hopLe = false;
            }

            // ==========================================
            // 2. KIỂM TRA MẬT KHẨU
            // ==========================================
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                errMatKhau.Text = "Không được để trống.";
                txtMatKhau.CssClass = "form-input input-error";
                hopLe = false;
            }

            if (!hopLe)
            {
                return;
            }

            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;

            // ==========================================
            // 3. MÃ HÓA MẬT KHẨU NGƯỜI DÙNG NHẬP
            // ==========================================
            string matKhauMaHoa = MaHoaMatKhau(matKhau);

            // ==========================================
            // 4. KIỂM TRA DATABASE
            // ==========================================
            using (MySqlConnection conn = DB.GetConnection())
            {
                try
                {
                    conn.Open();

                    string sql = @"
                        SELECT
                            MaTaiKhoan,
                            TenDangNhap,
                            MatKhau,
                            HoVaTen,
                            VaiTro,
                            TrangThaiTaiKhoan
                        FROM taikhoan
                        WHERE TenDangNhap = @TenDangNhap
                        LIMIT 1";

                    using (MySqlCommand cmd =
                        new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@TenDangNhap",
                            tenDangNhap);

                        using (MySqlDataReader reader =
                            cmd.ExecuteReader())
                        {
                            // Không tìm thấy tài khoản
                            if (!reader.Read())
                            {
                                errTenDangNhap.Text =
                                    "Tên đăng nhập hoặc mật khẩu không chính xác.";

                                txtTenDangNhap.CssClass =
                                    "form-input input-error";

                                txtMatKhau.CssClass =
                                    "form-input input-error";

                                return;
                            }

                            string matKhauDB =
                                reader["MatKhau"].ToString();

                            string trangThai =
                                reader["TrangThaiTaiKhoan"].ToString();

                            // ==========================================
                            // 5. KIỂM TRA MẬT KHẨU
                            // ==========================================
                            if (matKhauMaHoa != matKhauDB)
                            {
                                errTenDangNhap.Text =
                                    "Tên đăng nhập hoặc mật khẩu không chính xác.";

                                txtTenDangNhap.CssClass =
                                    "form-input input-error";

                                txtMatKhau.CssClass =
                                    "form-input input-error";

                                return;
                            }

                            // ==========================================
                            // 6. KIỂM TRA TRẠNG THÁI TÀI KHOẢN
                            // ==========================================
                            if (trangThai != "HoatDong")
                            {
                                errTenDangNhap.Text =
                                    "Tài khoản hiện không hoạt động.";

                                txtTenDangNhap.CssClass =
                                    "form-input input-error";

                                return;
                            }

                            // ==========================================
                            // 7. LƯU THÔNG TIN VÀO SESSION
                            // ==========================================
                            Session["MaTaiKhoan"] =
                                Convert.ToInt32(
                                    reader["MaTaiKhoan"]);

                            Session["TenDangNhap"] =
                                reader["TenDangNhap"].ToString();

                            Session["HoVaTen"] =
                                reader["HoVaTen"].ToString();

                            Session["VaiTro"] =
                                Convert.ToInt32(
                                    reader["VaiTro"]);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine(
                        ex.Message);

                    errTenDangNhap.Text =
                        "Không thể kết nối đến cơ sở dữ liệu.";

                    return;
                }
            }

            // ==========================================
            // 8. ĐĂNG NHẬP THÀNH CÔNG
            // ==========================================
            ClientScript.RegisterStartupScript(
                this.GetType(),
                "DangNhapThanhCong",
                @"
                var thongBao = document.createElement('div');

                thongBao.className = 'thong-bao-thanh-cong';

                thongBao.innerHTML =
                    '<i class=""fa-solid fa-circle-check""></i>' +
                    ' Đăng nhập thành công!';

                document.body.appendChild(thongBao);

                setTimeout(function () {
                    window.location.href = 'TrangChu.aspx';
                }, 2000);
                ",
                true
            );
        }

        // ==========================================
        // MÃ HÓA MẬT KHẨU SHA-256
        // ==========================================
        private string MaHoaMatKhau(string matKhau)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes =
                    Encoding.UTF8.GetBytes(matKhau);

                byte[] hash =
                    sha256.ComputeHash(bytes);

                StringBuilder builder =
                    new StringBuilder();

                foreach (byte b in hash)
                {
                    builder.Append(
                        b.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        // ==========================================
        // XÓA LỖI CŨ
        // ==========================================
        private void XoaLoi()
        {
            errTenDangNhap.Text = "";
            errMatKhau.Text = "";

            txtTenDangNhap.CssClass =
                "form-input";

            txtMatKhau.CssClass =
                "form-input";
        }
    }
}