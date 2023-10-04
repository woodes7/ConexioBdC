using ConexionConPostgreSQL.Dtos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionConPostgreSQL.Servicios
{
    internal interface InterfazConsultas
    {   /// <summary>
        /// Metodo para listar todos con su los libros solo con su Id del lirbo y Nombre del libro
        /// </summary>
        /// <param name="conexion"></param>
        /// <returns></returns>
        List<LibroDto> ListarLibro(NpgsqlConnection conexion);
        /// <summary>
        /// Metodo para ver todos los libros Lirbos
        /// </summary>
        /// <param name="conexion"></param>
        /// <returns></returns>
        List<LibroDto> SeccionarTodosLibros(NpgsqlConnection conexion);
        /// <summary>
        /// Metodo para modificar libros en la base de datos
        /// </summary>
        /// <param name="conexion"></param>
        /// <returns></returns>
        List<LibroDto> ModificarLibro(NpgsqlConnection conexion);
        /// <summary>
        /// Metodo para Inserta libros en la base de datos
        /// </summary>
        /// <param name="conexion"></param>
        /// <returns></returns>
        List<LibroDto> InsertarLirbo(NpgsqlConnection conexion);
        /// <summary>
        /// Metodo para borrar libros en la base de datos
        /// </summary>
        /// <param name="conexion"></param>
        /// <returns></returns>
        List<LibroDto> BorrarLirbo(NpgsqlConnection conexion);
    }
}
