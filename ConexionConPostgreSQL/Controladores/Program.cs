using ConexionConPostgreSQL.Conexion;
using ConexionConPostgreSQL.Dtos;
using ConexionConPostgreSQL.Servicios;
using ConexionConPostgreSQL.Util;
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
            InterfazConexion conexionPostgresqlInterfaz = new ImplementacionConexion();
            InterfazConsultas consultasPostgresqlInterfaz = new ImplementacionConsultas();
            NpgsqlConnection conexion = null;
            conexion = conexionPostgresqlInterfaz.EstableceConexion();

            if (conexion != null)
            {
                foreach (LibroDto libro in consultasPostgresqlInterfaz.SeccionarTodosLibros(conexion))
                {
                    Console.WriteLine(libro.Titulo);
                }

            }
            Console.ReadLine();

        }
    }
}

