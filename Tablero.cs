using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPfinal
{
    public enum Estado { Nada,uno,dos}
    class Tablero
    {
        public Estado[,] matriz { get; private set; }

        public Tablero(int filas, int columnas)
        {
            //Tablero vacio
            matriz = new Estado[filas, columnas];
            for (int i = 0; i < this.matriz.GetLength(0); i++)
            {
                for (int j = 0; j < this.matriz.GetLength(1); j++)
                {
                    this.matriz[i, j] = Estado.Nada;
                }
            }
        }
    }
}
