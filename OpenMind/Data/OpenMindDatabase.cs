using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using OpenMind.Modelos.SQLite;
//using OpenMind.Helpers;

namespace OpenMind.Data
{
    public class OpenMindDatabase
    {
		static object locker = new object();
		SQLiteConnection database;
		public OpenMindDatabase()
		{
			database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<cursos>();
            database.CreateTable<faqs>();
		}

        public IEnumerable<cursos> GetCursos()
		{
			lock (locker)
			{
				return database.Query<cursos>("SELECT * FROM [cursos] ");
			}
		}

		public IEnumerable<faqs> GetFAQs()
		{
			lock (locker)
			{
                return database.Query<faqs>("SELECT * FROM [faqs] ");
			}
		}

        public int InsertCursos(cursos curso)
		{
			lock (locker)
			{
				if (curso.id != 0)
				{
					database.Update(curso);
					return curso.id;
				}
				else
				{
					return database.Insert(curso);

				}
			}
		}

		public int InsertFAQ(faqs faq)
		{
			lock (locker)
			{
				if (faq.id != 0)
				{
					database.Update(faq);
					return faq.id;
				}
				else
				{
					return database.Insert(faq);

				}
			}
		}


		public int limpiarCursos()
		{
			lock (locker)
			{
                return database.DeleteAll<cursos>();
			}
		}

		public int limpiarFAQs()
		{
			lock (locker)
			{
				return database.DeleteAll<faqs>();
			}
		}
    }
}
