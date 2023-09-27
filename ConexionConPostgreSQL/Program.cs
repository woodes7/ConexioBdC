using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionConPostgreSQL
    {
        class Program
        {
            static void Main(string[] args)
            {
                // Creamos Instancia de la clase de conexión
                Conexion.Conexion conexion = new Conexion.Conexion();

                //Establecer la conexión a la base de datos
                NpgsqlConnection dbConnection = conexion.EstablecerConexion();
                
                if (dbConnection != null && dbConnection.State == System.Data.ConnectionState.Open)
                {                  
                    string query = "SELECT * FROM gbp_almacen.gbp_alm_cat_libros ORDER BY id_libro ASC";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, dbConnection);
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    
                    // Mostrar
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["columna1"] + " " + reader["columna2"]+ reader["columna3"] + reader["columna4"] + reader["columna5"] );
                    }

                    // Cerramos
                    dbConnection.Close();
                }
                else
                {
                    Console.WriteLine("No se pudo establecer la conexión a la base de datos.");
                }

                Console.ReadLine(); 
            }
        }
}

