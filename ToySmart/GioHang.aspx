<%@ Page Title="Giỏ hàng" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="GioHang.aspx.cs"
    Inherits="ToySmart.GioHang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/GioHang.css?v=2" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="home-section">

        <div class="section-heading">
            <div>
                <span class="section-label">ĐƠN HÀNG</span>
                <h2>Giỏ hàng của bạn</h2>
            </div>
        </div>

        <asp:UpdatePanel ID="upGioHang" runat="server" UpdateMode="Always">
            <ContentTemplate>

                <asp:Repeater ID="rptGioHang" runat="server"
                    OnItemCommand="rptGioHang_ItemCommand">

                    <HeaderTemplate>
                        <table class="bang-gio-hang">
                            <tr>
                                <th>Sản phẩm</th>
                                <th>Đơn giá</th>
                                <th>Số lượng</th>
                                <th>Thành tiền</th>
                                <th></th>
                            </tr>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td class="cot-san-pham">
                                <img src='<%# Eval("HinhAnh") %>' alt="" />
                                <a href='ChiTietSanPham.aspx?MaSanPham=<%# Eval("MaSanPham") %>'>
                                    <%# Eval("TenSanPham") %>
                                </a>
                            </td>

                            <td><%# Eval("Gia", "{0:N0}") %> đ</td>

                            <td class="cot-so-luong">
                                <asp:LinkButton ID="btnGiam" runat="server"
                                    CommandName="Giam"
                                    CommandArgument='<%# Eval("MaSanPham") %>'
                                    CssClass="btn-so-luong" Text="-" />

                                <span><%# Eval("SoLuong") %></span>

                                <asp:LinkButton ID="btnTang" runat="server"
                                    CommandName="Tang"
                                    CommandArgument='<%# Eval("MaSanPham") %>'
                                    CssClass="btn-so-luong" Text="+" />
                            </td>

                            <td class="cot-thanh-tien">
                                <%# Eval("ThanhTien", "{0:N0}") %> đ
                            </td>

                            <td>
                                <asp:LinkButton ID="btnXoa" runat="server"
                                    CommandName="Xoa"
                                    CommandArgument='<%# Eval("MaSanPham") %>'
                                    CssClass="btn-xoa"
                                    OnClientClick="return xacNhanXoa(this);">
                                    <i class="fa-solid fa-trash"></i>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        </table>
                    </FooterTemplate>

                </asp:Repeater>

                <asp:Panel ID="pnlGioHangTrong" runat="server"
                    CssClass="gio-hang-trong" Visible="false">
                    <p>Giỏ hàng của bạn đang trống.</p>
                    <a href="TrangChu.aspx" class="banner-button">Tiếp tục mua sắm</a>
                </asp:Panel>

                <asp:Panel ID="pnlTongTien" runat="server"
                    CssClass="khu-tong-tien" Visible="false">
                    <span>Tổng cộng:</span>
                    <strong><asp:Label ID="lblTongTien" runat="server" /> đ</strong>

                    <asp:Button ID="btnThanhToan" runat="server"
                        Text="Thanh toán"
                        CssClass="btn-thanh-toan"
                        OnClick="btnThanhToan_Click" />
                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>

    </section>


    <!-- ==========================================
         MODAL XÁC NHẬN XÓA
         Thay cho confirm() mặc định của trình duyệt
         để đồng bộ màu sắc với các thông báo khác.
         ========================================== -->

    <div class="modal-nen" id="modalXacNhanXoa">

        <div class="modal-hop">

            <i class="fa-solid fa-trash-can"></i>

            <p>Xóa sản phẩm này khỏi giỏ hàng?</p>

            <div class="modal-actions">

                <button type="button" class="modal-btn modal-btn-huy"
                    onclick="dongModalXoa();">
                    Hủy
                </button>

                <button type="button" class="modal-btn modal-btn-xoa"
                    onclick="xacNhanXoaOK();">
                    Xóa
                </button>

            </div>

        </div>

    </div>


    <script>
        var _nutXoaDangChon = null;

        function xacNhanXoa(nut) {
            _nutXoaDangChon = nut;
            document.getElementById('modalXacNhanXoa').classList.add('hien');
            return false;
        }

        function dongModalXoa() {
            document.getElementById('modalXacNhanXoa').classList.remove('hien');
            _nutXoaDangChon = null;
        }

        function xacNhanXoaOK() {
            document.getElementById('modalXacNhanXoa').classList.remove('hien');

            if (_nutXoaDangChon) {
                var lenh = _nutXoaDangChon.getAttribute('href');

                if (lenh && lenh.indexOf('__doPostBack') !== -1) {
                    eval(lenh.replace('javascript:', ''));
                }

                _nutXoaDangChon = null;
            }
        }
    </script>

</asp:Content>