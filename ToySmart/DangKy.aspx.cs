using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace ToySmart
{
    public partial class DangKy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // ==========================================
        // NÚT ĐĂNG KÝ
        // ==========================================
        protected void btnDangKy_Click(object sender, EventArgs e)
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

            // ==========================================
            // 3. KIỂM TRA NHẬP LẠI MẬT KHẨU
            // ==========================================
            if (string.IsNullOrWhiteSpace(txtNhapLaiMatKhau.Text))
            {
                errNhapLaiMatKhau.Text = "Không được để trống.";
                txtNhapLaiMatKhau.CssClass = "form-input input-error";
                hopLe = false;
            }

            // ==========================================
            // 4. HỌ VÀ TÊN
            // ==========================================
            if (string.IsNullOrWhiteSpace(txtHoVaTen.Text))
            {
                errHoVaTen.Text = "Không được để trống.";
                txtHoVaTen.CssClass = "form-input input-error";
                hopLe = false;
            }

            // ==========================================
            // 5. EMAIL
            // ==========================================
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                errEmail.Text = "Không được để trống.";
                txtEmail.CssClass = "form-input input-error";
                hopLe = false;
            }

            // ==========================================
            // 6. SỐ ĐIỆN THOẠI
            // ==========================================
            if (string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                errSoDienThoai.Text = "Không được để trống.";
                txtSoDienThoai.CssClass = "form-input input-error";
                hopLe = false;
            }

            // ==========================================
            // 7. QUÊ QUÁN KHÔNG BẮT BUỘC
            // ==========================================

            if (!hopLe)
            {
                return;
            }

            // ==========================================
            // 8. KIỂM TRA MẬT KHẨU NHẬP LẠI
            // ==========================================
            if (txtMatKhau.Text != txtNhapLaiMatKhau.Text)
            {
                errNhapLaiMatKhau.Text =
                    "Mật khẩu nhập lại không khớp.";

                txtNhapLaiMatKhau.CssClass =
                    "form-input input-error";

                return;
            }

            // ==========================================
            // 9. KIỂM TRA EMAIL
            // ==========================================
            string email = txtEmail.Text.Trim();

            string mauEmail =
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(email, mauEmail))
            {
                errEmail.Text =
                    "Email không đúng định dạng.";

                txtEmail.CssClass =
                    "form-input input-error";

                return;
            }

            // ==========================================
            // 10. KIỂM TRA SỐ ĐIỆN THOẠI
            // ==========================================
            string soDienThoai =
                txtSoDienThoai.Text.Trim();

            string mauSoDienThoai =
                @"^0\d{9}$";

            if (!Regex.IsMatch(
                    soDienThoai,
                    mauSoDienThoai))
            {
                errSoDienThoai.Text =
                    "Số điện thoại phải gồm 10 số và bắt đầu bằng 0.";

                txtSoDienThoai.CssClass =
                    "form-input input-error";

                return;
            }

            // ==========================================
            // 11. LẤY DỮ LIỆU
            // ==========================================
            string tenDangNhap =
                txtTenDangNhap.Text.Trim();

            string matKhau =
                txtMatKhau.Text;

            string hoVaTen =
                txtHoVaTen.Text.Trim();

            string queQuan =
                txtQueQuan.Text.Trim();

            // ==========================================
            // 12. KIỂM TRA TRÙNG DỮ LIỆU
            // ==========================================
            using (MySqlConnection conn = DB.GetConnection())
            {
                try
                {
                    conn.Open();

                    string sql = @"
                        SELECT
                            TenDangNhap,
                            Email,
                            SoDienThoai
                        FROM taikhoan
                        WHERE TenDangNhap = @TenDangNhap
                           OR Email = @Email
                           OR SoDienThoai = @SoDienThoai
                        LIMIT 1";

                    using (MySqlCommand cmd =
                        new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@TenDangNhap",
                            tenDangNhap);

                        cmd.Parameters.AddWithValue(
                            "@Email",
                            email);

                        cmd.Parameters.AddWithValue(
                            "@SoDienThoai",
                            soDienThoai);

                        using (MySqlDataReader reader =
                            cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string tenDangNhapDB =
                                    reader["TenDangNhap"].ToString();

                                string emailDB =
                                    reader["Email"].ToString();

                                string soDienThoaiDB =
                                    reader["SoDienThoai"].ToString();

                                if (tenDangNhapDB
                                    .Equals(
                                        tenDangNhap,
                                        StringComparison.OrdinalIgnoreCase))
                                {
                                    errTenDangNhap.Text =
                                        "Tên đăng nhập đã tồn tại.";

                                    txtTenDangNhap.CssClass =
                                        "form-input input-error";

                                    return;
                                }

                                if (emailDB
                                    .Equals(
                                        email,
                                        StringComparison.OrdinalIgnoreCase))
                                {
                                    errEmail.Text =
                                        "Email đã được sử dụng.";

                                    txtEmail.CssClass =
                                        "form-input input-error";

                                    return;
                                }

                                if (soDienThoaiDB
                                    == soDienThoai)
                                {
                                    errSoDienThoai.Text =
                                        "Số điện thoại đã được sử dụng.";

                                    txtSoDienThoai.CssClass =
                                        "form-input input-error";

                                    return;
                                }
                            }
                        }
                    }

                    // ==========================================
                    // 13. MÃ HÓA MẬT KHẨU
                    // ==========================================
                    string matKhauMaHoa =
                        MaHoaMatKhau(matKhau);

                    // ==========================================
                    // 14. INSERT TÀI KHOẢN
                    // ==========================================
                    string sqlInsert = @"
                        INSERT INTO taikhoan
                        (
                            TenDangNhap,
                            MatKhau,
                            HoVaTen,
                            Email,
                            SoDienThoai,
                            QueQuan
                        )
                        VALUES
                        (
                            @TenDangNhap,
                            @MatKhau,
                            @HoVaTen,
                            @Email,
                            @SoDienThoai,
                            @QueQuan
                        )";

                    using (MySqlCommand cmdInsert =
                        new MySqlCommand(sqlInsert, conn))
                    {
                        cmdInsert.Parameters.AddWithValue(
                            "@TenDangNhap",
                            tenDangNhap);

                        cmdInsert.Parameters.AddWithValue(
                            "@MatKhau",
                            matKhauMaHoa);

                        cmdInsert.Parameters.AddWithValue(
                            "@HoVaTen",
                            hoVaTen);

                        cmdInsert.Parameters.AddWithValue(
                            "@Email",
                            email);

                        cmdInsert.Parameters.AddWithValue(
                            "@SoDienThoai",
                            soDienThoai);

                        // Quê quán có thể để trống
                        if (string.IsNullOrWhiteSpace(queQuan))
                        {
                            cmdInsert.Parameters.AddWithValue(
                                "@QueQuan",
                                DBNull.Value);
                        }
                        else
                        {
                            cmdInsert.Parameters.AddWithValue(
                                "@QueQuan",
                                queQuan);
                        }

                        cmdInsert.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    // Lỗi do UNIQUE hoặc Database
                    errTenDangNhap.Text =
                        "Không thể đăng ký tài khoản. Vui lòng kiểm tra lại dữ liệu.";

                    System.Diagnostics.Debug.WriteLine(
                        ex.Message);

                    return;
                }
            }

            // ==========================================
            // 15. ĐĂNG KÝ THÀNH CÔNG
            // ==========================================
            ClientScript.RegisterStartupScript(
                this.GetType(),
                "DangKyThanhCong",
                @"
                var thongBao = document.createElement('div');

                thongBao.className = 'thong-bao-thanh-cong';

                thongBao.innerHTML =
                '<i class=""fa-solid fa-circle-check""></i>' +
                ' Đăng ký tài khoản thành công!';

                document.body.appendChild(thongBao);

                setTimeout(function () {
                window.location.href = 'DangNhap.aspx';
                }, 2000);
                ",
                true
            );
        }

        // ==========================================
        // HÀM MÃ HÓA MẬT KHẨU SHA-256
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
        // XÓA LỖI
        // ==========================================
        private void XoaLoi()
        {
            errTenDangNhap.Text = "";
            errMatKhau.Text = "";
            errNhapLaiMatKhau.Text = "";
            errHoVaTen.Text = "";
            errEmail.Text = "";
            errSoDienThoai.Text = "";
            errQueQuan.Text = "";

            txtTenDangNhap.CssClass =
                "form-input";

            txtMatKhau.CssClass =
                "form-input";

            txtNhapLaiMatKhau.CssClass =
                "form-input";

            txtHoVaTen.CssClass =
                "form-input";

            txtEmail.CssClass =
                "form-input";

            txtSoDienThoai.CssClass =
                "form-input";

            txtQueQuan.CssClass =
                "form-input";
        }
    }
}