using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commanigy.ProtectDotApp.Data {
	public class PackageRepository {
		private DataClassesDataContext db = new DataClassesDataContext();

		public IQueryable<Package> FindAllPackages() {
			return db.Packages;
		}

		public Package Find(int id) {
			return db.Packages.SingleOrDefault(a => a.ID == id);
		}
			
		public void Add(Package a) {
			db.Packages.InsertOnSubmit(a);
		}

		public void Remove(Package a) {
			db.Packages.DeleteOnSubmit(a);
		}

		public void Save() {
			db.SubmitChanges();
		}
	}
}
