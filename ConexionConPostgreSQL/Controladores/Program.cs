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
            InterfazConexion ImpConexion = new ImplementacionConexion();
            InterfazConsultas ImpConsulta = new ImplementacionConsultas();
            InterfazMenu IntMenu = new ImplementacionMenu();
            NpgsqlConnection conexion = null;
            List<LibroDto> listaLibros = new List<LibroDto>();
            conexion = ImpConexion.EstableceConexion();
            bool continuarConsultando = false;
            bool opcionValida = false;
            bool esSi = false;
            string isbnAConsultar, ok="";
            int opcion;
            int opcionSeleccionadaUsuario;
            
            // Menú donde elegimos la opción y mandará mensaje según lo que suceda
            do
            {
                try
                {
                    // Mostramos el menú
                    IntMenu.MostrarMenu();
                    Console.WriteLine("Elige una opción");
                    opcion = Convert.ToInt32(Console.ReadLine());

                    switch (opcion)
                    {
                        case 0:
                            opcionValida = true;
                            Console.WriteLine("Adiós");
                            break;
                        case 1:
                            isbnAConsultar = "";
                            listaLibros.Clear();
                            listaLibros = ImpConsulta.SeleccionarLibros(conexion);
                            for(int i = 0; i< listaLibros.Count; i++)
                            {
                                Console.WriteLine("--------------------------------------------------");
                                Console.WriteLine("Id: " + listaLibros[i].Id_libro);
                                Console.WriteLine("Titulo: " + listaLibros[i].Titulo);
                                Console.WriteLine("Autor: " + listaLibros[i].Autor);
                                Console.WriteLine("ISBN: " + listaLibros[i].Isbn);
                                Console.WriteLine("Edición: " + listaLibros[i].Edicion);
                            }
                            Console.WriteLine("--------------------------------------------------");
                            break;
                        case 2:
                            
                            Console.WriteLine("[INFO-Main] LISTAR UN LIBRO");                            
                           
                                Console.WriteLine("Dime el ISBN del libro a consultar");                               
                                isbnAConsultar = Console.ReadLine();
                                LibroDto libro = ImpConsulta.SeleccionarUnLibro(conexion, isbnAConsultar);
                            Console.WriteLine("el libro seleccionado es: idLibro: {0}, titulo:{1}, autor: {2}, isbn: {3}, edicion: {4}.", libro.Id_libro, libro.Titulo, libro.Autor, libro.Isbn, libro.Edicion);
                            break;
                        case 3:
                            ImpConsulta.InsertarLirbo(conexion);
                            break;
                        case 4:
                            isbnAConsultar = "";
                            Console.WriteLine("Digame el ISBN del libro que desea modificar");                            
                            isbnAConsultar = Console.ReadLine();
                            ImpConsulta.ModificarLibro(conexion, isbnAConsultar);

                                break;
                        case 5:
                            ImpConsulta.BorrarLirbo(conexion);
                            break;
                        default:
                            Console.WriteLine("[INFORMACIÓN-Menu]Error: Opción inválida. Por favor, introduce un número válido.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Debes introducir un número.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Se produjo un error: " + e.Message);
                }
            } while (!opcionValida);

            /*
            if (conexion != null)
            {
                foreach (LibroDto libro in IntConsulta.SeleccionarLibros(conexion, listaLibros, isbnAConsultar)
                {
                    Console.WriteLine(libro.Titulo);
                }

            }*/

            Console.ReadLine();


        }
        private static void ListarLibrosObtenida(List<LibroDto> listaLibrosObtenida)
        {
            if (listaLibrosObtenida.Count > 0)
            {
                for (int i = 0; i < listaLibrosObtenida.Count; i++)
                {
                    Console.WriteLine(listaLibrosObtenida[i].ToString());
                }
            }
            else
            {
                Console.WriteLine("[INFO-Main] No hay libros en el catálogo");
            }
        }

    }
}


