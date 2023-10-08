using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionConPostgreSQL.Servicios
{
    internal class ImplementacionMenu : InterfazMenu
    {
        public void MostrarMenu()
        {
            Console.WriteLine("Opciones de Menu");
            Console.WriteLine("----------------");
            Console.WriteLine("1. Seleccionar todos los libros.");
            Console.WriteLine("2. Seleccionar un lirbo.");            
            Console.WriteLine("3. Insertar libro.");
            Console.WriteLine("4. Modificar libro.");
            Console.WriteLine("5. Borrar libro.");
            Console.WriteLine("0. Salir.");
        }
    }
}
