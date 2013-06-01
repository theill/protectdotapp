namespace Commanigy.ProtectDotApp.Data {
	partial class DataClassesDataContext {
		partial void OnCreated() {
			System.Configuration.ConnectionStringSettings settings = System.Configuration.ConfigurationManager.ConnectionStrings["protectdotappConnectionString"];
			if (settings != null) {
				this.Connection.ConnectionString = settings.ConnectionString;
			}
		}
	}

	public enum PackageStatus : int {
		New = 1,
		Secured = 2,
		Paid = 3
	}
}
