<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DangNhap.aspx.cs" Inherits="ToySmart.DangNhap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/TaiKhoan.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="tai-khoan-page">

        <div class="tai-khoan-box">

            <!-- Tiêu đề -->
            <h1 class="tai-khoan-title">
                Đăng nhập
            </h1>

            <!-- Mô tả -->
            <p class="tai-khoan-description">
                Đăng nhập để tiếp tục mua sắm cùng CUONG TRAM TINH
            </p>


            <!-- Tên đăng nhập -->
            <div class="form-group">

                <asp:Label
                    ID="lblTenDangNhap"
                    runat="server"
                    Text="Tên đăng nhập"
                    CssClass="form-label">
                </asp:Label>

                <asp:TextBox
                    ID="txtTenDangNhap"
                    runat="server"
                    CssClass="form-input"
                    placeholder="Nhập tên đăng nhập">
                </asp:TextBox>

                <asp:Label
                    ID="errTenDangNhap"
                    runat="server"
                    CssClass="form-error">
                </asp:Label>

            </div>


            <!-- Mật khẩu -->
            <div class="form-group">

                <asp:Label
                    ID="lblMatKhau"
                    runat="server"
                    Text="Mật khẩu"
                    CssClass="form-label">
                </asp:Label>

                <asp:TextBox
                    ID="txtMatKhau"
                    runat="server"
                    TextMode="Password"
                    CssClass="form-input"
                    placeholder="Nhập mật khẩu">
                </asp:TextBox>

                <asp:Label
                    ID="errMatKhau"
                    runat="server"
                    CssClass="form-error">
                </asp:Label>

            </div>


            <!-- Nút đăng nhập -->
            <asp:Button
                ID="btnDangNhap"
                runat="server"
                Text="Đăng nhập"
                CssClass="tai-khoan-button"
                OnClick="btnDangNhap_Click"/>


            <!-- Chuyển sang đăng ký -->
            <div class="tai-khoan-link">

                Bạn chưa có tài khoản?

                <a href="DangKy.aspx">
                    Đăng ký
                </a>

            </div>

        </div>

    </div>

</asp:Content>
