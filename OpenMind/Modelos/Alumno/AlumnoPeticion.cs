using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OpenMind.Modelos.Alumno
{
	public class AlumnoPeticion
	{
		public string nocarnet { get; set; }
		public string parametros
		{
			get
			{
				return
					"nocarnet=" + nocarnet;
			}
		}
	}

	public class AlumnoPeticion2
	{
		public string idCatedratico { get; set; }
		public string parametros
		{
			get
			{
				return
					"idCatedratico=" + idCatedratico;
			}
		}
	}


	public class AlumnoRespuesta
	{
		public string nombres { get; set; }
		public string apellidos { get; set; }
		public string correo { get; set; }
		public List<asignaciones> asignaciones { get; set; }
        public string pagado { get; set; }
        public string noCarnet { get; set; }		
	}

	public class AlumnoRespuesta1
	{
		public string nombres { get; set; }
		public string apellidos { get; set; }
		public string correo { get; set; }
		public List<List<asignaciones>> asignaciones { get; set; }
		public string pagado { get; set; }
		public string noCarnet { get; set; }
	}

    public class asignaciones
    {        
        public cursosRespuesta curso { get; set; }
        public string correlativo { get; set; }

    }

    public class cursosRespuesta
    {        
        public string idCurso { get; set; }
        public string seccion { get; set; }
        public string nombre { get; set; }
    }

}
