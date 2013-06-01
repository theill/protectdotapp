#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ZayPay.Sdk;
using Commanigy.ProtectDotApp.Site.Properties;
using System.Data.Services.Client;
using System.Data.Services.Common;
using System.Threading;
using Commanigy.ProtectDotApp.Data;
using log4net;
#endregion

namespace Commanigy.ProtectDotApp.Site.Controllers {

	public class PackageController : Controller {
		private ILog log = LogManager.GetLogger("site");

		private ZayPay.Sdk.ZayPay zaypay = new ZayPay.Sdk.ZayPay(Settings.Default.ZayPayApi);
		private int priceSettingID = Settings.Default.ZayPayPriceSettingID;

		PackageRepository repository = new PackageRepository();

		public ActionResult New() {
			return View();
		}

		[HttpPost]
		public ActionResult Create(FormCollection formCollection) {
			if (Request.Files.Count == 0 || Request.Files[0].ContentLength == 0) {
				ViewData["ErrorMessage"] = "You need to specify an assembly";
				return View("New");
			}

			HttpPostedFileBase hpf = Request.Files[0];
			if (!(new string[] { ".dll", ".exe", ".msi" }.Contains(Path.GetExtension(hpf.FileName).ToLower()))) {
				ViewData["ErrorMessage"] = "Your file needs to be either a .msi, .dll or .exe file";
				return View("New");
			}

			Package package = new Package {
				CreatedAt = DateTime.UtcNow,
				Name = Path.GetFileName(hpf.FileName),
				ContentType = hpf.ContentType,
				ContentLength = hpf.ContentLength,
				Status = (int)PackageStatus.New,
				IPAddress = this.Request.UserHostAddress
			};

			repository.Add(package);
			repository.Save();

			PackageStore.Write(package, hpf);

			return RedirectToAction("Details", new { id = package.ID });
		}

		public ActionResult Details(int id) {
			var package = repository.Find(id);
			var user = (Session["user_id"] != null) ? new DataClassesDataContext().Users.First(u => u.ID == Convert.ToInt32(Session["user_id"])) : null;

			//Session.Remove("pendingPaymentInstructions");

			string paymentInstructions = Session["pendingPaymentInstructions"] as string;

			if (paymentInstructions == null) {
				try {
					Locale locale = zaypay.LocaleForIP(this.Request.UserHostAddress, priceSettingID);
					LocaleInfo localeInfo = zaypay.ListLocales("", priceSettingID);

					List<Country> countries = new List<Country>(localeInfo.Countries);
					Country selectedCountry = countries.Find((Country c) => { return c.Code == ((locale != null) ? locale.Country : "DK"); });
					if (selectedCountry == null) {
						selectedCountry = countries.First();
					}

					PaymentMethod[] paymentMethods = zaypay.ListPaymentMethods("", new Locale("en", selectedCountry.Code), priceSettingID);

					// use sms if available - fallback to first if it isn't
					int paymentMethodID = (int)PaymentMethodType.Sms;
					if (paymentMethods.First((pm) => { return pm.PaymentMethodType == PaymentMethodType.Sms; }) == null) {
						paymentMethodID = (int)paymentMethods.First().PaymentMethodType;
					}

					CreatePaymentResponse rsp = zaypay.CreatePayment("", new Locale("en", selectedCountry.Code), priceSettingID, paymentMethodID, string.Format("package_id={0}", package.ID));

					using (DataClassesDataContext db = new DataClassesDataContext()) {
						package.ZayPayPaymentID = rsp.Payment.ID;
						db.SubmitChanges();
					}

					Session["pendingPaymentInstructions"] = paymentInstructions = rsp.LongInstructions;
				}
				catch (Exception x) {
					// got an error probably because we got something different from ZayPay 
					// than expected
					log.Error("Failed to prepare payment for ZayPay", x);
				}
			}
			else {
				paymentInstructions = Session["pendingPaymentInstructions"] as string;
			}

			ViewData["PaymentInstructions"] = paymentInstructions ?? "It is not possible to pay via SMS in your country.";
			ViewData["PayPalGatewayUrl"] = Settings.Default.PayPalGatewayUrl.Trim();
			ViewData["PayPalHostedButtonID"] = Settings.Default.PayPalHostedButtonID.Trim();

			ViewData["user"] = user;

			return View(package);
		}
	}
}
