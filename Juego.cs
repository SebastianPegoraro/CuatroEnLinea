using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPfinal
{
    class Juego
    {
        public Tablero tablero;
        public Juego(Tablero tablero)
        {
            this.tablero = tablero;
        }

        public bool Empate()
        {
            for (int columnas = 0; columnas < tablero.matriz.GetLength(1); columnas++)
            {
                if (tablero.matriz[0, columnas] == Estado.Nada)
                {
                    return false;
                }
            }
            return true;
        }

        public Estado Ganador()
        {
            for (int filas = 0; filas < tablero.matriz.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < tablero.matriz.GetLength(1); columnas++)
                {
                    if (tablero.matriz[filas, columnas] != Estado.Nada && (Vertical(filas, columnas) || Horizontal(filas, columnas) || DiagonalAdelante(filas, columnas) || DiagonalAtras(filas, columnas)))
                    {
                        return tablero.matriz[filas, columnas];
                    }
                }
            }
            return Estado.Nada;
        }

        public bool Colocar(Estado estado, int columna)
        {
            //Empieza desde abajo
            for (int fila = tablero.matriz.GetLength(0) - 1; fila >= 0; fila--)
            {
                if (tablero.matriz[fila, columna] == Estado.Nada)
                {
                    tablero.matriz[fila, columna] = estado;
                    return true;
                }
            }
            return false;
        }

        public int PiezasEnColumna(int columna)
        {
            int numeroDePiezas = 0;
            for (int fila = tablero.matriz.GetLength(0) - 1; fila >= 0; fila--)
            {
                if (tablero.matriz[fila, columna] != Estado.Nada)
                {
                    numeroDePiezas++;
                }
            }
            return numeroDePiezas;
        }

        //Condiciones de victoria
        private bool Vertical(int fila, int columna)
        {
            if (tablero.matriz[fila, columna] == Estado.Nada)
            {
                return false;
            }
            int cont = 1;
            int filaCursor = fila - 1;
            while (filaCursor >= 0 && tablero.matriz[filaCursor, columna] == tablero.matriz[fila, columna])
            {
                cont++;
                filaCursor--;
            }
            filaCursor = fila + 1;
            while (filaCursor < tablero.matriz.GetLength(0) && tablero.matriz[filaCursor, columna] == tablero.matriz[fila, columna])
            {
                cont++;
                filaCursor++;
            }
            if (cont < 4)
            {
                return false;
            }
            return true;
        }

        private bool Horizontal(int fila, int columna)
        {
            if (tablero.matriz[fila, columna] == Estado.Nada)
            {
                return false;
            }
            int cont = 1;
            int colCursor = columna - 1;
            while (colCursor >= 0 && tablero.matriz[fila, colCursor] == tablero.matriz[fila, columna])
            {
                cont++;
                colCursor--;
            }
            colCursor = columna + 1;
            while (colCursor < tablero.matriz.GetLength(1) && tablero.matriz[fila, colCursor] == tablero.matriz[fila, columna])
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
            if (tablero.matriz[fila, columna] == Estado.Nada)
            {
                return false;
            }
            int cont = 1;
            int filaCursor = fila - 1;
            int colCursor = columna + 1;
            while (filaCursor >= 0 && colCursor < tablero.matriz.GetLength(1) && tablero.matriz[filaCursor, colCursor] == tablero.matriz[fila, columna])
            {
                cont++;
                filaCursor--;
                colCursor++;
            }
            filaCursor = fila + 1;
            colCursor = columna - 1;
            while (filaCursor < tablero.matriz.GetLength(0) && colCursor >= 0 && tablero.matriz[filaCursor, colCursor] == tablero.matriz[fila, columna])
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
            if (tablero.matriz[fila, columna] == Estado.Nada)
            {
                return false;
            }
            int count = 1;
            int rowCursor = fila + 1;
            int colCursor = columna + 1;
            while (rowCursor < tablero.matriz.GetLength(0) && colCursor < tablero.matriz.GetLength(1) && tablero.matriz[rowCursor, colCursor] == tablero.matriz[fila, columna])
            {
                count++;
                rowCursor++;
                colCursor++;
            }
            rowCursor = fila - 1;
            colCursor = columna - 1;
            while (rowCursor >= 0 && colCursor >= 0 && tablero.matriz[rowCursor, colCursor] == tablero.matriz[fila, columna])
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

