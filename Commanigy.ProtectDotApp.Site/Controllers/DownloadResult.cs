#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc; 
#endregion

namespace Commanigy.ProtectDotApp.Site.Controllers {
	public class DownloadResult : ActionResult {

		public DownloadResult() {
		}

		public DownloadResult(string path) {
			this.Path = path;
		}

		public string Path {
			get;
			set;
		}

		public string ContentType {
			get;
			set;
		}

		public string FileDownloadName {
			get;
			set;
		}

		public override void ExecuteResult(ControllerContext context) {
			if (!String.IsNullOrEmpty(FileDownloadName)) {
				context.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + this.FileDownloadName);
				context.HttpContext.Response.ContentType = this.ContentType;
			}

			context.HttpContext.Response.TransmitFile(this.Path);
		}
	}
}