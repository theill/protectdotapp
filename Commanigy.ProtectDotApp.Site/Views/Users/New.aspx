<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Commanigy.ProtectDotApp.Data.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Sign up
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<% Html.BeginForm("Create", "Users", FormMethod.Post); %>
	<%= Html.Hidden("packageToken", Request["packageToken"]) %>
    <h2>Sign up</h2>

	<p>
		For a limited time only, you are able to create an account and get your
		first protection run <i>for free</i>. No credit-card required!
	</p>

	<p>
		This will allow you to test out how effective <%= Commanigy.ProtectDotApp.Site.Properties.Resources.ApplicationTitle %>
		really is. We are confident you won't be disappointed.
	</p>

	<fieldset>
		<%= Html.LabelFor(model => model.Email) %><br />
		<%= Html.TextBoxFor(model => model.Email) %><br />
		<%= Html.ValidationMessageFor(model => model.Email) %>
	</fieldset>
		
	<fieldset>
		<%= Html.LabelFor(model => model.Password) %><br />
		<%= Html.PasswordFor(model => model.Password) %><br />
		<%= Html.ValidationMessageFor(model => model.Password) %>
	</fieldset>

	<fieldset>
		<input type="submit" name="name" value="Sign Up" />
	</fieldset>

	<% Html.EndForm(); %>

</asp:Content>