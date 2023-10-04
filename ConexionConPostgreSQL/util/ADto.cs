using ConexionConPostgreSQL.Dtos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionConPostgreSQL.Util
{
    internal class ADto
    { /// <summary>S
      /// Métodos que pasan a objeto de tipo DTO
      /// </summary>
        
        public List<LibroDto> readerALibroDto(NpgsqlDataReader resultadoConsulta)
        {
            List<LibroDto> listaLibros = new List<LibroDto>();
            while (resultadoConsulta.Read())
            {
                listaLibros.Add(new LibroDto(
                    long.Parse(resultadoConsulta[0].ToString()),
                    resultadoConsulta[1].ToString(),
                    resultadoConsulta[2].ToString(),
                    resultadoConsulta[3].ToString(),
                    (int)Int64.Parse(resultadoConsulta[4].ToString())
                    )
                    );

            }
            return listaLibros;
        }
        public LibroDto readerLibroSeleccionado(NpgsqlDataReader resultadoConsulta)
        {
            if (resultadoConsulta.Read())
            {
                LibroDto libroSeleccionado = readerLibroSeleccionado(resultadoConsulta);
                Console.WriteLine("Información del libro escogido: ");
                Console.WriteLine("ID: " + libroSeleccionado.Id_libro);
                Console.WriteLine("Título: " + libroSeleccionado.Titulo);
                Console.WriteLine("Autor: " + libroSeleccionado.Autor);
                Console.WriteLine("ISBN: " + libroSeleccionado.Isbn);
                Console.WriteLine("Edicion: " + libroSeleccionado.Edicion);
            }
            else
            {
                Console.WriteLine("No se encontró ningún libro con la ID especificada.");
            }
       
    }

    }
}
