<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductCard.ascx.cs" Inherits="ToySmart.ProductCard" %>
<link href="CSS/TaiKhoan.css" rel="stylesheet" />

<asp:UpdatePanel
    ID="upProductCard"
    runat="server"
    UpdateMode="Conditional"
    ChildrenAsTriggers="true">

    <ContentTemplate>

        <div class="product-card">

            <!-- Ảnh sản phẩm -->
            <asp:HyperLink
                ID="lnkHinhAnh"
                runat="server"
                CssClass="product-image-link">

                <div class="product-image">
                    <asp:Image
                        ID="imgSanPham"
                        runat="server"
                        CssClass="product-img"
                        AlternateText="Sản phẩm" />
                </div>

            </asp:HyperLink>


            <!-- Thông tin sản phẩm -->
            <div class="product-info">

                <h3 class="product-name">
                    <asp:HyperLink
                        ID="lnkTenSanPham"
                        runat="server">
                    </asp:HyperLink>
                </h3>


                <div class="product-price">

                    <asp:Label
                        ID="lblGia"
                        runat="server">
                    </asp:Label>

                    <span>đ</span>

                </div>


                <div class="product-meta">

                    <asp:Label
                        ID="lblThuongHieu"
                        runat="server">
                    </asp:Label>

                </div>


                <asp:Label
                    ID="lblThongBao"
                    runat="server"
                    CssClass="product-login-message">
                </asp:Label>


                <div class="product-actions">

                    <asp:LinkButton
                        ID="btnYeuThich"
                        runat="server"
                        CssClass="btn-favorite"
                        ToolTip="Thêm vào yêu thích"
                        OnClick="btnYeuThich_Click">

                        <i class="fa-solid fa-heart fa-heart-solid"></i>

                    </asp:LinkButton>


                    <asp:LinkButton
                        ID="btnThemGioHang"
                        runat="server"
                        CssClass="btn-cart"
                        ToolTip="Thêm vào giỏ hàng"
                        OnClick="btnThemGioHang_Click">

                        <i class="fa-solid fa-cart-plus"></i>

                        <span>
                            Thêm giỏ hàng
                        </span>

                    </asp:LinkButton>

                </div>

            </div>

        </div>

    </ContentTemplate>

</asp:UpdatePanel>