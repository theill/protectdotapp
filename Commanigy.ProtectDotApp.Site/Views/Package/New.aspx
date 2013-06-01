<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Commanigy.ProtectDotApp.Site.Properties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Resources.ApplicationTitle %> | Prevent decompilation of .NET applications
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" method="post" enctype="multipart/form-data" action="/Package/Create">
	<h2 id="upload" class="active"><span>Step 1</span> <%= Resources.Step1_Title %></h2>
	<% if (!string.IsNullOrEmpty(ViewData["ErrorMessage"] as string)) { %>
		<div class="message errormsg"><p><%= ViewData["ErrorMessage"] %></p></div>
	<% } %>
	<p> 
		Pick a .NET assembly from your local filesystem and upload it to our system. We
		will protect it and return an updated assembly back to you. Use this assembly in
		your deployment packages.
	</p> 
	<p>
		<asp:FileUpload ID="FileUpload" runat="server" /> <asp:Button ID="Button" runat="server" Text="Upload" />
	</p>
	
	<h2 id="pay" class="inactive"><span>Step 2</span> <%= Resources.Step2_Title %></h2> 
	<h2 id="download" class="inactive"><span>Step 3</span> <%= Resources.Step3_Title %></h2> 
	</form>
</asp:Content>
