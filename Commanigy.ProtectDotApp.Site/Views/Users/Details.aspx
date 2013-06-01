<%@ Page Title="Sign Up Complete" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Commanigy.ProtectDotApp.Data.User>" %>
<%@ Import Namespace="Commanigy.ProtectDotApp.Site.Properties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Resources.ApplicationTitle %> | Signup complete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Signup complete</h2>

	<p>
		You have signed up. Continue your <%= Html.ActionLink("free protection run", "Details", "Package", new { id = Request["packageToken"] }, null) %> now.
	</p>

</asp:Content>