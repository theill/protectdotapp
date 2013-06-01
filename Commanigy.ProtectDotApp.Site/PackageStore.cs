#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Commanigy.ProtectDotApp.Data;
using System.IO;
using System.Threading;
using log4net; 
#endregion

namespace Commanigy.ProtectDotApp.Site {
	/// <summary>
	/// Store an uploaded package to a specific location.
	/// </summary>
	public class PackageStore {
		private static ILog log = LogManager.GetLogger("site");

		class ProtectorData {
			public Package Package { get; set; }
			public string FileName { get; set; }
		}

		public static string Read(Package package) {
			string packagesDirectory = Path.Combine(Properties.Settings.Default.StorageLocation.Trim(), "_packages");
			packagesDirectory = Path.Combine(packagesDirectory, package.ID.ToString());
			string savedFileName = Path.Combine(packagesDirectory, Path.GetFileName(package.Name));
			return savedFileName;
		}

		public static void Write(Package package, HttpPostedFileBase file) {
			// ensure to ".Trim()" line to fix bug in Web.config transformations
			string packagesDirectory = Path.Combine(Properties.Settings.Default.StorageLocation.Trim(), "_packages");
			new DirectoryInfo(packagesDirectory).Create();

			packagesDirectory = Path.Combine(packagesDirectory, package.ID.ToString());
			new DirectoryInfo(packagesDirectory).Create();

			string savedFileName = Path.Combine(packagesDirectory, Path.GetFileName(file.FileName));
			file.SaveAs(savedFileName);

			new Thread(new ParameterizedThreadStart(delegate(object data) {
				var b = data as ProtectorData;

				// TODO: do actual protection
				Protector protector = new Protector(b.FileName);

				Thread.Sleep(10000);
				string token = protector.GetToken();

				log.DebugFormat("Protected {0} and stored with unique token {1}", b.FileName, token);
				using (DataClassesDataContext db = new DataClassesDataContext()) {
					var securedPackage = db.Packages.First(p => p.ID == b.Package.ID);
					securedPackage.Status = (int)PackageStatus.Secured;
					db.SubmitChanges();
				}
			})).Start(new ProtectorData { FileName = savedFileName, Package = package });
		}
	}
}