using ConexionConPostgreSQL.Conexion;
using ConexionConPostgreSQL.Dtos;
using ConexionConPostgreSQL.Util;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionConPostgreSQL.Servicios
{
    internal class ImplementacionConsultas : InterfazConsultas
    {
        LibroDto libroDto = new LibroDto();
        ADto aDto = new ADto();
        InterfazConexion conexion;
        NpgsqlCommand consulta;
        NpgsqlCommand comando;
        SqlDataReader resultadoConsulta = null;

        List<LibroDto> InterfazConsultas.SeleccionarLibros(NpgsqlConnection conexion)
        {
            List<LibroDto> listaLibrosObtenida = new List<LibroDto>();
            try
            {
                //Se define y ejecuta la consulta Select
                consulta = new NpgsqlCommand("SELECT * FROM \"gbp_almacen\".\"gbp_alm_cat_libros\"", conexion);
                NpgsqlDataReader resultadoConsultado = consulta.ExecuteReader();


                // Llamada a la conversión a dtoAlumno
                listaLibrosObtenida = aDto.ResultadoALibrosDto(resultadoConsultado);

                resultadoConsultado.Close();

            }
            catch (SqlException e)
            {
                Console.WriteLine("[ERROR-ConsultasImplementacion-ListarLibros] Error generando o ejecutando el listado del catálogo: " + e);
            }

            return listaLibrosObtenida;
        }
        LibroDto InterfazConsultas.SeleccionarUnLibro(NpgsqlConnection conexion, string isbnAConsultar)
        {
            List<LibroDto> listaLibrosObtenida = new List<LibroDto>();

            try
            {
                consulta = new NpgsqlCommand($"SELECT * FROM \"gbp_almacen\".\"gbp_alm_cat_libros\" WHERE isbn = @isbn", conexion);
                consulta.Parameters.AddWithValue("@isbn", isbnAConsultar);
                NpgsqlDataReader resultadoConsultado = consulta.ExecuteReader();

                listaLibrosObtenida = aDto.ResultadoALibrosDto(resultadoConsultado);
                resultadoConsultado.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("[ERROR-ConsultasImplementacion-ListarLibros] Error generando o ejecutando el listado del catálogo opción particular: " + e);
            }
            return listaLibrosObtenida[0];
        }
        void InterfazConsultas.InsertarLirbo(NpgsqlConnection conexion)
        {
            List<LibroDto> listaLibrosAInsertar = new List<LibroDto>();
            try
            {
                string query = "INSERT INTO \"gbp_almacen\".\"gbp_alm_cat_libros\" (titulo, autor, isbn, edicion) " +
                               "VALUES (@titulo, @autor, @isbn, @edicion)";

                string respuesta;
                do
                {
                    Console.WriteLine("\t\nDígame el libro que desea insertar");
                    Console.Write("\t\nDígame el título del libro nuevo: ");
                    string titulo = Console.ReadLine();
                    Console.Write("\t\nDígame el autor del libro nuevo: ");
                    string autor = Console.ReadLine();
                    Console.Write("\t\nDígame el ISBN del libro nuevo: ");
                    string isbn = Console.ReadLine();
                    Console.Write("\t\nDígame la edición del libro nuevo: ");
                    int edicion = Convert.ToInt32(Console.ReadLine());

                    LibroDto libroDto = new LibroDto(titulo, autor, isbn, edicion);
                    listaLibrosAInsertar.Add(libroDto);

                    Console.Write("\t\n¿Quiere insertar más libros? (s/n): ");
                    respuesta = Console.ReadLine().Trim().ToLower();
                } while (respuesta == "s");

                // Insertar todos los libros en la base de datos
                using (NpgsqlCommand comando = new NpgsqlCommand(query, conexion))
                {
                    foreach (var libro in listaLibrosAInsertar)
                    {
                        comando.Parameters.Clear();
                        comando.Parameters.AddWithValue("titulo", libro.Titulo);
                        comando.Parameters.AddWithValue("autor", libro.Autor);
                        comando.Parameters.AddWithValue("isbn", libro.Isbn);
                        comando.Parameters.AddWithValue("edicion", libro.Edicion);

                        int filasAfectadas = comando.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                        {
                            Console.WriteLine("[INFORMACIÓN-ConexionPostgresqlImplementacion-InsertarLibros] Libro insertado correctamente.");
                        }
                        else
                        {
                            Console.WriteLine("[INFORMACIÓN-ConexionPostgresqlImplementacion-InsertarLibros] Error: No se pudo insertar el libro.");
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("[INFORMACIÓN-ConexionPostgresqlImplementacion-InsertarLibros] Error de PostgreSQL: " + ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("[INFORMACIÓN-ConexionPostgresqlImplementacion-InsertarLibros] Error: Ingrese un formato de número válido para la edición.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[INFORMACIÓN-ConexionPostgresqlImplementacion-InsertarLibros] Error: " + ex.Message);
            }
        }

        void ModificarManuel(LibroDto libro)
        {

        }

        void InterfazConsultas.ModificarLibro(NpgsqlConnection conexion, string isbnAConsultar)
        {
            try
            {
                bool esOk = false;

                Console.Write($"Título: {libroDto.Titulo}");
                Console.Write($"Autor: {libroDto.Autor}");
                Console.Write($"Isbn: {libroDto.Isbn}");
                Console.WriteLine($"Edición: {libroDto.Edicion}");

                Console.WriteLine("¿Qué deseas modificar? Escriba la opcion: (titulo/autor/isbn/edicion/nada)");
                string opcion = Console.ReadLine().ToLower();
                if (opcion.Length < 9)
                    esOk = true;
                else
                {
                    Console.WriteLine("[INFORMACIÓN-Menu-Modificar] Error: Por favor, introduzca 'si' o 'no' (sin comillas) como respuesta.");
                }

                if (esOk == true)
                {
                    esOk = false;
                    do
                    {
                        switch (opcion)
                        {

                            case "titulo":
                                Console.WriteLine("Introduce el nuevo título:");
                                string nuevoTitulo = Console.ReadLine();
                                // Realizar la actualización en la base de datos
                                string queryTitulo = $"UPDATE libros SET titulo = @nuevoTitulo WHERE isbn = @Isbn";
                                consulta = new NpgsqlCommand(queryTitulo);
                                consulta.Parameters.AddWithValue("@Isbn", isbnAConsultar);
                                consulta.Parameters.AddWithValue("@nuevoTitulo", nuevoTitulo);
                                break;
                            case "autor":
                                Console.WriteLine("Introduce el nuevo autor: ");
                                string nuevoAutor = Console.ReadLine();
                                // Realizar la actualización en la base de datos
                                string queryAutor = $"UPDATE libros SET autor =  @nuevoAutor WHERE isbn = @Isbn";
                                consulta = new NpgsqlCommand(queryAutor);
                                consulta.Parameters.AddWithValue("@Isbn", isbnAConsultar);
                                consulta.Parameters.AddWithValue("@nuevoAutor", nuevoAutor);
                                break;
                            case "isbn":
                                Console.WriteLine("Introduce el nuevo Isbn: ");
                                string nuevoISBN = Console.ReadLine();
                                string queryIsbn = $"UPDATE libros SET isbn = @nuevoISBN WHERE isbn = @Isbn";
                                consulta = new NpgsqlCommand(queryIsbn);
                                consulta.Parameters.AddWithValue("@Isbn", isbnAConsultar);
                                consulta.Parameters.AddWithValue("@nuevoISBN", nuevoISBN);
                                break;
                            case "edicion":
                                Console.WriteLine("Introduce la nueva edición:");
                                int nuevaEdicion = Convert.ToInt32(Console.ReadLine());
                                string queryEdicion = $"UPDATE libros SET edicion = @nuevaEdicion WHERE id = @Isbn";
                                consulta = new NpgsqlCommand(queryEdicion);
                                consulta.Parameters.AddWithValue("@Isbn", isbnAConsultar);
                                consulta.Parameters.AddWithValue("@nuevaEdicion", nuevaEdicion);
                                break;
                            case "salir":
                                Console.WriteLine("Salinedo a Menu");
                                esOk = true;
                                break;
                            default:
                                Console.WriteLine("Opción no válida.");
                                break;
                        }
                    } while (esOk == true);

                }

            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message + "[INFORMACIÓN-Menu-Modificar-NpgsqlException]");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message + " [INFORMACIÓN-Menu-Modificar-IndexOutOfRangeException]");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " [INFORMACIÓN-Menu-Modificar-Exception]");
            }
        }
        void InterfazConsultas.BorrarLirbo(NpgsqlConnection conexion)
        {
            try
            {
                Console.WriteLine("Estas seguro que deseas Borrar un libro");
                Console.Write("\t\n¿Quiere insertar más libros? (s/n): ");
                string respuesta = Console.ReadLine().Trim().ToLower();
                if ((respuesta.Length == 1 )&& (respuesta.Equals("s") )){ 
                Console.WriteLine("Introduce el ISBN del libro que deseas borrar:");
                string isbnABorrar = Console.ReadLine();

                string queryBorrar = $"DELETE FROM \"gbp_almacen\".\"gbp_alm_cat_libros\" WHERE isbn = @Isbn";
                NpgsqlCommand consulta = new NpgsqlCommand(queryBorrar, conexion);
                consulta.Parameters.AddWithValue("@Isbn", isbnABorrar);
                int filasAfectadas = consulta.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    Console.WriteLine($"Libro con ISBN {isbnABorrar} eliminado correctamente.");
                }
                else
                {
                    Console.WriteLine($"No se encontró ningún libro con ISBN {isbnABorrar} para eliminar.");
                }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al intentar eliminar el libro: {ex.Message}");
            }
        }
    }
}







