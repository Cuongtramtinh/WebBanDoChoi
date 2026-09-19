<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrangChu.aspx.cs" Inherits="ToySmart.TrangChu" MaintainScrollPositionOnPostBack="true" %>
<%@ Register Src="~/ProductCard.ascx" TagPrefix="uc" TagName="ProductCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/TrangChu.css" rel="stylesheet" />
    
</asp:Content>









<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- ================= BANNER ================= -->
    <section class="home-banner">

        <div class="banner-content">

            <span class="banner-label">
                CUONG TRAM TINH
            </span>

            <h1>
                Khám phá thế giới<br />
                đồ chơi dành cho bé
            </h1>

            <p>
                Đồ chơi phù hợp với từng độ tuổi,
                sở thích và quá trình phát triển của bé.
            </p>

            <a href="SanPham.aspx" class="banner-button">
                Khám phá ngay
                <i class="fa-solid fa-arrow-right"></i>
            </a>

        </div>

        <div class="banner-image">

            <div class="toy-circle">

                <div class="toy-item toy-1">🧸</div>
                <div class="toy-item toy-2">🚗</div>
                <div class="toy-item toy-3">🧩</div>
                <div class="toy-item toy-4">🎨</div>

            </div>

        </div>

    </section>


    <!-- ================= DANH MỤC ================= -->
    <section class="home-section">

        <div class="section-heading">

            <div>
                <span class="section-label">
                    KHÁM PHÁ
                </span>

                <h2>
                    Danh mục nổi bật
                </h2>
            </div>

            <a href="SanPham.aspx" class="view-all">
                Xem tất cả
                <i class="fa-solid fa-angle-right"></i>
            </a>

        </div>

        <div class="category-grid">

            <!-- Sau này dữ liệu sẽ lấy từ bảng DanhMuc -->

            <asp:Repeater ID="rptDanhMuc" runat="server">

                <ItemTemplate>

                    <a href='<%# "SanPham.aspx?MaDanhMuc=" + Eval("MaDanhMuc") %>'
                       class="category-card">

                        <div class="category-icon">
                            <i class="fa-solid fa-puzzle-piece"></i>
                        </div>

                        <h3>
                            <%# Eval("TenDanhMuc") %>
                        </h3>

                    </a>

                </ItemTemplate>

            </asp:Repeater>

        </div>

    </section>


    <!-- ================= ĐỘ TUỔI ================= -->
    <section class="age-section">

        <div class="age-heading">

            <span class="section-label">
                DÀNH CHO BÉ
            </span>

            <h2>
                Chọn đồ chơi theo độ tuổi
            </h2>

            <p>
                Tìm sản phẩm phù hợp với từng giai đoạn phát triển của bé.
            </p>

        </div>


        <div class="age-grid">

            <a href="SanPham.aspx?Tuoi=0-2"
               class="age-card">

                <span class="age-number">
                    0–2
                </span>

                <div>
                    <h3>Bé 0–2 tuổi</h3>
                    <p>Khám phá thế giới</p>
                </div>

                <i class="fa-solid fa-arrow-right"></i>

            </a>


            <a href="SanPham.aspx?Tuoi=3-5"
               class="age-card">

                <span class="age-number">
                    3–5
                </span>

                <div>
                    <h3>Bé 3–5 tuổi</h3>
                    <p>Học mà chơi</p>
                </div>

                <i class="fa-solid fa-arrow-right"></i>

            </a>


            <a href="SanPham.aspx?Tuoi=6-8"
               class="age-card">

                <span class="age-number">
                    6–8
                </span>

                <div>
                    <h3>Bé 6–8 tuổi</h3>
                    <p>Phát triển tư duy</p>
                </div>

                <i class="fa-solid fa-arrow-right"></i>

            </a>


            <a href="SanPham.aspx?Tuoi=9-12"
               class="age-card">

                <span class="age-number">
                    9–12
                </span>

                <div>
                    <h3>Bé 9–12 tuổi</h3>
                    <p>Sáng tạo & khám phá</p>
                </div>

                <i class="fa-solid fa-arrow-right"></i>

            </a>

        </div>

    </section>


    <!-- ================= KHUYẾN MÃI ================= -->
    <section class="home-section">

        <div class="section-heading">

            <div>
                <span class="section-label">
                    ƯU ĐÃI
                </span>

                <h2>
                    Khuyến mãi hôm nay
                </h2>
            </div>

            <a href="KhuyenMai.aspx"
               class="view-all">

                Xem tất cả
                <i class="fa-solid fa-angle-right"></i>

            </a>

        </div>



        <div class="product-grid">

            <!-- Sau này lấy sản phẩm từ Database -->

            <asp:Repeater ID="rptKhuyenMai" runat="server" OnItemDataBound="rptKhuyenMai_ItemDataBound">
                <ItemTemplate>
                    <uc:ProductCard
                        ID="ProductCard1"
                        runat="server" />

                </ItemTemplate>
            </asp:Repeater>

        </div>



</section>



    <!-- ================= BÁN CHẠY ================= -->
    <section class="home-section">

        <div class="section-heading">

            <div>

                <span class="section-label">
                    NỔI BẬT
                </span>

                <h2>
                    Sản phẩm bán chạy
                </h2>

            </div>

            <a href="BanChay.aspx"
               class="view-all">

                Xem tất cả
                <i class="fa-solid fa-angle-right"></i>

            </a>

        </div>

        <div class="product-grid">

            <!-- Sau này tính từ ChiTietDonHang -->
            <asp:Repeater ID="rptBanChay" runat="server" OnItemDataBound="rptBanChay_ItemDataBound">

                <ItemTemplate>
                    <uc:ProductCard
                      ID="ProductCard2"
                      runat="server" />
                </ItemTemplate>
            </asp:Repeater>
        </div>


    </section>


    <!-- ================= AI ================= -->
    <section class="ai-home-section">

        <div class="ai-content">

            <span class="ai-label">

                <i class="fa-solid fa-wand-magic-sparkles"></i>

                CÔNG NGHỆ AI

            </span>


            <h2>
                Gợi ý đồ chơi<br />
                dành riêng cho bé
            </h2>


            <p>
                Cho chúng tôi biết độ tuổi, sở thích và nhu cầu của bé.
                Hệ thống sẽ phân tích và đề xuất những sản phẩm phù hợp.
            </p>


            <a href="GoiY.aspx"
               class="ai-button">

                Khám phá gợi ý

                <i class="fa-solid fa-arrow-right"></i>

            </a>

        </div>


        <div class="ai-visual">

            <div class="ai-icon">
                <i class="fa-solid fa-wand-magic-sparkles"></i>
            </div>

            <div class="ai-toy ai-toy-1">🧸</div>
            <div class="ai-toy ai-toy-2">🧩</div>
            <div class="ai-toy ai-toy-3">🚀</div>

        </div>

    </section>


    <!-- ================= CAM KẾT ================= -->
    <section class="service-section">

        <div class="service-item">

            <i class="fa-solid fa-shield-heart"></i>

            <div>
                <h3>Sản phẩm phù hợp</h3>
                <p>
                    Lựa chọn theo độ tuổi và nhu cầu của bé
                </p>
            </div>

        </div>


        <div class="service-item">

            <i class="fa-solid fa-truck-fast"></i>

            <div>
                <h3>Giao hàng nhanh</h3>
                <p>
                    Đóng gói cẩn thận, giao hàng tận nơi
                </p>
            </div>

        </div>


        <div class="service-item">

            <i class="fa-solid fa-headset"></i>

            <div>
                <h3>Hỗ trợ tận tâm</h3>
                <p>
                    Sẵn sàng hỗ trợ bạn mỗi ngày
                </p>
            </div>

        </div>


        <div class="service-item">

            <i class="fa-solid fa-wand-magic-sparkles"></i>

            <div>
                <h3>Gợi ý bằng AI</h3>
                <p>
                    Cá nhân hóa sản phẩm dành cho bé
                </p>
            </div>

        </div>

    </section>

</asp:Content>