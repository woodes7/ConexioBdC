using ConexionConPostgreSQL.Dtos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionConPostgreSQL.Servicios
{
    internal interface InterfazConsultas
    {   /// <summary>
        /// Metodo para ver todos los libros Lirbos
        /// </summary>
        /// <param name="conexion"></param>
        /// <returns></returns>
        List<LibroDto> SeleccionarLibros(NpgsqlConnection conexion);

        /// <summary>
        /// Metodo que Muestra al libro buscado por su Isbn
        /// </summary>
        /// <param name="conexion"></param>
        /// <param name="listaLibrosObtenida"></param>
        /// <param name="isbnAConsultar"></param>
        LibroDto SeleccionarUnLibro(NpgsqlConnection conexion, string isbnAConsultar);
        
            /// <summary>
        /// Metodo para modificar libros en la base de datos
        /// </summary>
        /// <param name="conexion"></param>
        /// <returns></returns>        
        void ModificarLibro(NpgsqlConnection conexion, string isbnAConsultar);
        
        /// <summary>
        /// Metodo para Inserta libros en la base de datos
        /// </summary>
        /// <param name="conexion"></param>
        /// <returns></returns>
        void InsertarLirbo(NpgsqlConnection conexion);
        
        /// <summary>
        /// Metodo para borrar libros en la base de datos
        /// </summary>
        /// <param name="conexion"></param>
        /// <returns></returns>
        void BorrarLirbo(NpgsqlConnection conexion);
       
    }
}
