<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="Commanigy.ProtectDotApp.Site.Properties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Resources.ApplicationTitle %> | Prevent decompilation of .NET applications
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="spacer"></div> 

	<h4 id="what-is-it">What is <%= Resources.ApplicationTitle %>?</h4>
	<p>
		<%= Resources.ApplicationTitle %> is an advanced encryption and 
		obfuscating service for your .NET assemblies with practically no
		startup penalty. Best of all it requires <b>zero</b> configuration to get
		started.
	</p>
				
	<h4 id="how-does-it-work">How does it work?</h4> 
	<p> 
		Once we receive your file, we will run it in a clean environment and analyze its call-stack 
		to figure out the best way to encrypt this assembly. This step help archive a very fast
		application startup.
	</p> 
	<p> 
		Vital parts of the assembly is then encrypted with a strong AES encryption to avoid people
		extracting your assembly file using any PE editors. Every assembly is encrypted with a unique
		key so no Protect .APP protected assemblies uses the same key.
	</p> 
	<p> 
		Scenarios where encryption is not possible or plausible will be intelligent obfuscated 
		on IL level.
	</p> 
				
	<h4 id="benchmarks">Benchmarks</h4> 
	<p> 
		When you choose to protect your application with our system, you do not have to be afraid
		of any penalties in start-up time. <%= Resources.ApplicationTitle %> has been designed to have practically no
		negative impact on performance. 
	</p> 
 
	<h4 id="pricing">Pricing</h4> 
	<p> 
		<%= Resources.ApplicationTitle %> is a pay-per-use service giving you a very cheap solution for protecting your
		commercial applications.
	</p> 
	<p> 
		The cost of protecting your assembly is <b>5 USD</b>. This amount might	change slightly, 
		based on local VAT, current days exchange rate and payment method. You will
		be informed about actual charged amount before making your payment.
	</p> 

	<h4 id="api">Developer API</h4>
	<p>
		We are hard at work creating an API for you to use with your build process. We know
		it is important for you to be able to automate this as much as possible. Subscribe to
		our <a href="#api-subscribe" id="open-announcement-form">announcements list</a> to get
		notified of updates.
	</p>
	<div id="api-subscribe">
		<form action="http://commanigy.createsend.com/t/y/s/kriklj/" method="post">
		<p>
		<label for="kriklj-kriklj">E-mail address</label><br />
		<input type="text" name="cm-kriklj-kriklj" id="kriklj-kriklj" /> <input type="submit" value="Subscribe" />
		to our announcement list
		</p>
		</form>
	</div>
</asp:Content>