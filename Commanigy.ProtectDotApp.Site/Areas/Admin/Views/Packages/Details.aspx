<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Commanigy.ProtectDotApp.Data.Package>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Details for <%= this.Model.Name %></h2>

	<p>FileSize: <%= this.Model.ContentLength %></p>
	<p>Content-type: <%= this.Model.ContentType %>
	</p>
	<p>Status: <%= this.Model.Status %>
	</p>
	<p>ZayPay payment ID: <%= this.Model.ZayPayPaymentID %>
	</p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
