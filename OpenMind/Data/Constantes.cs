﻿using System;
using System.Collections.Generic;


namespace OpenMind.Data
{
	public static class Constantes
	{
		public static string URL = "http://34.233.183.228:8080/seminario/";
        public static bool PerfilAbierto = false;
        public static bool ModalAbierto = false;
		public static string URL_Users_Login = URL + "oauth/token?";
        public static string URL_Users_GetQR = URL + "alumno/getqr?";
        public static string URL_Users_getAlumnos = URL + "/catedratico/getAlumnos/";
        public static string URL_Users_reloadmail = URL + "/alumno/reloadmail?";
        public static string URL_Users_confirmUser = URL + "confirmUser?";
        public static string URL_Users_changepassword = URL + "changepassword?";
        public static string URL_Users_infoAlumno = URL + "alumno/infoalumno?";
        public static string URL_Users_getCursosCatedratico = URL + "catedratico/getCourses?";		
		public static string URL_Users_allQuestion = URL + "preguntas/allQuestion";
    }
}
