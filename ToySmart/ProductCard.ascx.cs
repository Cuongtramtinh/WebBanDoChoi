using System;
using MySql.Data.MySqlClient;
using System.Web.UI;

namespace ToySmart
{
    public partial class ProductCard : System.Web.UI.UserControl
    {
        // ==========================================
        // MaSanPham LƯU QUA VIEWSTATE
        // Để giữ đúng giá trị khi bấm nút (postback),
        // vì Repeater không load lại dữ liệu mỗi lần bấm nút.
        // ==========================================
        public int MaSanPham
        {
            get
            {
                return ViewState["MaSanPham"] != null
                    ? (int)ViewState["MaSanPham"]
                    : 0;
            }
            set
            {
                ViewState["MaSanPham"] = value;
            }
        }

        public string TenSanPham { get; set; }

        public string ThuongHieu { get; set; }

        public decimal Gia { get; set; }

        public string HinhAnh { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {   

           
                HienThiSanPham();
                KiemTraDaYeuThich();
            

            // Chưa đăng nhập
            if (Session["MaTaiKhoan"] == null)
            {
                string script = @"
                    var thongBao = document.createElement('div');

                    thongBao.className = 'thong-bao-thanh-cong';

                    thongBao.innerHTML =
                        '<i class=""fa-solid fa-circle-info""></i>' +
                        ' Bạn cần đăng nhập để có thể tiếp tục.';

                    document.body.appendChild(thongBao);

                    setTimeout(function () {
                        thongBao.remove();
                    }, 2000);

                    return false;
                ";

                btnYeuThich.OnClientClick = script;

                btnThemGioHang.OnClientClick = script;
            }
        }


        private void HienThiSanPham()
        {
            lnkHinhAnh.NavigateUrl =
                "ChiTietSanPham.aspx?MaSanPham=" + MaSanPham;

            lnkTenSanPham.Text = TenSanPham;

            lnkTenSanPham.NavigateUrl =
                "ChiTietSanPham.aspx?MaSanPham=" + MaSanPham;

            imgSanPham.ImageUrl = HinhAnh;

            imgSanPham.AlternateText = TenSanPham;

            lblGia.Text = Gia.ToString("N0");

            lblThuongHieu.Text = ThuongHieu;
        }


        // ==========================================
        // KIỂM TRA XEM SẢN PHẨM NÀY ĐÃ ĐƯỢC
        // KHÁCH ĐANG ĐĂNG NHẬP YÊU THÍCH CHƯA
        // ==========================================
        private void KiemTraDaYeuThich()
        {
            if (Session["MaTaiKhoan"] == null)
            {
                return;
            }

            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    string sql = @"
                        SELECT 1
                        FROM yeuthich
                        WHERE MaTaiKhoan = @MaTaiKhoan
                          AND MaSanPham = @MaSanPham
                        LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        cmd.Parameters.AddWithValue("@MaSanPham", MaSanPham);

                        conn.Open();

                        object ketQua = cmd.ExecuteScalar();

                        HienThiTrangThaiYeuThich(ketQua != null);
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }


        // ==========================================
        // ĐỔI GIAO DIỆN NÚT YÊU THÍCH
        // (trái tim đặc màu đỏ khi đã yêu thích,
        //  trái tim viền khi chưa yêu thích)
        // ==========================================
        private void HienThiTrangThaiYeuThich(bool daYeuThich)
        {
            if (daYeuThich)
            {
                btnYeuThich.CssClass = "btn-favorite active";
                btnYeuThich.ToolTip = "Bỏ yêu thích";
            }
            else
            {
                btnYeuThich.CssClass = "btn-favorite";
                btnYeuThich.ToolTip = "Thêm vào yêu thích";
            }

            upProductCard.Update();
        }


        // ==========================================
        // NÚT YÊU THÍCH
        // ==========================================
        protected void btnYeuThich_Click(object sender, EventArgs e)
        {
            if (Session["MaTaiKhoan"] == null)
            {
                return;
            }

            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();

                    // ==========================================
                    // 1. KIỂM TRA ĐÃ YÊU THÍCH CHƯA
                    // ==========================================
                    string sqlKiemTra = @"
                        SELECT 1
                        FROM yeuthich
                        WHERE MaTaiKhoan = @MaTaiKhoan
                          AND MaSanPham = @MaSanPham
                        LIMIT 1";

                    bool daYeuThich;

                    using (MySqlCommand cmdKiemTra =
                        new MySqlCommand(sqlKiemTra, conn))
                    {
                        cmdKiemTra.Parameters.AddWithValue(
                            "@MaTaiKhoan", maTaiKhoan);

                        cmdKiemTra.Parameters.AddWithValue(
                            "@MaSanPham", MaSanPham);

                        daYeuThich = cmdKiemTra.ExecuteScalar() != null;
                    }

                    if (daYeuThich)
                    {
                        // ==========================================
                        // 2A. ĐÃ YÊU THÍCH -> BỎ YÊU THÍCH
                        // ==========================================
                        string sqlXoa = @"
                            DELETE FROM yeuthich
                            WHERE MaTaiKhoan = @MaTaiKhoan
                              AND MaSanPham = @MaSanPham";

                        using (MySqlCommand cmdXoa =
                            new MySqlCommand(sqlXoa, conn))
                        {
                            cmdXoa.Parameters.AddWithValue(
                                "@MaTaiKhoan", maTaiKhoan);

                            cmdXoa.Parameters.AddWithValue(
                                "@MaSanPham", MaSanPham);

                            cmdXoa.ExecuteNonQuery();
                        }

                        HienThiTrangThaiYeuThich(false);
                    }
                    else
                    {
                        // ==========================================
                        // 2B. CHƯA YÊU THÍCH -> THÊM YÊU THÍCH
                        // ==========================================
                        string sqlThem = @"
                            INSERT INTO yeuthich
                            (MaTaiKhoan, MaSanPham)
                            VALUES
                            (@MaTaiKhoan, @MaSanPham)";

                        using (MySqlCommand cmdThem =
                            new MySqlCommand(sqlThem, conn))
                        {
                            cmdThem.Parameters.AddWithValue(
                                "@MaTaiKhoan", maTaiKhoan);

                            cmdThem.Parameters.AddWithValue(
                                "@MaSanPham", MaSanPham);

                            cmdThem.ExecuteNonQuery();
                        }

                        // ==========================================
                        // 3. LƯU HÀNH VI (chỉ lưu khi THÊM yêu thích,
                        //    không lưu khi bỏ yêu thích)
                        // ==========================================
                        string sqlHanhVi = @"
                            INSERT INTO hanhvicuakhachhang
                            (MaTaiKhoan, MaSanPham, LoaiHanhVi)
                            VALUES
                            (@MaTaiKhoan, @MaSanPham, 'YeuThich')";

                        using (MySqlCommand cmdHanhVi =
                            new MySqlCommand(sqlHanhVi, conn))
                        {
                            cmdHanhVi.Parameters.AddWithValue(
                                "@MaTaiKhoan", maTaiKhoan);

                            cmdHanhVi.Parameters.AddWithValue(
                                "@MaSanPham", MaSanPham);

                            cmdHanhVi.ExecuteNonQuery();
                        }

                        HienThiTrangThaiYeuThich(true);
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }






        protected void btnThemGioHang_Click(object sender, EventArgs e)
        {
            if (Session["MaTaiKhoan"] == null)
            {
                return;
            }

            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();

                    // ==========================================
                    // 1. KIỂM TRA TỒN KHO
                    // So sánh tồn kho với số lượng đang có trong giỏ
                    // ==========================================
                    string sqlKiemTra = @"
                SELECT  s.SoLuong,
                        IFNULL(g.SoLuong, 0)
                FROM sanpham s
                LEFT JOIN giohang g
                       ON g.MaSanPham = s.MaSanPham
                      AND g.MaTaiKhoan = @MaTaiKhoan
                WHERE s.MaSanPham = @MaSanPham";

                    int tonKho = 0;
                    int dangCo = 0;

                    using (MySqlCommand cmdKiemTra =
                        new MySqlCommand(sqlKiemTra, conn))
                    {
                        cmdKiemTra.Parameters.AddWithValue(
                            "@MaTaiKhoan", maTaiKhoan);

                        cmdKiemTra.Parameters.AddWithValue(
                            "@MaSanPham", MaSanPham);

                        using (MySqlDataReader reader = cmdKiemTra.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                tonKho = reader.GetInt32(0);
                                dangCo = reader.GetInt32(1);
                            }
                        }
                    }

                    if (dangCo >= tonKho)
                    {
                        HienThiThongBao(
                            "fa-circle-exclamation",
                            "Sản phẩm đã hết hàng hoặc bạn đã lấy tối đa.");

                        return;
                    }

                    // ==========================================
                    // 2. THÊM VÀO GIỎ
                    // Đã có trong giỏ -> cộng dồn số lượng
                    // ==========================================
                    string sqlThem = @"
                INSERT INTO giohang
                (MaTaiKhoan, MaSanPham, SoLuong)
                VALUES
                (@MaTaiKhoan, @MaSanPham, 1)
                ON DUPLICATE KEY UPDATE
                    SoLuong = SoLuong + 1";

                    using (MySqlCommand cmdThem = new MySqlCommand(sqlThem, conn))
                    {
                        cmdThem.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        cmdThem.Parameters.AddWithValue("@MaSanPham", MaSanPham);

                        cmdThem.ExecuteNonQuery();
                    }

                    // ==========================================
                    // 3. LƯU HÀNH VI
                    // ==========================================
                    string sqlHanhVi = @"
                INSERT INTO hanhvicuakhachhang
                (MaTaiKhoan, MaSanPham, LoaiHanhVi)
                VALUES
                (@MaTaiKhoan, @MaSanPham, 'ThemGioHang')";

                    using (MySqlCommand cmdHanhVi = new MySqlCommand(sqlHanhVi, conn))
                    {
                        cmdHanhVi.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        cmdHanhVi.Parameters.AddWithValue("@MaSanPham", MaSanPham);

                        cmdHanhVi.ExecuteNonQuery();
                    }
                }

                HienThiThongBao("fa-circle-check", "Đã thêm vào giỏ hàng.");
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }


        // ==========================================
        // THÔNG BÁO NHỎ GÓC MÀN HÌNH
        // Nút nằm trong UpdatePanel nên phải dùng
        // ScriptManager.RegisterStartupScript, không dùng
        // ClientScript.RegisterStartupScript được.
        // ==========================================
        private void HienThiThongBao(string icon, string noiDung)
        {
            string script = @"
        var thongBao = document.createElement('div');
        thongBao.className = 'thong-bao-thanh-cong';
        thongBao.innerHTML =
            '<i class=""fa-solid " + icon + @"""></i> " + noiDung + @"';
        document.body.appendChild(thongBao);
        setTimeout(function () { thongBao.remove(); }, 2000);
    ";

            ScriptManager.RegisterStartupScript(
                upProductCard,
                upProductCard.GetType(),
                "thongBaoGioHang" + MaSanPham,
                script,
                true);
        }
    }
}