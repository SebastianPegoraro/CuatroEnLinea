using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteWPF
{
    public class Tablero
    {
        public Ficha[][] matriz;

        public int filas { get; set; }

        public int columnas { get; set; }


        public Tablero()
        {
            this.filas = 6;
            this.columnas = 7;
        }

        

    }
}
