#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Commanigy.ProtectDotApp.Data;
using System.IO; 
#endregion

namespace Commanigy.ProtectDotApp.Site.Controllers {
	public class SecurePackageController : Controller {
		//
		// GET: /SecurePakage/Details/<guid>
		public ActionResult Details(long id) {
			using (DataClassesDataContext db = new DataClassesDataContext()) {
				var package = db.Packages.SingleOrDefault(p => p.ID == id && p.Status == (int)PackageStatus.Paid);
				return View(package);
			}
		}

		public DownloadResult Download(long id) {
			using (DataClassesDataContext db = new DataClassesDataContext()) {
				var package = db.Packages.SingleOrDefault(p => p.ID == id && p.Status == (int)PackageStatus.Paid);

				string savedFileName = PackageStore.Read(package);

				return new DownloadResult { Path = savedFileName, ContentType = package.ContentType, FileDownloadName = package.Name };
			}
		}
	}
}