<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Commanigy.ProtectDotApp.Data.Package>" %>
<%@ Import Namespace="Commanigy.ProtectDotApp.Site.Properties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Resources.ApplicationTitle %> | Pay protection run
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript" language="javascript">
	$(function() {
        $.poll(2000, function (retry) {
            $.getJSON('<%= Url.Action("Details", "Payment", new { id = (this.ViewData.Model as Commanigy.ProtectDotApp.Data.Package).ID }) %>', function (data) {
				if (data.Status == <%= (int)Commanigy.ProtectDotApp.Data.PackageStatus.Paid %>) { // paid
					var downloadUrl = '<%= Url.Action("Details", "SecurePackage", new { id = "__token__"}, null) %>';
					downloadUrl = downloadUrl.replace(/__token__/, data.DownloadToken);
					location.href = downloadUrl;
				}
			});
            retry();
        });
	});
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2 id="upload" class="inactive"><span>Step 1</span> <%= Resources.Step1_Title %></h2>
	<h2 id="pay" class="active"><span>Step 2</span> <%= Resources.Step2_Title %></h2>
    <p>
		The assembly <b><%= Html.Encode(this.Model.Name) %></b> has been successfully
		received and is currently being analyzed and secured for you.
	</p>

	<% if (ViewData["user"] != null && (ViewData["user"] as Commanigy.ProtectDotApp.Data.User).Credits > 0) { %>
		<p>
			You have <%= (ViewData["user"] as Commanigy.ProtectDotApp.Data.User).Credits %> credit(s) available
			on your account. Would you like to use one credit for this protection run?
		</p>
		<p>
			<%= Html.ActionLink("Yes, use one credit", "Credit", "Payment", new { id = this.Model.ID, userID = (ViewData["user"] as Commanigy.ProtectDotApp.Data.User) .ID }, null)%>
		</p>
	<% } %>

	<div id="PaymentStatus" class="loading">
		Waiting for payment...
	</div>
	
	<p>
		You should send your payment now. Once your payment has been received, a download 
		link for your new secured assembly will be revealed.
	</p>

	<table>
	<tr>
		<td width="48%"><blockquote class="pay-text"><div class="arrow"></div>
			<div class="bubble">
				<p><b>Pay via SMS</b> <span style="float: right; color: #666">or</span></p>
				<p><%= Html.Encode(ViewData["PaymentInstructions"]) %></p>
			</div>
			</blockquote>
		</td>
		<td width="4%">&nbsp;</td>
		<td width="48%"><blockquote class="pay-paypal"><div class="arrow"></div>
		<div class="bubble">
			<p><b>Pay with PayPal</b></p>
			<p>
				Pay using any major credit cards or your PayPal account. You will pay 5 USD.
			</p>
			<center><form action="<%= ViewData["PayPalGatewayUrl"] %>" method="post">
<input type="hidden" name="cmd" value="_s-xclick" />
<input type="hidden" name="hosted_button_id" value="<%= ViewData["PayPalHostedButtonID"] %>" />
<input type="hidden" name="custom" value="<%= this.Model.ID %>" />
<input type="image" src="https://www.paypal.com/en_US/i/btn/btn_paynowCC_LG_global.gif" name="submit" alt="PayPal - The safer, easier way to pay online." width="144" height="47" />
<img alt="" border="0" src="https://www.paypal.com/en_US/i/scr/pixel.gif" width="1" height="1" />
</form><br /></center>
			<p>
				You will be redirected back to this page once payment has been completed.
			</p>
		</div></blockquote></td>
	</tr>
	</table>

	<% if (ViewData["user"] == null) { %>
		<div>
			<h3>Limited time offer</h3>
			<p>
				<%= Html.ActionLink("Create an account", "New", "Users", new { packageToken = Model.ID }, null)%> with us now and get your first 
				protection run <i>for free</i>. This offer is valid for all new customers.
			</p>
		</div>
	<% } %>
	<h2 id="download" class="inactive"><span>Step 3</span> <%= Resources.Step3_Title %></h2>
</asp:Content>