using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

namespace Commanigy.ProtectDotApp.Site {
	/// <summary>
	/// Checks the User's authentication using FormsAuthentication
	/// and redirects to the Login Url for the application on fail
	/// </summary>
	public class RequireBasicAuthenticationAttribute : ActionFilterAttribute {

		public override void OnActionExecuting(ActionExecutingContext filterContext) {
			var req = filterContext.HttpContext.Request;
			if (string.IsNullOrEmpty(req.Headers["Authorization"]) || req.Headers["Authorization"] != "Basic YWRtaW46c2VjcmV0NDI=") {
				var res = filterContext.HttpContext.Response;
				res.StatusCode = 401;
				res.AddHeader("WWW-Authenticate", "Basic realm=\"Commanigy\"");
				res.End();
			}
		}
	}
}