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
using System.Windows.Shapes;

namespace TPfinal
{
    
    public partial class Principal : Window
    {
        
        public Principal()
        {
            InitializeComponent();
        }

        private void btnAI_Click(object sender, RoutedEventArgs e)
        {
            MainWindow juegoAI = new MainWindow();
            juegoAI.setAI(1);
            this.Close();
            juegoAI.Show();
        }

        //btn vs local
        private void Button_Click(object sender, RoutedEventArgs e)
        {         
            MainWindow juegoLocal = new MainWindow();
            juegoLocal.setAI(0);
            this.Close();
            juegoLocal.Show();           
        }
    }
}
