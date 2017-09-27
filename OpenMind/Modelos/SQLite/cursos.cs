using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace OpenMind.Modelos.SQLite
{
	public class cursos
	{
		public cursos() { }
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
        public string idCurso { get; set; }
        public string seccion { get; set; }
        public string nombre { get; set; }		
    }
}
