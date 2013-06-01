<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Commanigy.ProtectDotApp.Data.User>>" %>

<%@ Import Namespace="Commanigy.ProtectDotApp.Site.Properties" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Resources.ApplicationTitle %> | Listing all users
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2>
		All Users</h2>
	<table class="simple">
		<tr>
			<th>
				Email
			</th>
			<th>
				Credits
			</th>
			<th>
				Created
			</th>
		</tr>
		<% foreach (var item in Model) { %>
		<tr>
			<td>
				<%: Html.ActionLink(item.Email, "Details", new { id=item.ID }) %>
			</td>
			<td>
				<%: item.Credits %>
			</td>
			<td>
				<%: String.Format("{0:g}", item.CreatedAt) %>
			</td>
		</tr>
		<% } %>
	</table>
</asp:Content>
