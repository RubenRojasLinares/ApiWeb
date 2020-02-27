using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace EjemploApiMVC.DAOPersonas
{
    public class PersonaBD
    {
        private static readonly object _mutex = new Object();
        private static PersonaBD _instance;

        public static PersonaBD Instance
        {
            get
            {
                lock (_mutex)
                {
                    if (_instance == null)
                    {

                        _instance = new PersonaBD();
                    }
                }
                return _instance;
            }
        }

        public DataTable ObtienePersonas(string nombre,int edad, string mail)
        {
            DataTable dt = new DataTable("Datos");

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString))
            {
                
                SqlCommand command = new SqlCommand("SP_ObtenerPersonas", connection);
                command.Parameters["@nombre"].Value = nombre;
                command.Parameters["@edad"].Value = edad;
                command.Parameters["@mail"].Value = mail;

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    command.CommandTimeout = 0;
                    command.Connection.Open();
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public int InsertarPersonas(string nombre, int edad, string mail)
        {
            DataTable dt = new DataTable("Datos");
            int rowsAfected = 0;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("SP_InsertarPersonas", connection);
                command.Parameters["@nombre"].Value = nombre;
                command.Parameters["@edad"].Value = edad;
                command.Parameters["@mail"].Value = mail;

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    command.CommandTimeout = 0;
                    command.Connection.Open();
                    rowsAfected=command.ExecuteNonQuery();
                }
            }

            return rowsAfected;
        }


    }
}
