<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<List<Commanigy.ProtectDotApp.Data.Package>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>All Packages</h2>

	<table class="simple">
	<thead>
	<tr>
		<th>Name</th>
		<th>Status</th>
		<th>Created</th>
	</tr>
	</thead>
	<tbody>
	<% foreach (var package in ViewData.Model) { %>
	<tr>
		<td><%= Html.ActionLink(Html.Encode(package.Name), "Details", new { id = package.ID }) %></td>
		<td><%= Commanigy.ProtectDotApp.Site.Areas.Admin.Helpers.PackagesHelper.ToStringStatus(package.Status) %></td>
		<td><%= package.CreatedAt %></td>
	</tr>
	<% } %>
	</tbody>
	</table>

</asp:Content>
