using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaWPF
{
    public enum Estado { Nada, Rojo, Azul}
    class CuatroEnLinea
    {
        public Estado[,] Tablero { get; private set; }

        public CuatroEnLinea(int filas, int columnas)
        {
            //Tablero vacio
            Tablero = new Estado[filas, columnas];
            for(int i=0; i<this.Tablero.GetLength(0); i++)
            {
                for(int j=0; j<this.Tablero.GetLength(1); j++)
                {
                    this.Tablero[i, j] = Estado.Nada;
                }
            }
        }

        public bool Empate()
        {
            for(int columnas = 0; columnas < this.Tablero.GetLength(1); columnas++)
            {
                if(Tablero[0,columnas] == Estado.Nada)
                {
                    return false;
                }
            }
            return true;
        }

        public Estado Ganador()
        {
            for(int filas=0; filas < this.Tablero.GetLength(0); filas++)
            {
                for(int columnas=0; columnas < this.Tablero.GetLength(1); columnas++)
                {
                    if(Tablero[filas,columnas] != Estado.Nada && (Vertical(filas,columnas) || Horizontal(filas,columnas) || DiagonalAdelante(filas,columnas) || DiagonalAtras(filas, columnas)))
                    {
                        return Tablero[filas, columnas];
                    }
                }
            }
            return Estado.Nada;
        }

        public bool Colocar(Estado estado, int columna)
        {
            //Empieza desde abajo
            for(int fila = Tablero.GetLength(0) - 1; fila >= 0; fila--)
            {
                if(Tablero[fila,columna] == Estado.Nada)
                {
                    Tablero[fila, columna] = estado;
                    return true;
                }
            }
            return false;
        }

        public int PiezasEnColumna(int columna)
        {
            int numeroDePiezas = 0;
            for(int fila = Tablero.GetLength(0) - 1; fila >= 0; fila--)
            {
                if(Tablero[fila,columna] != Estado.Nada)
                {
                    numeroDePiezas++;
                }
            }
            return numeroDePiezas;
        }

        //Condiciones de victoria
        private bool Vertical(int fila, int columna)
        {
            if(Tablero[fila,columna] == Estado.Nada)
            {
                return false;
            }
            int cont = 1;
            int filaCursor = fila - 1;
            while(filaCursor >= 0 && Tablero[filaCursor,columna] == Tablero[fila, columna])
            {
                cont++;
                filaCursor--;
            }
            filaCursor = fila + 1;
            while(filaCursor < Tablero.GetLength(0) && Tablero[filaCursor, columna] == Tablero[fila, columna])
            {
                cont++;
                filaCursor++;
            }
            if(cont < 4)
            {
                return false;
            }
            return true;
        }

        private bool Horizontal(int fila, int columna)
        {
            if (Tablero[fila, columna] == Estado.Nada)
            {
                return false;
            }
            int cont = 1;
            int colCursor = columna - 1;
            while (colCursor >= 0 && Tablero[fila, colCursor] == Tablero[fila, columna])
            {
                cont++;
                colCursor--;
            }
            colCursor = columna + 1;
            while (colCursor < Tablero.GetLength(1) && Tablero[fila, colCursor] == Tablero[fila, columna])
            {
                cont++;
                colCursor++;
            }
            if (cont < 4)
                return false;
            return true;
        }

        private bool DiagonalAdelante(int fila, int columna)
        {
            if (Tablero[fila, columna] == Estado.Nada)
            {
                return false;
            }
            int cont = 1;
            int filaCursor = fila - 1;
            int colCursor = columna + 1;
            while (filaCursor >= 0 && colCursor < Tablero.GetLength(1) && Tablero[filaCursor, colCursor] == Tablero[fila, columna])
            {
                cont++;
                filaCursor--;
                colCursor++;
            }
            filaCursor = fila + 1;
            colCursor = columna - 1;
            while (filaCursor < Tablero.GetLength(0) && colCursor >= 0 && Tablero[filaCursor, colCursor] == Tablero[fila, columna])
            {
                cont++;
                filaCursor++;
                colCursor--;
            }
            if (cont < 4)
            {
                return false;
            }
            return true;
        }

        private bool DiagonalAtras(int fila, int columna)
        {
            if (Tablero[fila, columna] == Estado.Nada)
            {
                return false;
            }
            int count = 1;
            int rowCursor = fila + 1;
            int colCursor = columna + 1;
            while (rowCursor < Tablero.GetLength(0) && colCursor < Tablero.GetLength(1) && Tablero[rowCursor, colCursor] == Tablero[fila, columna])
            {
                count++;
                rowCursor++;
                colCursor++;
            }
            rowCursor = fila - 1;
            colCursor = columna - 1;
            while (rowCursor >= 0 && colCursor >= 0 && Tablero[rowCursor, colCursor] == Tablero[fila, columna])
            {
                count++;
                rowCursor--;
                colCursor--;
            }
            if (count < 4)
                return false;
            return true;
        }

    }
}
