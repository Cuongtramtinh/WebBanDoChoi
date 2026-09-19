using System;
using System.Data;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace ToySmart
{
    public partial class GioHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Bắt buộc đăng nhập mới xem được giỏ hàng
            if (Session["MaTaiKhoan"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }

            if (!IsPostBack)
            {
                TaiGioHang();
            }
        }


        // ==========================================
        // LẤY DANH SÁCH SẢN PHẨM TRONG GIỎ
        // ==========================================
        private void TaiGioHang()
        {
            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);

            DataTable bang = new DataTable();

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    string sql = @"
                        SELECT  g.MaSanPham,
                                s.TenSanPham,
                                s.HinhAnh,
                                s.Gia,
                                s.SoLuong AS TonKho,
                                g.SoLuong,
                                (s.Gia * g.SoLuong) AS ThanhTien
                        FROM giohang g
                        INNER JOIN sanpham s
                                ON s.MaSanPham = g.MaSanPham
                        WHERE g.MaTaiKhoan = @MaTaiKhoan
                        ORDER BY s.TenSanPham";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

                        conn.Open();

                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(bang);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            rptGioHang.DataSource = bang;
            rptGioHang.DataBind();

            // Tính tổng tiền
            decimal tongTien = 0;

            foreach (DataRow dong in bang.Rows)
            {
                tongTien += Convert.ToDecimal(dong["ThanhTien"]);
            }

            lblTongTien.Text = tongTien.ToString("N0");

            bool coHang = bang.Rows.Count > 0;

            pnlTongTien.Visible = coHang;
            pnlGioHangTrong.Visible = !coHang;
        }


        // ==========================================
        // XỬ LÝ NÚT TĂNG / GIẢM / XÓA
        // ==========================================
        protected void rptGioHang_ItemCommand(
            object source, RepeaterCommandEventArgs e)
        {
            int maSanPham = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Tang")
            {
                CapNhatSoLuong(maSanPham, 1);
            }
            else if (e.CommandName == "Giam")
            {
                CapNhatSoLuong(maSanPham, -1);
            }
            else if (e.CommandName == "Xoa")
            {
                XoaSanPham(maSanPham);
            }

            TaiGioHang();
        }


        // ==========================================
        // CỘNG / TRỪ SỐ LƯỢNG
        // Tăng: không vượt quá tồn kho.
        // Giảm: về 0 thì xóa khỏi giỏ.
        // ==========================================
        private void CapNhatSoLuong(int maSanPham, int thayDoi)
        {
            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        UPDATE giohang g
                        INNER JOIN sanpham s
                                ON s.MaSanPham = g.MaSanPham
                        SET g.SoLuong = g.SoLuong + @ThayDoi
                        WHERE g.MaTaiKhoan = @MaTaiKhoan
                          AND g.MaSanPham = @MaSanPham
                          AND g.SoLuong + @ThayDoi <= s.SoLuong";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ThayDoi", thayDoi);
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);

                        cmd.ExecuteNonQuery();
                    }

                    string sqlDonDep = @"
                        DELETE FROM giohang
                        WHERE MaTaiKhoan = @MaTaiKhoan
                          AND MaSanPham = @MaSanPham
                          AND SoLuong <= 0";

                    using (MySqlCommand cmdDonDep =
                        new MySqlCommand(sqlDonDep, conn))
                    {
                        cmdDonDep.Parameters.AddWithValue(
                            "@MaTaiKhoan", maTaiKhoan);

                        cmdDonDep.Parameters.AddWithValue(
                            "@MaSanPham", maSanPham);

                        cmdDonDep.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }


        private void XoaSanPham(int maSanPham)
        {
            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    string sql = @"
                        DELETE FROM giohang
                        WHERE MaTaiKhoan = @MaTaiKhoan
                          AND MaSanPham = @MaSanPham";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }


        protected void btnThanhToan_Click(object sender, EventArgs e)
        {
            Response.Redirect("ThanhToan.aspx");
        }
    }
}