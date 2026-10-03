using MySqlConnector;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool dev = false;
        internal static Connect Conn = new Connect();
        public MainWindow()
        {
            InitializeComponent();
        }


        private bool kukac() // Kukac az emailben vizsgálat
        {
            string email = (string)emailtxt.Text;


            foreach (char c in email)
            {
                if (c == '@')
                {
                    return true;
                }

            }
            return false;
        }

        private void longinbtnvisb_Click(object sender, RoutedEventArgs e) //Login button
        {
            string email = (string)emailtxt.Text;
            string jelszo = (string)password.Password;

            if (email == "" || jelszo == "")
            {
                MessageBox.Show("No data found");
            }
            else if(kukac() == false || RealUser(email) == false)
            {
                MessageBox.Show("Wrong email input or not a real user");
            }
            else
            {
                Conn.Connection.Close();
                Conn.Connection.Open();
                var sql = $"SELECT * FROM `users` WHERE email = @email";
                var cmd = new MySqlCommand(sql, Conn.Connection);
                cmd.Parameters.AddWithValue("@email", email);
                var dr = cmd.ExecuteReader();
                dr.Read();
                do
                {
                    var felhasznalo = new
                    {
                        NameAdatbazis = dr.GetString(1),
                        EmailAdatbazis = dr.GetString(2),
                        JelszoAdatbazis = dr.GetString(3),
                        RoleAdatbazis = dr.GetString(6)
                    };
                    if (jelszo != felhasznalo.JelszoAdatbazis)
                    {
                        MessageBox.Show("Wrong password");
                        break;
                    }
                    else if (RoleVizsg(felhasznalo.RoleAdatbazis) ==  true)
                    {
                        MessageBox.Show($"Sikeres Bejelentkezés. Üdvözlünk {felhasznalo.NameAdatbazis}");
                        MainFrame.Visibility = Visibility.Visible;
                        LoginGrid.Visibility = Visibility.Collapsed;
                        MainFrame.Navigate(new mainmenu());
                    }
                    else
                    {
                        MessageBox.Show($"Sikertelen Bejelentkezés. Nincs jogosultságod hozzá.");
                    }
                }
                while (dr.Read());
                dr.Close();
                Conn.Connection.Close();
            }
        }
        private bool RoleVizsg(string felhaszrole) //Role vizsgálat
        {
            string cos = "Customer";
            string role = "Admin";
            if(cos != felhaszrole)
            {
                if (role != felhaszrole)
                {
                    dev = true;
                }
                return true;
            }
            return false;
        }


        private bool RealUser(string email) //real user vizsgálat
        {
            Conn.Connection.Open();
            var sql = $"SELECT * FROM `users` WHERE email = @email";
            var cmd = new MySqlCommand(sql, Conn.Connection);
            cmd.Parameters.AddWithValue("@email", email);
            var dr = cmd.ExecuteReader();
            while (dr.Read()) 
            {
                var felhasznalo = new
                {
                    email = dr.GetString(2)
                };
                if(email == felhasznalo.email){
                    return true;
                }
                else
                {
                    break;
                }
            }
            dr.Close();
            Conn.Connection.Close();
            return false;
        }
    }
}