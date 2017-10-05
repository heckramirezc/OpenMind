// Helpers/Settings.cs
using Refractored.Xam.Settings;
using Refractored.Xam.Settings.Abstractions;

namespace OpenMind.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string access_token = "access_token";
        private const string token_type = "";
        private const string refresh_token = "refresh_token";
        private const string expires_in = "expires_in";
        private const string scope = "expires_in";
        private const string carne = "carne";
        private const string nombre = "nombre";
        private const string url = "url";
        private const string role = "role";

        private static readonly string Predeterminado = string.Empty;

		#endregion


		public static string session_access_token
		{
			get
			{
				return AppSettings.GetValueOrDefault(access_token, Predeterminado);
			}
			set
			{
				AppSettings.AddOrUpdateValue(access_token, value);
			}
		}
		public static string session_token_type
		{
			get
			{
				return AppSettings.GetValueOrDefault(token_type, Predeterminado);
			}
			set
			{
				AppSettings.AddOrUpdateValue(token_type, value);
			}
		}
		public static string session_refresh_token
		{
			get
			{
				return AppSettings.GetValueOrDefault(refresh_token, Predeterminado);
			}
			set
			{
				AppSettings.AddOrUpdateValue(refresh_token, value);
			}
		}
		public static string session_expires_in
		{
			get
			{
				return AppSettings.GetValueOrDefault(expires_in, Predeterminado);
			}
			set
			{
				AppSettings.AddOrUpdateValue(expires_in, value);
			}
		}
		public static string session_scope
		{
			get
			{
				return AppSettings.GetValueOrDefault(scope, Predeterminado);
			}
			set
			{
				AppSettings.AddOrUpdateValue(scope, value);
			}
		}

		public static string session_carne
		{
			get
			{
				return AppSettings.GetValueOrDefault(carne, Predeterminado);
			}
			set
			{
				AppSettings.AddOrUpdateValue(carne, value);
			}
		}

		public static string session_nombre
		{
			get
			{
				return AppSettings.GetValueOrDefault(nombre, Predeterminado);
			}
			set
			{
				AppSettings.AddOrUpdateValue(nombre, value);
			}
		}

		public static string session_url
		{
			get
			{
				return AppSettings.GetValueOrDefault(url, Predeterminado);
			}
			set
			{
				AppSettings.AddOrUpdateValue(url, value);
			}
		}

		public static string session_role
		{
			get
			{
				return AppSettings.GetValueOrDefault(role, Predeterminado);
			}
			set
			{
				AppSettings.AddOrUpdateValue(role, value);
			}
		}

	}
}