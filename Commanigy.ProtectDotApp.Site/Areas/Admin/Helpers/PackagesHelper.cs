using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Commanigy.ProtectDotApp.Site.Areas.Admin.Helpers {
	public class PackagesHelper {
		public static string ToStringStatus(int packageStatus) {
			if (packageStatus == (int)Data.PackageStatus.New) {
				return "New";
			}
			else if (packageStatus == (int)Data.PackageStatus.Paid) {
				return "Paid";
			}
			else if (packageStatus == (int)Data.PackageStatus.Secured) {
				return "Secured";
			}

			return string.Empty;
		}
	}
}