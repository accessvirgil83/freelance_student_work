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

namespace PolynaWPF2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Form1 form1 = new Form1();

            // Вызовите метод Show() для отображения формы
            form1.Show();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            Form2 form2 = new Form2();

            // Вызовите метод Show() для отображения формы
            form2.Show();
        }

    }
}
