#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net.Config;
using log4net; 
#endregion

namespace Commanigy.ProtectDotApp.Site {
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication {
		private ILog log = LogManager.GetLogger("site");

		public static void RegisterRoutes(RouteCollection routes) {
			// look at this: http://stevehodgkiss.com/blog/2010/03/21/restful-routing-for-asp-net-mvc-2/

			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

//			routes.MapRoute("Root", "", new { controller = "Package", action = "New" });

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "About", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
				new string[] { "Commanigy.ProtectDotApp.Site.Controllers" }
			);
		}

		protected void Application_Start() {
			XmlConfigurator.Configure();
			log.Info("Protect.APP has been started up");

			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);
		}
	}
}