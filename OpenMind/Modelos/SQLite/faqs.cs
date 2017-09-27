using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace OpenMind.Modelos.SQLite
{
    public class faqs
	{
		public faqs() { }
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		public string pregunta { get; set; }
		public string respuesta { get; set; }		
	}
}
