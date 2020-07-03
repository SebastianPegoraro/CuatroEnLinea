﻿using System;
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
using System.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TPfinal
{
    
    public partial class MainWindow : Window
    {      
        Random ai = new Random();
        const int tamanioCirculo = 80;
        private Juego juego ;
        private DispatcherTimer animacion;
        private bool bloquearInput;
        private Estado estadoActual;
        private Ellipse circuloActual;
        private int columnaActual;
        private bool vsAI;
        

        public bool VsAI { get => vsAI; set => vsAI = value; }

        public MainWindow()
        {            
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            NuevoJuego();
            
        }

        private void DibujarFondo()
        {
            for (int fila = 0; fila < juego.tablero.matriz.GetLength(0); fila++)
            {
                for (int columna = 0; columna < juego.tablero.matriz.GetLength(1); columna++)
                {
                    Rectangle cuadrado = new Rectangle();
                    cuadrado.Width = tamanioCirculo;
                    cuadrado.Height = tamanioCirculo;
                    ImageBrush imagen = new ImageBrush();
                    imagen.ImageSource =
                    new BitmapImage(new Uri(@"pack://siteoforigin:,,,/fondo.png"));
                    cuadrado.Fill = (columna % 2 == 0) ? imagen : imagen;
                    Canvas.SetBottom(cuadrado, tamanioCirculo * fila);
                    Canvas.SetRight(cuadrado, tamanioCirculo * columna);
                    tableroCanvas.Children.Add(cuadrado);
                }
            }
        }
        

        private void PermitirTodosInsertButtons()
        {
            btn1.IsEnabled = true;
            btn2.IsEnabled = true;
            btn3.IsEnabled = true;
            btn4.IsEnabled = true;
            btn5.IsEnabled = true;
            btn6.IsEnabled = true;
            btn7.IsEnabled = true;
        }

        private void DenegarTodosLosInsertButtons()
        {
            btn1.IsEnabled = false;
            btn2.IsEnabled = false;
            btn3.IsEnabled = false;
            btn4.IsEnabled = false;
            btn5.IsEnabled = false;
            btn6.IsEnabled = false;
            btn7.IsEnabled = false;
        }

        private void InsertarFicha_Click(int columna)
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
            ImageBrush imagen = new ImageBrush();
            imagen.ImageSource =
            new BitmapImage(new Uri(@"pack://siteoforigin:,,,/azul.png"));
            ImageBrush imagen2 = new ImageBrush();
            imagen2.ImageSource =
            new BitmapImage(new Uri(@"pack://siteoforigin:,,,/rojo.png"));
            circulo.Fill = (estado == Estado.uno) ? imagen : imagen2;
            Canvas.SetTop(circulo, 0);
            Canvas.SetLeft(circulo, columna * 80);
            tableroCanvas.Children.Add(circulo);
            circuloActual = circulo;
            animacion.Tick += AnimacionCaidaCirculo;
        }

        private void AnimacionCaidaCirculo(object sender, EventArgs e)
        {
            int dropLength = tamanioCirculo * (juego.tablero.matriz.GetLength(1) - 1 - juego.PiezasEnColumna(columnaActual));
            int dropRate = 40;
            if (Canvas.GetTop(circuloActual) < dropLength)
            {
                Canvas.SetTop(circuloActual, Canvas.GetTop(circuloActual) + dropRate);
            }
            else
            {
                animacion.Tick -= AnimacionCaidaCirculo;
                bloquearInput = false;

                if (vsAI && estadoActual == Estado.dos)
                {
                    InsertarFicha_Click(ai.Next(0, 7));
                }
            }

        }

        private void FinDelTurno()
        {
            Estado winner = juego.Ganador();

            if (winner != Estado.Nada)
            {
                estadoText.Text = String.Format("Jugador {0} Gana!", estadoActual);
                DenegarTodosLosInsertButtons();
            }
            else if (juego.Empate())
            {
                estadoText.Text = "Juego Empatado!";
                DenegarTodosLosInsertButtons();
            }
            else
            {
                estadoActual = (estadoActual == Estado.dos) ? Estado.uno : Estado.dos;
                estadoText.Text = String.Format("Turno de Jugador {0}", estadoActual);       
            }
        }

        private void NuevoJuego()
        {
            bloquearInput = true;
            juego = new Juego(new Tablero(6, 7));
            estadoActual = Estado.uno;
            animacion = new DispatcherTimer();
            animacion.Interval = new TimeSpan(0, 0, 0, 0, 15);
            animacion.Start();
            tableroCanvas.Children.Clear();
            DibujarFondo();
            bloquearInput = false;
            PermitirTodosInsertButtons();
        }

        //btn1
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InsertarFicha_Click(0);                    
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            InsertarFicha_Click(1);
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            InsertarFicha_Click(2);
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            InsertarFicha_Click(3);
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            InsertarFicha_Click(4);
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            InsertarFicha_Click(5);
        }

        //btn7
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InsertarFicha_Click(6);
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            Principal nuevojuego = new Principal();         
            this.Close();        
            nuevojuego.Show();
        }

        
    }
}
