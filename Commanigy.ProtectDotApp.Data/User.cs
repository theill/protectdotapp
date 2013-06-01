#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail; 
#endregion

namespace Commanigy.ProtectDotApp.Data {
	[MetadataType(typeof(UserMetaData))]
	public partial class User {
		public partial class UserMetaData {
			[StringLength(128), Required]
			public string Email { get; set; }

			[Required]
			public string Password { get; set; }
		}

		public void SendSignupMail() {
			var message = new MailMessage("protectdotapp@commanigy.com", this.Email) {
				Subject = "Signed up for Protect .APP",
				Body = string.Format(@"Hi,

You have just signed up for a Protect .APP account and your credit score is {0}. Each protection run costs you one credit and it is possible to buy credits in 5, 10 or 15 packs for a reduced price.

Sign in now at http://protectdotapp.commanigy.com/ and protect your .NET applications in seconds.

Thanks

// The hard-working team behind Protect .APP", this.Credits)
			};

			var client = new SmtpClient();
			client.EnableSsl = true;
			client.Send(message);
		}
	}
}