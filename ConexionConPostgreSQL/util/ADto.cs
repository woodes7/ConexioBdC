﻿using ConexionConPostgreSQL.Dtos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionConPostgreSQL.Util
{
    internal class ADto
    { /// <summary>S
      /// Métodos que pasan a objeto de tipo DTO
      /// </summary>

        public List<LibroDto> ResultadoALibrosDto(NpgsqlDataReader resultadoConsulta)
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

        
    }
}


