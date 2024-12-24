using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane_Otomasyonu
{
	public class DatabaseConnection
	{
		private readonly string connectionString = "Host=localhost;Username=postgres;Password=123456;Database=hastaneyonetimsistemi";

		public NpgsqlConnection GetConnection()
		{
			return new NpgsqlConnection(connectionString);
		}
	}
}
