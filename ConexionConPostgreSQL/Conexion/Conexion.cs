using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.Remoting.Messaging;

namespace ConexionConPostgreSQL.Conexion
{
    internal class Conexion
    {
        NpgsqlConnection conex = new NpgsqlConnection();
        
        static String servidor = "localhost";
        static String bd = "gestorBibliotecaPersonal";
        static String usuario = "postgres";
        static String pasword = "";
        static String puerto = "5432";

        String CadenaConexion = "server=" + servidor + ";" + "port=" + puerto + ";" + "user id=" + usuario + "," + "pasword=" + pasword + ";" + "database=" + bd + ";" ;
             

        public NpgsqlConnection EstablecerConexion()
        {
            try
            { conex.ConnectionString = CadenaConexion;
                conex.Open();
                MessageBox.Show("Se conecto correctamente");
                Console.WriteLine("Se conecto correctamente");

            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("No se pudo conectar a la base de datos de PostgresSQL, error;" + e.ToString());

            }
            return conex;
        }
            
        
    }
}
