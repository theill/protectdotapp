#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO; 
#endregion

namespace Commanigy.ProtectDotApp.Site {
	public class Protector {
		private string file;

		public Protector(string file) {
			this.file = file;
		}

		public string GetToken() {
			FileStream fs = File.Open(file, FileMode.Open);

			string token = Guid.NewGuid().ToString("N");

			string securedFileName = Path.GetDirectoryName(file);
			TextWriter tw = new StreamWriter(Path.Combine(securedFileName, token) + ".txt");
			tw.WriteLine("This is just a sample of the generated file. We read a file of length: " + fs.Length);
			tw.Close();

			return token;
		}
	}
}