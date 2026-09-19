using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ToySmart
{
    public partial class TrangTimKiem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TimKiemSanPham();
            }
        }

        private void TimKiemSanPham()
        {
            string tuKhoa = Request.QueryString["TuKhoa"];

            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                lblTuKhoa.Text = "Vui lòng nhập từ khóa tìm kiếm.";
                return;
            }

            tuKhoa = tuKhoa.Trim();

            lblTuKhoa.Text =
                "Từ khóa: \"" + Server.HtmlEncode(tuKhoa) + "\"";
            LuuHanhViTimKiem(tuKhoa);
            using (MySqlConnection conn = DB.GetConnection())
            {
                string sql = @"
                    SELECT
                        MaSanPham,
                        TenSanPham,
                        ThuongHieu,
                        Gia,
                        HinhAnh
                    FROM SanPham
                    WHERE TrangThaiSanPham = 'DangBan'
                      AND (
                            TenSanPham LIKE @TuKhoa
                            OR ThuongHieu LIKE @TuKhoa
                            OR MoTa LIKE @TuKhoa
                          )
                    ORDER BY MaSanPham DESC";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@TuKhoa",
                        "%" + tuKhoa + "%");

                    conn.Open();

                    using (MySqlDataAdapter adapter =
                           new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();

                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            lblThongBao.Text =
                                "Không tìm thấy sản phẩm phù hợp.";

                            rptKetQuaTimKiem.DataSource = null;
                            rptKetQuaTimKiem.DataBind();

                            return;
                        }

                        lblThongBao.Text =
                            "Tìm thấy " + dt.Rows.Count +
                            " sản phẩm phù hợp.";

                        rptKetQuaTimKiem.DataSource = dt;
                        rptKetQuaTimKiem.DataBind();
                    }
                }
            }
        }
        protected void rptKetQuaTimKiem_ItemDataBound(
    object sender,
    RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ProductCard card =
                    (ProductCard)e.Item.FindControl("ProductCard1");

                DataRowView row =
                    (DataRowView)e.Item.DataItem;

                card.MaSanPham =
                    Convert.ToInt32(row["MaSanPham"]);

                card.TenSanPham =
                    row["TenSanPham"].ToString();

                card.ThuongHieu =
                    row["ThuongHieu"].ToString();

                card.Gia =
                    Convert.ToDecimal(row["Gia"]);

                card.HinhAnh =
                    row["HinhAnh"].ToString();
            }
        }

        private void LuuHanhViTimKiem(string tuKhoa)
        {
            // Chưa đăng nhập thì không lưu
            if (Session["MaTaiKhoan"] == null)
            {
                return;
            }

            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);

            using (MySqlConnection conn = DB.GetConnection())
            {
                string sql = @"
            INSERT INTO hanhvicuakhachhang
            (MaTaiKhoan, LoaiHanhVi, TuKhoa)
            VALUES
            (@MaTaiKhoan, 'TimKiem', @TuKhoa)";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                    cmd.Parameters.AddWithValue("@TuKhoa", tuKhoa);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}