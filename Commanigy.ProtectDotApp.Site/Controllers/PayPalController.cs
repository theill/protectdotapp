#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Commanigy.ProtectDotApp.Data;
using Commanigy.ProtectDotApp.Site.Properties; 
#endregion

namespace Commanigy.ProtectDotApp.Site.Controllers {
	public class PayPalController : Controller {
		private ILog log = LogManager.GetLogger("site");

		// GET: /PayPal/Create (PayPal redirects user to this page including "custom" parameter which, in our case, holds package id)
		public ActionResult Create() {
			string id = this.Request["custom"];
			log.DebugFormat("Got PayPal Create GET request with custom id {0} so redirecting to show action", id);

			return RedirectToAction("Details", "Package", new { id = Convert.ToInt32(id) });
		}

		// POST: /PayPal/Create (PayPal does a IPN POST to this url)
		[HttpPost]
		public ActionResult Create(FormCollection collection) {
			try {
				string packageID = collection["custom"];
				string receiver_email = collection["receiver_email"];
				log.DebugFormat("Got custom => {0} and receiver_email => {1} from paypal", packageID, receiver_email);

				PayPalIpn objPP = new PayPalIpn(Settings.Default.PayPalGateway.Trim(), Settings.Default.PayPalEmail.Trim());
				objPP.MakeHttpPost();
				string status = objPP.CheckStatus();

				log.DebugFormat("Got response => {0} from paypal which translates to => {1}", objPP.Response, status);
				
				if (status == "OK") {
					using (DataClassesDataContext db = new DataClassesDataContext()) {
						var package = db.Packages.First(p => p.ID == Convert.ToInt32(packageID) && p.Status != (int)PackageStatus.Paid);

						package.Status = (int)PackageStatus.Paid;
						db.SubmitChanges();
					}
				}

				return Content("OK");
			}
			catch (Exception x) {
				log.Error("Failed to process POST request from PayPal", x);
				throw new Exception("Failed to process request");
			}
		}
	}
}