using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.Remoting.Messaging;
using ConexionConPostgreSQL.Servicios;
using System.Configuration;

namespace ConexionConPostgreSQL.Conexion
{
    internal class ImplementacionConexion : InterfazConexion
    {
        public NpgsqlConnection InterfazConexionEestableceConexion()
        {
           

        NpgsqlConnection InterfazConexion.EstableceConexion()
        {
                //Se lee la cadena de conexión a Postgresql del archivo de configuración
                string stringConexionPostgresql = ConfigurationManager.ConnectionStrings["stringConexion"].ConnectionString;
                Console.WriteLine("[INFORMACIÓN-ConexionPostgresqlImplementacion-generarConexionPostgresql] Cadena conexión: " + stringConexionPostgresql);

                NpgsqlConnection conexion = null;
                string estado = "";

                if (!string.IsNullOrWhiteSpace(stringConexionPostgresql))
                {
                    try
                    {
                        conexion = new NpgsqlConnection(stringConexionPostgresql);
                        conexion.Open();
                        //Se obtiene el estado de conexión para informarlo por consola
                        estado = conexion.State.ToString();
                        if (estado.Equals("Open"))
                        {

                            Console.WriteLine("[INFORMACIÓN-ConexionPostgresqlImplementacion-generarConexionPostgresql] Estado conexión: " + estado);

                        }
                        else
                        {
                            conexion = null;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[ERROR-ConexionPostgresqlImplementacion-generarConexionPostgresql] Error al generar la conexión:" + e);
                        conexion = null;
                        return conexion;

                    }
                }

                return conexion;
            }
        }
    }
}
