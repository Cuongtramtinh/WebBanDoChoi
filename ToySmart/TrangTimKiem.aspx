<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrangTimKiem.aspx.cs" Inherits="ToySmart.TrangTimKiem" %>
<%@ Register Src="~/ProductCard.ascx" TagPrefix="uc" TagName="ProductCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/style.css" rel="stylesheet" />
    <link href="CSS/TrangChu.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="tim-kiem-page">

        <h1 class="tim-kiem-title">
            Kết quả tìm kiếm
        </h1>

        <asp:Label
            ID="lblTuKhoa"
            runat="server"
            CssClass="tim-kiem-tu-khoa">
        </asp:Label>

        <asp:Label
            ID="lblThongBao"
            runat="server"
            CssClass="tim-kiem-thong-bao">
        </asp:Label>

        <div class="product-grid">

            <asp:Repeater
                ID="rptKetQuaTimKiem"
                runat="server"
                OnItemDataBound="rptKetQuaTimKiem_ItemDataBound">

                <ItemTemplate>

                    <uc:ProductCard
                        ID="ProductCard1"
                        runat="server" />

                </ItemTemplate>

            </asp:Repeater>

        </div>

    </div>

</asp:Content>