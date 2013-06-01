#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZayPay.Sdk;
using Commanigy.ProtectDotApp.Site.Properties;
using Commanigy.ProtectDotApp.Data;
#endregion

namespace Commanigy.ProtectDotApp.Site.Controllers {
	public class ZayPayController : Controller {
		private static ZayPay.Sdk.ZayPay zaypay = new ZayPay.Sdk.ZayPay(Settings.Default.ZayPayApi);

		/// <summary>
		/// Callback from ZayPay when a payment changes
		/// </summary>
		/// <returns></returns>
		public ActionResult New() {
			if (Request["status"] == "paid") {
				string paymentID = Request["payment_id"];
				string priceSettingID = Request["price_setting_id"];
				//string message = Request["message"];

				// ensure request isn't spoofed by querying ZayPay
				ShowPaymentResponse rsp = zaypay.ShowPayment(Settings.Default.ZayPayPriceSettingID, Convert.ToInt32(paymentID));
				if (rsp.Payment.Status == PaymentStatus.Paid) {
					// custom data we initially sent
					int packageID = Convert.ToInt32(Request["package_id"]);

					// lookup package for which we just received a payment and ensure package hasn't
					// already been paid
					using (DataClassesDataContext db = new DataClassesDataContext()) {
						var package = (from p in db.Packages
									   where p.ID == packageID && p.Status != (int)PackageStatus.Paid
									   select p).First();
						
						package.Status = (int)PackageStatus.Paid;
						db.SubmitChanges();

						// return ZayPay marker (which is asterisk ok asterisk)
						return Content("*ok*");
					}
				}
			}

			return Content("*failed*");
		}
	}
}
