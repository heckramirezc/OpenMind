using System;
namespace OpenMind.Modelos.Alumno
{    
	public class AsistenciaPeticion
	{
		public string idCurso { get; set; }
		public string parametros
		{
			get
			{
				return
					"idCurso=" + idCurso;
			}
		}
	}

	public class AsistenciaRespuesta
	{
		public string nombres { get; set; }
		public string apellidos { get; set; }
		public string correo { get; set; }		
		public string pagado { get; set; }
		public string carnet { get; set; }
        public string nombre { get { return nombres + " " + apellidos; }}
	}
}
