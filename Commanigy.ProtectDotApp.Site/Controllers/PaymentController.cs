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
	public class PaymentController : Controller {
		//
		// GET: /Payment/Details/5
		[OutputCache(Duration=0, VaryByParam="None")]
		public JsonResult Details(int id) {
			using (DataClassesDataContext db = new DataClassesDataContext()) {
				var package = db.Packages.First(p => p.ID == id);

				return this.Json(new { Status = (int)package.Status, DownloadToken = package.ID }, JsonRequestBehavior.AllowGet);
			}
		}

		/// <summary>
		/// Decrease credit on users account and update package status to "paid"
		/// </summary>
		/// <param name="id">ID of package</param>
		/// <returns></returns>
		public ActionResult Credit(int id, int userID) {
			using (DataClassesDataContext db = new DataClassesDataContext()) {
				var package = db.Packages.First(p => p.ID == id);

				var user = db.Users.First(u => u.ID == userID);
				if (user.Credits > 0) {
					user.Credits--;
					package.Status = (int)PackageStatus.Paid;
					db.SubmitChanges();
				}

				return RedirectToAction("Details", "SecurePackage", new { id = package.ID });
			}
		}
	}
}
