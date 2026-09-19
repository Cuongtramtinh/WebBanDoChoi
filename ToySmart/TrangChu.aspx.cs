using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ToySmart
{
    public partial class TrangChu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDanhMuc();
                LoadSanPham();
            }
        }

        // =========================
        // LẤY DANH MỤC
        // =========================
        private void LoadDanhMuc()
        {
            using (MySqlConnection conn = DB.GetConnection())
            {
                string sql = @"
                    SELECT MaDanhMuc, TenDanhMuc
                    FROM DanhMuc
                    ORDER BY MaDanhMuc";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    conn.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        rptDanhMuc.DataSource = reader;
                        rptDanhMuc.DataBind();
                    }
                }
            }
        }

        // =========================
        // LẤY SẢN PHẨM
        // =========================
        private void LoadSanPham()
        {
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
                    ORDER BY MaSanPham DESC";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    conn.Open();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Hiện tạm sản phẩm mới nhất
                        // Sau này sẽ thay bằng truy vấn khuyến mãi
                        // và bán chạy riêng.
                        rptKhuyenMai.DataSource = dt;
                        rptKhuyenMai.DataBind();

                        rptBanChay.DataSource = dt;
                        rptBanChay.DataBind();
                    }
                }
            }
        }

        // =========================
        // GÁN DỮ LIỆU CHO PRODUCT CARD - KHUYẾN MÃI
        // =========================
        protected void rptKhuyenMai_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ProductCard card =
                    (ProductCard)e.Item.FindControl("ProductCard1");

                DataRowView row = (DataRowView)e.Item.DataItem;

                card.MaSanPham = Convert.ToInt32(row["MaSanPham"]);
                card.TenSanPham = row["TenSanPham"].ToString();
                card.ThuongHieu = row["ThuongHieu"].ToString();
                card.Gia = Convert.ToDecimal(row["Gia"]);
                card.HinhAnh = row["HinhAnh"].ToString();
            }
        }

        // =========================
        // GÁN DỮ LIỆU CHO PRODUCT CARD - BÁN CHẠY
        // =========================
        protected void rptBanChay_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ProductCard card =
                    (ProductCard)e.Item.FindControl("ProductCard2");

                DataRowView row = (DataRowView)e.Item.DataItem;

                card.MaSanPham = Convert.ToInt32(row["MaSanPham"]);
                card.TenSanPham = row["TenSanPham"].ToString();
                card.ThuongHieu = row["ThuongHieu"].ToString();
                card.Gia = Convert.ToDecimal(row["Gia"]);
                card.HinhAnh = row["HinhAnh"].ToString();
            }
        }
    }
}