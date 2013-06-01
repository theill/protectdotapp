#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Commanigy.ProtectDotApp.Data;
using log4net;
using System.Web.Security;
#endregion

namespace Commanigy.ProtectDotApp.Site.Controllers {
	public class UsersController : Controller {
		private ILog log = LogManager.GetLogger("site");

		//
		// GET: /Users/

		public ActionResult Index() {
			return View();
		}

		public ActionResult New() {
			this.ViewData.Model = new User();
			return View();
		}

		//
		// GET: /Users/Details/5

		public ActionResult Details(int id, string packageToken) {
			var user = new DataClassesDataContext().Users.First(u => u.ID == id);

			Session["user_id"] = user.ID;

			ViewData["packageToken"] = packageToken;

			return View(user);
		}

		//
		// POST: /Users/Create

		[HttpPost]
		public ActionResult Create([Bind(Include="Email,Password")]User user) {
			try {
				if (ModelState.IsValid) {
					// give users one free credit
					user.Credits = 1;

					if (CreateUser(user)) {
						FormsAuthentication.SetAuthCookie(user.Email, true);

						// dispatch email to user telling him about sign
						user.SendSignupMail();

						// TODO: set "form authentication token" or similar
						Session["user_id"] = user.ID;

						return RedirectToAction("Details", new { id = user.ID, packageToken = Request["packageToken"] });
					}
				}

				return View("New", user);
			}
			catch (Exception x) {
				log.Error("Failed to create user", x);
				return View("New", user);
			}
		}

		private bool CreateUser(Data.User user) {
			using (DataClassesDataContext db = new DataClassesDataContext()) {
				user.CreatedAt = DateTime.UtcNow;

				db.Users.InsertOnSubmit(user);

				try {
					db.SubmitChanges();
					return true;
				}
				catch (System.Data.SqlClient.SqlException x) {
					if (x.Message.Contains("IX_Users")) {
						// duplicate email
						this.ModelState.AddModelError("Email", "The email must be unique");
					}
					else {
						throw;
					}
				}
			}

			return false;
		}
	}
}
