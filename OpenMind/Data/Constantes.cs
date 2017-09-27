using System;
using System.Collections.Generic;


namespace OpenMind.Data
{
	public static class Constantes
	{
		public static string URL = "http://34.233.183.228:8080/seminario/";		
		public static string URL_Users_Login = URL + "oauth/token?";
        public static string URL_Users_GetQR = URL + "alumno/getqr?";
		public static string URL_Users_infoAlumno = URL + "alumno/infoalumno?";
        public static string URL_Users_allQuestion = URL + "preguntas/allQuestion";
	}
}
