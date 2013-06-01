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
	public class UsersController : Controller {
		private ILog log = LogManager.GetLogger("site");

		//
		// GET: /Admin/Users
		[RequireBasicAuthentication]
		public ActionResult Index() {
			var users = new DataClassesDataContext().Users.OrderBy(u => u.CreatedAt);

			return View(users.ToList());
		}

		//
		// GET: /Admin/Users/Details/5
		[RequireBasicAuthentication]
		public ActionResult Details(int id) {
			var user = new DataClassesDataContext().Users.First(p => p.ID == id);

			return View(user);
		}

		//
		// GET: /Admin/Users/Delete/5
		public ActionResult Delete(int id) {
			log.DebugFormat("Deleting user {0}", id);

			using (DataClassesDataContext db = new DataClassesDataContext()) {
				var user = db.Users.First(p => p.ID == id);
				db.Users.DeleteOnSubmit(user);
				db.SubmitChanges();

				return RedirectToAction("Index");
			}
		}
	}
}
