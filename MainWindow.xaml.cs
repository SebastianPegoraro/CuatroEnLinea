using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PruebaWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int tamanioCirculo = 80;

        private CuatroEnLinea juego;
        private DispatcherTimer animacion;
        private bool bloquearInput;

        private Estado estadoActual;
        private Ellipse circuloActual;
        private int columnaActual;

        public MainWindow()
        {
            InitializeComponent();
            NuevoJuego();
        }

        private void NuevoJuego()
        {
            bloquearInput = true;
            juego = new CuatroEnLinea(6, 7);
            estadoActual = Estado.Rojo;
            animacion = new DispatcherTimer();
            animacion.Interval = new TimeSpan(0, 0, 0, 0, 15);
            animacion.Start();
            GameCanvas.Children.Clear();
            DibujarFondo();
            bloquearInput = false;
            PermitirTodosInsertButtons();
        }

        private void DibujarFondo()
        {
            for(int fila=0; fila < juego.Tablero.GetLength(0); fila++)
            {
                for(int columna=0; columna<juego.Tablero.GetLength(1); columna++)
                {
                    Rectangle cuadrado = new Rectangle();
                    cuadrado.Width = tamanioCirculo;
                    cuadrado.Height = tamanioCirculo;
                    cuadrado.Fill = (columna % 2 == 0) ? Brushes.White : Brushes.LightGray;
                    Canvas.SetBottom(cuadrado, tamanioCirculo * fila);
                    Canvas.SetRight(cuadrado, tamanioCirculo * columna);
                    GameCanvas.Children.Add(cuadrado);
                }
            }
        }

        private void PermitirTodosInsertButtons()
        {
            InsertButton0.IsEnabled = true;
            InsertButton1.IsEnabled = true;
            InsertButton2.IsEnabled = true;
            InsertButton3.IsEnabled = true;
            InsertButton4.IsEnabled = true;
            InsertButton5.IsEnabled = true;
            InsertButton6.IsEnabled = true;
        }

        private void InsertButton_Click(int columna)
        {
            if (bloquearInput == false)
            {
                bool success = juego.Colocar(estadoActual, columna);
                if (success)
                {
                    columnaActual = columna;
                    DibujarCirculo(estadoActual, columna);
                    FinDelTurno();
                }
            }

        }

        private void DibujarCirculo(Estado estado, int columna)
        {
            bloquearInput = true;

            Ellipse circulo = new Ellipse();
            circulo.Height = tamanioCirculo;
            circulo.Width = tamanioCirculo;
            circulo.Fill = (estado == Estado.Rojo) ? Brushes.Red : Brushes.Blue;
            Canvas.SetTop(circulo, 0);
            Canvas.SetLeft(circulo, columna * 80);
            GameCanvas.Children.Add(circulo);
            circuloActual = circulo;
            animacion.Tick += AnimacionCaidaCirculo;
        }

        private void AnimacionCaidaCirculo(object sender, EventArgs e)
        {
            int dropLength = tamanioCirculo * (juego.Tablero.GetLength(1) - 1 - juego.PiezasEnColumna(columnaActual));
            int dropRate = 40;
            if (Canvas.GetTop(circuloActual) < dropLength)
            {
                Canvas.SetTop(circuloActual, Canvas.GetTop(circuloActual) + dropRate);
            }
            else
            {
                animacion.Tick -= AnimacionCaidaCirculo;
                bloquearInput = false;
            }

        }

        private void FinDelTurno()
        {
            Estado winner = juego.Ganador();

            if (winner != Estado.Nada)
            {
                StatusText.Text = String.Format("Jugador {0} Gana!", estadoActual);
                DenegarTodosLosInsertButtons();
            }
            else if (juego.Empate())
            {
                StatusText.Text = "Juego Empatado!";
                DenegarTodosLosInsertButtons();
            }
            else
            {
                estadoActual = (estadoActual == Estado.Azul) ? Estado.Rojo : Estado.Azul;
                StatusText.Text = String.Format("Turno de {0}", estadoActual);
            }
        }

        private void DenegarTodosLosInsertButtons()
        {
            InsertButton0.IsEnabled = false;
            InsertButton1.IsEnabled = false;
            InsertButton2.IsEnabled = false;
            InsertButton3.IsEnabled = false;
            InsertButton4.IsEnabled = false;
            InsertButton5.IsEnabled = false;
            InsertButton6.IsEnabled = false;
        }

        private void InsertButton0_Click(object sender, RoutedEventArgs e)
        {
            InsertButton_Click(0);
        }

        private void InsertButton1_Click(object sender, RoutedEventArgs e)
        {
            InsertButton_Click(1);
        }

        private void InsertButton2_Click(object sender, RoutedEventArgs e)
        {
            InsertButton_Click(2);
        }

        private void InsertButton3_Click(object sender, RoutedEventArgs e)
        {
            InsertButton_Click(3);
        }

        private void InsertButton4_Click(object sender, RoutedEventArgs e)
        {
            InsertButton_Click(4);
        }

        private void InsertButton5_Click(object sender, RoutedEventArgs e)
        {
            InsertButton_Click(5);
        }

        private void InsertButton6_Click(object sender, RoutedEventArgs e)
        {
            InsertButton_Click(6);
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            NuevoJuego();
        }
    }
}
