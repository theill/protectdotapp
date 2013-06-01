<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Commanigy.ProtectDotApp.Site.Properties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Resources.ApplicationTitle %> | Download protected assembly
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2 id="upload" class="inactive"><span>Step 1</span> <%= Resources.Step1_Title %></h2>
	<h2 id="pay" class="inactive"><span>Step 2</span> <%= Resources.Step2_Title %></h2>
	<h2 id="download" class="active"><span>Step 3</span> <%= Resources.Step3_Title %></h2>
	<p>
		Your assembly has been successfully protected and your payment has been received.	
	</p>
	<p>
		Thank you for using <%= Resources.ApplicationTitle %>.
	</p>
	<p id="download-assembly">
		<%= Html.ActionLink("Download protected assembly now", "Download", "SecurePackage", new { id = (this.ViewData.Model as Commanigy.ProtectDotApp.Data.Package).ID }, null) %>
	</p>
	<p>
		Once downloaded your protected assembly will be removed from our servers.
	</p>
</asp:Content>