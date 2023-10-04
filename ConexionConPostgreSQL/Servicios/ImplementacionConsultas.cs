using ConexionConPostgreSQL.Dtos;
using ConexionConPostgreSQL.Util;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionConPostgreSQL.Servicios
{
    internal class ImplementacionConsultas : InterfazConsultas
    {
        ADto aDto = new ADto();
        LibroDto libroDto = new LibroDto();
        NpgsqlCommand consulta = new NpgsqlCommand("SELECT * FROM \"gbp_almacen\".\"gbp_alm_cat_libros\" WHERE id_libro = @idLibro", conexion);;
        NpgsqlDataReader resultadoConsulta ;
        private long idLibro;

        private long CapturaLibro(NpgsqlConnection conexion)
        {
            Console.WriteLine("Dime la id del libro que de seas");
            try
            {
                long idLibro = long.Parse(Console.ReadLine());
                libroDto.Id_libro = idLibro;
                
                consulta.Parameters.AddWithValue("@id_libro", idLibro);
                resultadoConsulta = consulta.ExecuteReader();


                LibroDto libroSeleccionado = ADto.readerLibroSeleccionado(resultadoConsulta);

                if (resultadoConsulta.Read())
                {
                    idLibro = resultadoConsulta.GetInt64(0);
                    Console.WriteLine("ID del libro escogido: " + idLibro);
                }
                else
                {
                    Console.WriteLine("No se encontró ningún libro con la ID especificada.");
                    idLibro = 0; // Establecer el valor en 0 o cualquier otro valor predeterminado según tus necesidades.
                }

                Console.WriteLine("[INFORMACIÓN-ConsultasPostgresqlImplementacion-seccionarTodosLibros] Cierre conexión y conjunto de datos");
                conexion.Close();
                resultadoConsulta.Close();

              
                int cont = listaLibros.Count();
                Console.WriteLine("[INFORMACIÓN-ConsultasPostgresqlImplementacion-seccionarTodosLibros] Número de libros: " + cont);


                Console.WriteLine("El libro escogido es {0} {1} {2} {3} {4}", idLibro,)
              
            }
            catch ()
            {

            }
            return idLibro;
        }
        List<LibroDto> InterfazConsultas.InsertarLirbo(NpgsqlConnection conexion)
        {
          long idLibro = CapturaLibro(NpgsqlConnection connexion);

           throw new NotImplementedException();
        }

        List<LibroDto> InterfazConsultas.ListarLibro(NpgsqlConnection conexion)
        {
            throw new NotImplementedException();
        }      

        List<LibroDto> InterfazConsultas.BorrarLirbo(NpgsqlConnection conexion)
        {
            throw new NotImplementedException();
        }

        List<LibroDto> InterfazConsultas.ModificarLibro(NpgsqlConnection conexion)
        {
            throw new NotImplementedException();
        }
        List<LibroDto> InterfazConsultas.SeccionarTodosLibros(NpgsqlConnection conexion)
        {
           
            List<LibroDto> listaLibros = new List<LibroDto>();
            try
            {
                //Se define y ejecuta la consulta Select
                 consulta = new NpgsqlCommand("SELECT * FROM \"gbp_almacen\".\"gbp_alm_cat_libros\"", conexion);
                 consulta.ExecuteReader();

                //Paso de DataReader a lista de alumnoDTO
                listaLibros = aDto.readerALibroDto(resultadoConsulta);
                int cont = listaLibros.Count();
                Console.WriteLine("[INFORMACIÓN-ConsultasPostgresqlImplementacion-seccionarTodosLibros] Número de libros: " + cont);

                Console.WriteLine("[INFORMACIÓN-ConsultasPostgresqlImplementacion-seccionarTodosLibros] Cierre conexión y conjunto de datos");
                conexion.Close();
                resultadoConsulta.Close();

            }
            catch (Exception e)
            {

                Console.WriteLine("[ERROR-ConsultasPostgresqlImplementacion-seccionarTodosLibros] Error al ejecutar consulta: " + e);
                conexion.Close();

            }
            return listaLibros;
        }

    }
}
