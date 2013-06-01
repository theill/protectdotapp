#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Commanigy.ProtectDotApp.Data;
using log4net; 
#endregion

namespace Commanigy.ProtectDotApp.Site.Areas.Admin.Controllers {
	public class PackagesController : Controller {
		private ILog log = LogManager.GetLogger("site");

		//
		// GET: /Admin/Packages
		[RequireBasicAuthentication]
		public ActionResult Index() {
			var packages = from p in new DataClassesDataContext().Packages
						   orderby p.CreatedAt descending
						   select p;

			return View(packages.ToList());
		}

		//
		// GET: /Admin/Packages/Details/5
		[RequireBasicAuthentication]
		public ActionResult Details(int id) {
			var package = new DataClassesDataContext().Packages.First(p => p.ID == id);

			return View(package);
		}
	}
}
