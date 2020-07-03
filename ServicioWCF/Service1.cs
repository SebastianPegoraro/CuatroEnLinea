using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ClienteWPF;

namespace ServicioWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código y en el archivo de configuración a la vez.
    [Serializable]
    public class Service1 : IService1
    {

        public Tablero ProbarTablero(Tablero tablero)
        {
            return tablero;
        }
        public Ficha[][] IniciarTablero(Ficha[][] matriz, Ficha fichaVacia)
        {

            int filas = 6;
            int columnas = 7;
            Ficha[][] tablero = new Ficha[filas][];
            //Tablero vacio
            for (int i = 0; i < filas; i++)
            {
                tablero[i] = new Ficha[columnas];
                for (int j = 0; j < columnas; j++)
                {
                    tablero[i][j] = fichaVacia;
                }
            }
            return tablero;
        }
        public int PiezasEnColumna(Ficha[][] tableroM, int columna, Ficha fichaVacia)
        {
            Ficha[,] tablero = Estatica.AMultiDim(tableroM);
            int numeroDePiezas = 0;
            int filas = 6;
            for (int fila = filas - 1; fila >= 0; fila--)
            {
                if (tablero[fila,columna].Id != fichaVacia.Id)
                {
                    numeroDePiezas++;
                }
            }
            return numeroDePiezas;
        }

        public bool VerificarLugar(Ficha[][] tablero, int columna, Ficha fichaVacia, Jugador jugadorActual)
        {
            //Empieza desde abajo
            for (int fila = tablero.GetLength(0) - 1; fila >= 0; fila--)
            {
                if (tablero[fila][columna].Id == fichaVacia.Id)
                {
                    return true;
                }
            }
            return false;
        }
        public Ficha[][] Colocar(Ficha[][] tablero, int columna, Ficha fichaVacia, Jugador jugadorActual)
        {
            //Empieza desde abajo
            for (int fila = tablero.GetLength(0) - 1; fila >= 0; fila--)
            {
                if (tablero[fila][columna].Id == fichaVacia.Id)
                {
                    tablero[fila][columna] = jugadorActual.Ficha;
                    return tablero;
                }
            }
            return tablero;
        }

        public Jugador CambiarTurnoJugador(Jugador jugador1, Jugador jugador2, Jugador jugadorActual)
        {
            return (jugadorActual.Id == jugador2.Id) ? jugador1 : jugador2;
        }

        public string FinDeTurno(Ficha[][] tableroM, Ficha fichaVacia, Jugador jugadorActual)
        {
            Ficha[,] tablero = Estatica.AMultiDim(tableroM);

            string nombreGanador = Ganador(tablero, fichaVacia, jugadorActual);

            if (nombreGanador != "Vacia")
            {
                return nombreGanador;
            }
            else if (Empate(tablero, fichaVacia))
            {
                return "empate";
            }
            else
            {
                return "continua";
            }
        }
        public string Ganador(Ficha[,] tablero, Ficha fichaVacia, Jugador jugadorActual)
        {
            for (int filas = 0; filas < tablero.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < tablero.GetLength(1); columnas++)
                {
                    if (tablero[filas,columnas] != fichaVacia && (Vertical(tablero, fichaVacia, filas, columnas) || Horizontal(tablero, fichaVacia, filas, columnas) || DiagonalAdelante(tablero, fichaVacia, filas, columnas) || DiagonalAtras(tablero, fichaVacia, filas, columnas)))
                    {
                        return jugadorActual.Nombre;
                    }
                }
            }
            return fichaVacia.Color;
        }
        public bool Empate(Ficha[,] tablero, Ficha fichaVacia)
        {
            for (int columnas = 0; columnas < tablero.GetLength(1); columnas++)
            {
                if (tablero[0,columnas].Id == fichaVacia.Id)
                {
                    return false;
                }
            }
            return true;
        }
        //Condiciones de victoria
        public bool Vertical(Ficha[,] tablero, Ficha fichaVacia, int fila, int columna)
        {
            if (tablero[fila,columna].Id == fichaVacia.Id)
            {
                return false;
            }
            int cont = 1;
            int filaCursor = fila - 1;
            while (filaCursor >= 0 && tablero[filaCursor,columna].Id == tablero[fila,columna].Id)
            {
                cont++;
                filaCursor--;
            }
            filaCursor = fila + 1;
            while (filaCursor < tablero.GetLength(0) && tablero[filaCursor,columna].Id == tablero[fila,columna].Id)
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
        public bool Horizontal(Ficha[,] tablero, Ficha fichaVacia, int fila, int columna)
        {
            if (tablero[fila,columna].Id == fichaVacia.Id)
            {
                return false;
            }
            int cont = 1;
            int colCursor = columna - 1;
            while (colCursor >= 0 && tablero[fila,colCursor].Id == tablero[fila,columna].Id)
            {
                cont++;
                colCursor--;
            }
            colCursor = columna + 1;
            while (colCursor < tablero.GetLength(1) && tablero[fila,colCursor].Id == tablero[fila,columna].Id)
            {
                cont++;
                colCursor++;
            }
            if (cont < 4)
                return false;
            return true;
        }
        public bool DiagonalAdelante(Ficha[,] tablero, Ficha fichaVacia, int fila, int columna)
        {
            if (tablero[fila,columna].Id == fichaVacia.Id)
            {
                return false;
            }
            int cont = 1;
            int filaCursor = fila - 1;
            int colCursor = columna + 1;
            while (filaCursor >= 0 && colCursor < tablero.GetLength(1) && tablero[filaCursor,colCursor].Id == tablero[fila,columna].Id)
            {
                cont++;
                filaCursor--;
                colCursor++;
            }
            filaCursor = fila + 1;
            colCursor = columna - 1;
            while (filaCursor < tablero.GetLength(0) && colCursor >= 0 && tablero[filaCursor,colCursor].Id == tablero[fila,columna].Id)
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
        public bool DiagonalAtras(Ficha[,] tablero, Ficha fichaVacia, int fila, int columna)
        {
            if (tablero[fila,columna].Id == fichaVacia.Id)
            {
                return false;
            }
            int count = 1;
            int rowCursor = fila + 1;
            int colCursor = columna + 1;
            while (rowCursor < tablero.GetLength(0) && colCursor < tablero.GetLength(1) && tablero[rowCursor,colCursor].Id == tablero[fila,columna].Id)
            {
                count++;
                rowCursor++;
                colCursor++;
            }
            rowCursor = fila - 1;
            colCursor = columna - 1;
            while (rowCursor >= 0 && colCursor >= 0 && tablero[rowCursor,colCursor].Id == tablero[fila,columna].Id)
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
