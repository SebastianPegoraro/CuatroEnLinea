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
using System.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TPfinal
{
    
    public partial class Principal : Window
    {
        SoundPlayer comienzo = new SoundPlayer("comienzo.wav");
        public Principal()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;          
            comienzo.Play();
        }

        private void btnAI_Click(object sender, RoutedEventArgs e)
        {
            MainWindow juegoAI = new MainWindow();
            juegoAI.VsAI = true;
            this.Close();
            
            juegoAI.Show();
        }

        //btn vs local
        private void Button_Click(object sender, RoutedEventArgs e)
        {         
            MainWindow juegoLocal = new MainWindow();
            juegoLocal.VsAI = false;
            this.Close();
            juegoLocal.Show();
        }
    }
}
