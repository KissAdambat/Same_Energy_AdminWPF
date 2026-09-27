using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Same_Energy_AdminWPF
{
    /// <summary>
    /// Interaction logic for mainmenu.xaml
    /// </summary>
    public partial class mainmenu : Page
    {
        public mainmenu()
        {
            InitializeComponent();

            if (MainWindow.dev == true)
            {
                MessageBox.Show("Developerként van bejelentkezve");
                userbtn.Visibility = Visibility.Visible;
            }
            else
            {
                userbtn.Visibility = Visibility.Hidden;
            }
        }
    }
}
