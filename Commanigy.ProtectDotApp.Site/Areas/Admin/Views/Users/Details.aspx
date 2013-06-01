<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Commanigy.ProtectDotApp.Data.User>" %>
<%@ Import Namespace="Commanigy.ProtectDotApp.Site.Properties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Resources.ApplicationTitle %> | Displaying single user
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Model.Email %></h2>

    <fieldset>
        <div class="display-label">Email</div>
        <div class="display-field"><%: Model.Email %></div>
        
        <div class="display-label">ConfirmationToken</div>
        <div class="display-field"><%: Model.ConfirmationToken %></div>
        
        <div class="display-label">Credits</div>
        <div class="display-field"><%: Model.Credits %></div>
    </fieldset>

    <p>
		<%: Html.ActionLink("Delete", "Delete", new { id = Model.ID }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>