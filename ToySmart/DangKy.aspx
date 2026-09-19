<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DangKy.aspx.cs" Inherits="ToySmart.DangKy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/TaiKhoan.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="tai-khoan-page">

        <div class="tai-khoan-box">

            <h1 class="tai-khoan-title">
                Đăng ký tài khoản
            </h1>

            <p class="tai-khoan-description">
                Tạo tài khoản để bắt đầu mua sắm cùng CUONG TRAM TINH
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


            <!-- Nhập lại mật khẩu -->
            <div class="form-group">

                <asp:Label
                    ID="lblNhapLaiMatKhau"
                    runat="server"
                    Text="Nhập lại mật khẩu"
                    CssClass="form-label">
                </asp:Label>

                <asp:TextBox
                    ID="txtNhapLaiMatKhau"
                    runat="server"
                    TextMode="Password"
                    CssClass="form-input"
                    placeholder="Nhập lại mật khẩu">
                </asp:TextBox>

                <asp:Label
                    ID="errNhapLaiMatKhau"
                    runat="server"
                    CssClass="form-error">
                </asp:Label>

            </div>


            <!-- Họ và tên -->
            <div class="form-group">

                <asp:Label
                    ID="lblHoVaTen"
                    runat="server"
                    Text="Họ và tên"
                    CssClass="form-label">
                </asp:Label>

                <asp:TextBox
                    ID="txtHoVaTen"
                    runat="server"
                    CssClass="form-input"
                    placeholder="Nhập họ và tên">
                </asp:TextBox>

                <asp:Label
                    ID="errHoVaTen"
                    runat="server"
                    CssClass="form-error">
                </asp:Label>

            </div>


            <!-- Email -->
            <div class="form-group">

                <asp:Label
                    ID="lblEmail"
                    runat="server"
                    Text="Email"
                    CssClass="form-label">
                </asp:Label>

                <asp:TextBox
                    ID="txtEmail"
                    runat="server"
                    TextMode="Email"
                    CssClass="form-input"
                    placeholder="Nhập email">
                </asp:TextBox>

                <asp:Label
                    ID="errEmail"
                    runat="server"
                    CssClass="form-error">
                </asp:Label>

            </div>


            <!-- Số điện thoại -->
            <div class="form-group">

                <asp:Label
                    ID="lblSoDienThoai"
                    runat="server"
                    Text="Số điện thoại"
                    CssClass="form-label">
                </asp:Label>

                <asp:TextBox
                    ID="txtSoDienThoai"
                    runat="server"
                    CssClass="form-input"
                    placeholder="Nhập số điện thoại">
                </asp:TextBox>

                <asp:Label
                    ID="errSoDienThoai"
                    runat="server"
                    CssClass="form-error">
                </asp:Label>

            </div>


            <!-- Quê quán -->
            <div class="form-group">

                <asp:Label
                    ID="lblQueQuan"
                    runat="server"
                    Text="Quê quán"
                    CssClass="form-label">
                </asp:Label>

                <asp:TextBox
                    ID="txtQueQuan"
                    runat="server"
                    CssClass="form-input"
                    placeholder="Nhập quê quán">
                </asp:TextBox>

                <asp:Label
                    ID="errQueQuan"
                    runat="server"
                    CssClass="form-error">
                </asp:Label>

            </div>


            <!-- Nút đăng ký -->
            <asp:Button
                ID="btnDangKy"
                runat="server"
                Text="Đăng ký"
                CssClass="tai-khoan-button"
                OnClick="btnDangKy_Click" />


            <!-- Chuyển sang đăng nhập -->
            <div class="tai-khoan-link">

                Bạn đã có tài khoản?

                <a href="DangNhap.aspx">
                    Đăng nhập
                </a>

            </div>

        </div>

    </div>

</asp:Content>