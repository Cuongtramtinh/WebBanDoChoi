using System;
using System.Data;
using System.Web.UI;
using MySql.Data.MySqlClient;

namespace ToySmart
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HienThiTrangThaiTaiKhoan();
            HienThiGioHang();
        }

        private void HienThiTrangThaiTaiKhoan()
        {
            if (Session["MaTaiKhoan"] == null)
            {
                pnlChuaDangNhap.Visible = true;
                pnlDaDangNhap.Visible = false;
            }
            else
            {
                pnlChuaDangNhap.Visible = false;
                pnlDaDangNhap.Visible = true;

                lblHoVaTen.Text = Session["HoVaTen"].ToString();
            }
        }


        // ==========================================
        // HIỂN THỊ DROPDOWN GIỎ HÀNG TRÊN HEADER
        // ==========================================
        private void HienThiGioHang()
        {
            if (Session["MaTaiKhoan"] == null)
            {
                pnlGioHangChuaDangNhap.Visible = true;
                pnlGioHangDaDangNhap.Visible = false;

                lblSoLuongGioHang.Text = "0";
                return;
            }

            pnlGioHangChuaDangNhap.Visible = false;
            pnlGioHangDaDangNhap.Visible = true;

            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);

            DataTable bang = new DataTable();

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    string sql = @"
                        SELECT  s.TenSanPham,
                                s.HinhAnh,
                                s.Gia,
                                g.SoLuong
                        FROM giohang g
                        INNER JOIN sanpham s
                                ON s.MaSanPham = g.MaSanPham
                        WHERE g.MaTaiKhoan = @MaTaiKhoan
                        ORDER BY s.TenSanPham
                        LIMIT 5";

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

            rptGioHangMini.DataSource = bang;
            rptGioHangMini.DataBind();

            pnlGioHangTrongMini.Visible = bang.Rows.Count == 0;

            // Đếm tổng số lượng (không giới hạn LIMIT 5 như trên)
            lblSoLuongGioHang.Text = DemSoLuongGioHang(maTaiKhoan).ToString();
        }


        private int DemSoLuongGioHang(int maTaiKhoan)
        {
            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    string sql = @"
                        SELECT IFNULL(SUM(SoLuong), 0)
                        FROM giohang
                        WHERE MaTaiKhoan = @MaTaiKhoan";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

                        conn.Open();

                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return 0;
            }
        }


        protected void btnDangXuat_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("TrangChu.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                return;
            }

            Response.Redirect(
                "TrangTimKiem.aspx?TuKhoa=" +
                Server.UrlEncode(tuKhoa));
        }
    }
}