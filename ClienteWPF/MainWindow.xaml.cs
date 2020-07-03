
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
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

namespace ClienteWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random ai = new Random();

        const int tamanioCirculo = 80;
        private DispatcherTimer animacion;
        private bool bloquearInput;
        private Ellipse circuloActual;
        private int columnaActual;

        public ServiceReference1.Tablero tableroService;
        public ServiceReference1.Ficha fichaRojaService;
        public ServiceReference1.Ficha fichaAzulService;
        public ServiceReference1.Ficha fichaVaciaService;

        ServiceReference1.Service1Client proxy;

        
        public ServiceReference1.Jugador jugador1Service;
        public ServiceReference1.Jugador jugador2Service;
        public ServiceReference1.Jugador jugadorActualService;

        private bool vsAI;
        public bool VsAI { get => vsAI; set => vsAI = value; }

        public MainWindow()
        {
            InitializeComponent();
            InitJuego();
        }

        public void InitInstancias()
        {
            proxy = new ServiceReference1.Service1Client();
            this.fichaRojaService = new ServiceReference1.Ficha();
            fichaRojaService.Color = "Rojo";
            fichaRojaService.Id = 1;
            this.fichaAzulService = new ServiceReference1.Ficha();
            fichaAzulService.Color = "Azul";
            fichaAzulService.Id = 2;
            this.fichaVaciaService = new ServiceReference1.Ficha();
            fichaVaciaService.Color = "Vacia";
            fichaVaciaService.Id = 0;
            this.tableroService = new ServiceReference1.Tablero();
            tableroService.Columnas = 7;
            tableroService.Filas = 6;
            jugador1Service = new ServiceReference1.Jugador();
            jugador2Service = new ServiceReference1.Jugador();
            jugador1Service.Ficha = fichaRojaService;
            jugador2Service.Ficha = fichaAzulService;
            jugador1Service.Id = 1;
            jugador2Service.Id = 2;
            jugador1Service.Nombre = "Pepito";
            jugador2Service.Nombre = "Juancito";
            jugadorActualService = jugador1Service;
        }
        private void InitJuego()
        {
            InitInstancias();
            IniciarTablero();
            bloquearInput = true;
            animacion = new DispatcherTimer();
            animacion.Interval = new TimeSpan(0, 0, 0, 0, 15);
            animacion.Start();
            GameCanvas.Children.Clear();
            DibujarFondo();
            bloquearInput = false;
            PermitirTodosInsertButtons();
        }

        public void IniciarTablero()
        {
            tableroService.Matriz = proxy.IniciarTablero(tableroService.Matriz, fichaVaciaService);
            
        }

        private void DibujarFondo()
        {
            ServiceReference1.Ficha[,] tablero = Estatica.AMultiDim(tableroService.Matriz);

            for (int fila = 0; fila < tablero.GetLength(0); fila++)
            {
                for (int columna = 0; columna < tablero.GetLength(1); columna++)
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
        private void DibujarCirculo(int estado, int columna)
        {
            bloquearInput = true;

            Ellipse circulo = new Ellipse();
            circulo.Height = tamanioCirculo;
            circulo.Width = tamanioCirculo;
            circulo.Fill = (estado == 1) ? Brushes.Red : Brushes.Blue;
            Canvas.SetTop(circulo, 0);
            Canvas.SetLeft(circulo, columna * 80);
            GameCanvas.Children.Add(circulo);
            circuloActual = circulo;
            animacion.Tick += AnimacionCaidaCirculo;
        }
        private void AnimacionCaidaCirculo(object sender, EventArgs e)
        {
            int dropLength = tamanioCirculo * (tableroService.Columnas - 1 - proxy.PiezasEnColumna(tableroService.Matriz, columnaActual, fichaVaciaService));
            int dropRate = 40;
            if (Canvas.GetTop(circuloActual) < dropLength)
            {
                Canvas.SetTop(circuloActual, Canvas.GetTop(circuloActual) + dropRate);
            }
            else
            {
                animacion.Tick -= AnimacionCaidaCirculo;
                bloquearInput = false;

                if (vsAI && jugadorActualService.Id == jugador2Service.Id)
                {
                    InsertButton_Click(ai.Next(0, 7));
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

        private void InsertButton_Click(int columna)
        {
            if (bloquearInput == false)
            {
                bool success = proxy.VerificarLugar(tableroService.Matriz, columna, fichaVaciaService, jugadorActualService);
                if (success)
                {
                    tableroService.Matriz = proxy.Colocar(tableroService.Matriz, columna, fichaVaciaService, jugadorActualService);
                    columnaActual = columna;
                    DibujarCirculo(jugadorActualService.Id, columna);
                    //FinDeTurno();
                    ServiceReference1.Ficha[,] tableroF = Estatica.AMultiDim(tableroService.Matriz);

                    for (int i = 0; i < tableroF.GetLength(0); i++)
                    {
                        for (int j = 0; j < tableroF.GetLength(1); j++)
                        {
                            Console.Write(tableroF[i,j].Color);
                        }
                        Console.WriteLine();
                    }
                    FinDeTurno();
                }
                else
                {
                    Console.WriteLine("LLENO");
                }
            }
        }

        public void FinDeTurno()
        {
            string continua = "continua";
            string empate = "empate";

            string estadoDeJuego = proxy.FinDeTurno(tableroService.Matriz, fichaVaciaService, jugadorActualService);

            if (estadoDeJuego == continua)
            {
                jugadorActualService = proxy.CambiarTurnoJugador(jugador1Service, jugador2Service, jugadorActualService);
                StatusText.Text = String.Format("Turno de {0}", jugadorActualService.Ficha.Color);

            }
            else if (estadoDeJuego == empate)
            {
                StatusText.Text = "Juego Empatado!";
                DenegarTodosLosInsertButtons();
            }
            else
            {
                StatusText.Text = String.Format("Jugador {0} Gana!", estadoDeJuego);
                DenegarTodosLosInsertButtons();
            }
        }


        private void InsetButton_Click(object sender, RoutedEventArgs e)
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
            InitJuego();
        }

        
    }
}
