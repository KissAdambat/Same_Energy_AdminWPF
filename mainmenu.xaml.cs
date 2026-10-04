using System;
using System.Collections.Generic;
using System.Net.Quic;
using System.Security.AccessControl;
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
using MySqlConnector;
using Same_Energy_AdminWPF.Models;

namespace Same_Energy_AdminWPF
{
    /// <summary>
    /// Interaction logic for mainmenu.xaml
    /// </summary>
    public partial class mainmenu : Page
    {
        internal static Connect Conn = new Connect();
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
            TotalProducts(null,null);
            TotalOrders(null, null);
            TotalUsers(null, null);
        }

        private void TotalProducts(object sender, RoutedEventArgs e)
        {
            Conn.Connection.Open();
            var sql = "SELECT COUNT(*) FROM `products`";
            var cmd = new MySqlCommand(sql, Conn.Connection);
            var count = Convert.ToInt32(cmd.ExecuteScalar());
            Conn.Connection.Close();
            totalproductDB.Text = count.ToString();
        }
        private void TotalOrders(object sender, RoutedEventArgs e)
        {
            Conn.Connection.Open();
            var sql = "SELECT COUNT(*) FROM `orders`";
            var cmd = new MySqlCommand(sql, Conn.Connection);
            var count = Convert.ToInt32(cmd.ExecuteScalar());
            Conn.Connection.Close();
            totalordersDB.Text = count.ToString();
        }
        private void TotalUsers(object sender, RoutedEventArgs e)
        {
            Conn.Connection.Open();
            var sql = "SELECT COUNT(*) FROM `users`";
            var cmd = new MySqlCommand(sql, Conn.Connection);
            var count = Convert.ToInt32(cmd.ExecuteScalar());
            Conn.Connection.Close();
            totalusersDB.Text = count.ToString();
        }
        private void productsbtn_Click(object sender, RoutedEventArgs e)
        {
            statuspd.Visibility = Visibility.Hidden;
            statusupdttxt.Visibility = Visibility.Hidden;
            updtstatusbttn.Visibility = Visibility.Hidden;
            updorders.Visibility = Visibility.Hidden;
            List<ProductsM> products = new List<ProductsM>();
            Conn.Connection.Open();
            var sql = "SELECT * FROM `products` WHERE 1";
            var cmd = new MySqlCommand(sql, Conn.Connection);
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var product = new ProductsM
                {
                    id = dr.GetInt32("id"),
                    name = dr.GetString("name"),
                    description = dr.GetString("description"),
                    price = dr.GetDecimal("price"),
                    size = dr.GetString("size"),
                    stock = dr.GetInt32("stock"),
                    image = dr.GetString("image"),
                    category = dr.GetString("category")
                };
                products.Add(product);
            }
            Conn.Connection.Close();
            maindg.ItemsSource = products;
            newprod.Visibility = Visibility.Visible;
            delprod.Visibility = Visibility.Visible;
            updtprod.Visibility = Visibility.Visible;
        }

        private void newprod_Click(object sender, RoutedEventArgs e)
        {
            if (updateprod.Visibility == Visibility.Visible)
            {
                newnameprod.Visibility = Visibility.Hidden;
                //newnameprod.Content = "Name:";
                newprodstock.Visibility = Visibility.Hidden;
                //newprodstock.Content = "Stock:";
                newprodprice.Visibility = Visibility.Hidden;
                //newprodprice.Content = "Price:";
                newprodsize.Visibility = Visibility.Hidden;
                //newprodsize.Content = "Size:";
                newprodnametext.Visibility = Visibility.Hidden;
                newprodstocktext.Visibility = Visibility.Hidden;
                newprodpricetext.Visibility = Visibility.Hidden;
                newprodsizetext.Visibility = Visibility.Hidden;
                updateprod.Visibility = Visibility.Hidden;
                newnameprod.Visibility = Visibility.Visible;
                newproddesc.Visibility = Visibility.Visible;
                newprodprice.Visibility = Visibility.Visible;
                newprodsize.Visibility = Visibility.Visible;
                newprodstock.Visibility = Visibility.Visible;
                newprodcategory.Visibility = Visibility.Visible;
                newprodimage.Visibility = Visibility.Visible;
                newprodnametext.Visibility = Visibility.Visible;
                newproddesctext.Visibility = Visibility.Visible;
                newprodpricetext.Visibility = Visibility.Visible;
                newprodsizetext.Visibility = Visibility.Visible;
                newprodstocktext.Visibility = Visibility.Visible;
                newprodcategorytext.Visibility = Visibility.Visible;
                newprodimagetext.Visibility = Visibility.Visible;
                addprodbbtn.Visibility = Visibility.Visible;
            }
            else
            {
                newnameprod.Visibility = Visibility.Visible;
                newproddesc.Visibility = Visibility.Visible;
                newprodprice.Visibility = Visibility.Visible;
                newprodsize.Visibility = Visibility.Visible;
                newprodstock.Visibility = Visibility.Visible;
                newprodcategory.Visibility = Visibility.Visible;
                newprodimage.Visibility = Visibility.Visible;
                newprodnametext.Visibility = Visibility.Visible;
                newproddesctext.Visibility = Visibility.Visible;
                newprodpricetext.Visibility = Visibility.Visible;
                newprodsizetext.Visibility = Visibility.Visible;
                newprodstocktext.Visibility = Visibility.Visible;
                newprodcategorytext.Visibility = Visibility.Visible;
                newprodimagetext.Visibility = Visibility.Visible;
                addprodbbtn.Visibility = Visibility.Visible;
            }
        }

        private void addprodbbtn_Click(object sender, RoutedEventArgs e)
        {
            string name = newprodnametext.Text;
            string description = newproddesctext.Text;
            decimal price = decimal.Parse(newprodpricetext.Text);
            string size = newprodsizetext.Text;
            int stock = int.Parse(newprodstocktext.Text);
            string category = newprodcategorytext.Text;
            string image = newprodimagetext.Text;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(size) || string.IsNullOrEmpty(category) || string.IsNullOrEmpty(image))
            {
                MessageBox.Show("Kérem töltse ki az összes mezőt!");
            }
            else if (price <= 0 || stock < 0)
            {
                MessageBox.Show("Az árnak nagyobbnak kell lennie 0-nál, a készletnek pedig nem lehet negatív!");
            }
            else if (category != "Cipők" && category != "Zoknik" && category != "Farmerok" && category != "Melegítők" && category != "Rövidnadrágok" && category != "Pólók"&& category != "Pulóverek" && category != "Kabátok" && category != "Kiegészítők" && category != "Parfümök")
            {
                MessageBox.Show("A kategória csak 'Cipők', 'Zoknik', 'Farmerok', 'Melegítők', 'Rövidnadrágok', 'Pólók', 'Pulóverek', 'Kabátok', 'Kiegészítők' vagy 'Parfümök' lehet!");
            }
            else if (MessageBox.Show("Biztosan hozzá akarja adni a terméket?", "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Conn.Connection.Open();
                var sql = "INSERT INTO `products` (`name`, `description`, `price`, `size`, `stock`, `image`, `category`) VALUES (@name, @description, @price, @size, @stock, @image, @category)";
                var cmd = new MySqlCommand(sql, Conn.Connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@size", size);
                cmd.Parameters.AddWithValue("@stock", stock);
                cmd.Parameters.AddWithValue("@image", image);
                cmd.Parameters.AddWithValue("@category", category);
                cmd.ExecuteNonQuery();
                Conn.Connection.Close();
                MessageBox.Show("A termék sikeresen hozzáadva!");
                newnameprod.Visibility = Visibility.Hidden;
                newproddesc.Visibility = Visibility.Hidden;
                newprodprice.Visibility = Visibility.Hidden;
                newprodsize.Visibility = Visibility.Hidden;
                newprodstock.Visibility = Visibility.Hidden;
                newprodcategory.Visibility = Visibility.Hidden;
                newprodimage.Visibility = Visibility.Hidden;
                newprodnametext.Visibility = Visibility.Hidden;
                newproddesctext.Visibility = Visibility.Hidden;
                newprodpricetext.Visibility = Visibility.Hidden;
                newprodsizetext.Visibility = Visibility.Hidden;
                newprodstocktext.Visibility = Visibility.Hidden;
                newprodcategorytext.Visibility = Visibility.Hidden;
                newprodimagetext.Visibility = Visibility.Hidden;
                addprodbbtn.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("A termék hozzáadása megszakítva!");
            }
        }

        private void updtprod_Click(object sender, RoutedEventArgs e)
        {
            if (addprodbbtn.Visibility == Visibility.Visible)
            {
                newnameprod.Visibility = Visibility.Hidden;
                newproddesc.Visibility = Visibility.Hidden;
                newprodprice.Visibility = Visibility.Hidden;
                newprodsize.Visibility = Visibility.Hidden;
                newprodstock.Visibility = Visibility.Hidden;
                newprodcategory.Visibility = Visibility.Hidden;
                newprodimage.Visibility = Visibility.Hidden;
                newprodnametext.Visibility = Visibility.Hidden;
                newproddesctext.Visibility = Visibility.Hidden;
                newprodpricetext.Visibility = Visibility.Hidden;
                newprodsizetext.Visibility = Visibility.Hidden;
                newprodstocktext.Visibility = Visibility.Hidden;
                newprodcategorytext.Visibility = Visibility.Hidden;
                newprodimagetext.Visibility = Visibility.Hidden;
                addprodbbtn.Visibility = Visibility.Hidden;
                //newnameprod.Content = "Price:";
                newnameprod.Visibility = Visibility.Visible;
                newprodnametext.Visibility = Visibility.Visible;
                //newprodprice.Content = "Size:";
                newprodprice.Visibility = Visibility.Visible;
                newprodpricetext.Visibility = Visibility.Visible;
                //newprodsize.Content = "Stock:";
                newprodsize.Visibility = Visibility.Visible;
                newprodsizetext.Visibility = Visibility.Visible;
                updateprod.Visibility = Visibility.Visible;
               // newprodstock.Content = "Category:";
                newprodstock.Visibility = Visibility.Visible;
                newprodstocktext.Visibility = Visibility.Visible;

            }
            else
            {
                //newnameprod.Content = "Price:";
                newnameprod.Visibility = Visibility.Visible;
                newprodnametext.Visibility = Visibility.Visible;
                //newprodprice.Content = "Size:";
                newprodprice.Visibility = Visibility.Visible;
                newprodpricetext.Visibility = Visibility.Visible;
                //newprodsize.Content = "Stock:";
                newprodsize.Visibility = Visibility.Visible;
                newprodsizetext.Visibility = Visibility.Visible;
                updateprod.Visibility = Visibility.Visible;
                //newprodstock.Content = "Category:";
                newprodstock.Visibility = Visibility.Visible;
                newprodstocktext.Visibility = Visibility.Visible;
            }
        }

        private void updateprod_Click(object sender, RoutedEventArgs e)
        {
            string price = newprodnametext.Text;
            string size = newprodpricetext.Text;
            string stock = newprodsizetext.Text;
            string category = newprodstocktext.Text;
            if (string.IsNullOrEmpty(price) || string.IsNullOrEmpty(size) || string.IsNullOrEmpty(stock) || string.IsNullOrEmpty(category))
            {
                MessageBox.Show("Kérem töltse ki az összes mezőt!");
            }
            else if (price == "0" || stock == "0")
            {
                MessageBox.Show("Az árnak és a készletnek nagyobbnak kell lennie 0-nál!");
            }
            else if (category != "Cipők" && category != "Zoknik" && category != "Farmerok" && category != "Melegítők" && category != "Rövidnadrágok" && category != "Pólók" && category != "Pulóverek" && category != "Kabátok" && category != "Kiegészítők" && category != "Parfümök")
            {
                MessageBox.Show("A kategória csak 'Cipők', 'Zoknik', 'Farmerok', 'Melegítők', 'Rövidnadrágok', 'Pólók', 'Pulóverek', 'Kabátok', 'Kiegészítők' vagy 'Parfümök' lehet!");
            }
            else if (maindg.SelectedItem == null)
            {
                MessageBox.Show("Kérem válasszon ki egy terméket a frissítéshez!");
            }
            else if (MessageBox.Show("Biztosan frissíteni akarja a terméket?", "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Conn.Connection.Open();
                var sql = "UPDATE `products` SET `price` = @price, `size` = @size, `stock` = @stock, `category` = @category WHERE `id` = @id";
                var cmd = new MySqlCommand(sql, Conn.Connection);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@size", size);
                cmd.Parameters.AddWithValue("@stock", stock);
                cmd.Parameters.AddWithValue("@category", category);
                cmd.Parameters.AddWithValue("@id", maindg.SelectedItem is ProductsM selectedProduct ? selectedProduct.id : 0);
                cmd.ExecuteNonQuery();
                Conn.Connection.Close();
                MessageBox.Show("A termék sikeresen frissítve!");
                newnameprod.Visibility = Visibility.Hidden;
                //newnameprod.Content = "Name:";
                newprodprice.Visibility = Visibility.Hidden;
                //newprodprice.Content = "Price:";
                newprodsize.Visibility = Visibility.Hidden;
                //newprodsize.Content = "Size:";
                newprodstock.Visibility = Visibility.Hidden;
                //newprodstock.Content = "Stock:";
                newprodnametext.Visibility = Visibility.Hidden;
                newprodpricetext.Visibility = Visibility.Hidden;
                newprodsizetext.Visibility = Visibility.Hidden;
                newprodstocktext.Visibility = Visibility.Hidden;
                updateprod.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("A termék frissítése megszakítva!");
            }
        }

        private void delprod_Click(object sender, RoutedEventArgs e)
        {
            if (maindg.SelectedItem == null)
            {
                MessageBox.Show("Kérem válasszon ki egy terméket a törléshez!");
            }
            else if (MessageBox.Show("Biztosan törölni akarja a terméket?", "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Conn.Connection.Open();
                var sql = "DELETE FROM `products` WHERE `id` = @id";
                var cmd = new MySqlCommand(sql, Conn.Connection);
                cmd.Parameters.AddWithValue("@id", maindg.SelectedItem is ProductsM selectedProduct ? selectedProduct.id : 0);
                cmd.ExecuteNonQuery();
                Conn.Connection.Close();
                MessageBox.Show("A termék sikeresen törölve!");
            }
            else
            {
                MessageBox.Show("A termék törlése megszakítva!");
            }   
        }

        private void exitbtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Biztosan ki akar lépni?", "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            newnameprod.Visibility = Visibility.Hidden;
            newproddesc.Visibility = Visibility.Hidden;
            newprodprice.Visibility = Visibility.Hidden;
            newprodsize.Visibility = Visibility.Hidden;
            newprodstock.Visibility = Visibility.Hidden;
            newprodcategory.Visibility = Visibility.Hidden;
            newprodimage.Visibility = Visibility.Hidden;
            newprodnametext.Visibility = Visibility.Hidden;
            newproddesctext.Visibility = Visibility.Hidden;
            newprodpricetext.Visibility = Visibility.Hidden;
            newprodsizetext.Visibility = Visibility.Hidden;
            newprodstocktext.Visibility = Visibility.Hidden;
            newprodcategorytext.Visibility = Visibility.Hidden;
            newprodimagetext.Visibility = Visibility.Hidden;
            updateprod.Visibility = Visibility.Hidden;
            newprod.Visibility = Visibility.Hidden;
            delprod.Visibility = Visibility.Hidden;
            updtprod.Visibility = Visibility.Hidden;
            maindg.ItemsSource = null;
            List<OrdersM> orders = new List<OrdersM>();
            Conn.Connection.Open();
            var sql = "SELECT * FROM `orders` WHERE 1";
            var cmd = new MySqlCommand(sql, Conn.Connection);
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var order = new OrdersM
                {
                    id = dr.GetInt32("id"),
                    user_id = dr.GetInt32("user_id"),
                    status = dr.GetString("status"),
                    total_price = dr.GetDecimal("total_price"),
                    order_date = dr.GetDateTime("order_date")
                };
                orders.Add(order);
            }
            Conn.Connection.Close();
            maindg.ItemsSource = orders;
            updorders.Visibility = Visibility.Visible;
        }

        private void updorders_Click(object sender, RoutedEventArgs e)
        {
            statuspd.Visibility = Visibility.Visible;
            statusupdttxt.Visibility = Visibility.Visible;
            updtstatusbttn.Visibility = Visibility.Visible;
        }

        private void updtstatusbttn_Click(object sender, RoutedEventArgs e)
        {
            string status = statusupdttxt.Text;
            if (status != "pending" && status != "shipped" && status != "delivered" && status != "cancelled")
            {
                MessageBox.Show("A státusz csak 'pending', 'shipped', 'delivered' vagy 'cancelled' lehet!");
            }
            else if (maindg.SelectedItem == null)
            {
                MessageBox.Show("Kérem válasszon ki egy rendelést a frissítéshez!");
            }
            else if (MessageBox.Show("Biztosan frissíteni akarja a rendelés státuszát?", "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Conn.Connection.Open();
                var sql = "UPDATE `orders` SET `status` = @status WHERE `id` = @id";
                var cmd = new MySqlCommand(sql, Conn.Connection);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@id", maindg.SelectedItem is OrdersM selectedOrder ? selectedOrder.id : 0);
                cmd.ExecuteNonQuery();
                Conn.Connection.Close();
                MessageBox.Show("A rendelés státusza sikeresen frissítve!");
                statuspd.Visibility = Visibility.Hidden;
                statusupdttxt.Visibility = Visibility.Hidden;
                updtstatusbttn.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("A rendelés státuszának frissítése megszakítva!");
            }
        }

        private void oderderd_item_Click(object sender, RoutedEventArgs e)
        {
            newnameprod.Visibility = Visibility.Hidden;
            newproddesc.Visibility = Visibility.Hidden;
            newprodprice.Visibility = Visibility.Hidden;
            newprodsize.Visibility = Visibility.Hidden;
            newprodstock.Visibility = Visibility.Hidden;
            newprodcategory.Visibility = Visibility.Hidden;
            newprodimage.Visibility = Visibility.Hidden;
            newprodnametext.Visibility = Visibility.Hidden;
            newproddesctext.Visibility = Visibility.Hidden;
            newprodpricetext.Visibility = Visibility.Hidden;
            newprodsizetext.Visibility = Visibility.Hidden;
            newprodstocktext.Visibility = Visibility.Hidden;
            newprodcategorytext.Visibility = Visibility.Hidden;
            newprodimagetext.Visibility = Visibility.Hidden;
            updateprod.Visibility = Visibility.Hidden;
            newprod.Visibility = Visibility.Hidden;
            delprod.Visibility = Visibility.Hidden;
            updtprod.Visibility = Visibility.Hidden;
            statuspd.Visibility = Visibility.Hidden;
            statusupdttxt.Visibility = Visibility.Hidden;
            updtstatusbttn.Visibility = Visibility.Hidden;
            updorders.Visibility = Visibility.Hidden;
            maindg.ItemsSource = null;
            List<OrderedItemsM> orders = new List<OrderedItemsM>();
            Conn.Connection.Open();
            var sql = "SELECT * FROM `order_items` WHERE 1";
            var cmd = new MySqlCommand(sql, Conn.Connection);
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var order = new OrderedItemsM  
                {
                    id = dr.GetInt32("id"),
                    order_id = dr.GetInt32("order_id"),
                    product_id = dr.GetInt32("product_id"),
                    product_name = dr.GetString("product_name"),
                    size = dr.GetString("size"),
                    quantity = dr.GetInt32("quantity"),
                    unit_price = dr.GetDecimal("unit_price")
                };
                orders.Add(order);
            }
            Conn.Connection.Close();
            maindg.ItemsSource = orders;
        }

        private void userbtn_Click(object sender, RoutedEventArgs e)
        {
            newnameprod.Visibility = Visibility.Hidden;
            newproddesc.Visibility = Visibility.Hidden;
            newprodprice.Visibility = Visibility.Hidden;
            newprodsize.Visibility = Visibility.Hidden;
            newprodstock.Visibility = Visibility.Hidden;
            newprodcategory.Visibility = Visibility.Hidden;
            newprodimage.Visibility = Visibility.Hidden;
            newprodnametext.Visibility = Visibility.Hidden;
            newproddesctext.Visibility = Visibility.Hidden;
            newprodpricetext.Visibility = Visibility.Hidden;
            newprodsizetext.Visibility = Visibility.Hidden;
            newprodstocktext.Visibility = Visibility.Hidden;
            newprodcategorytext.Visibility = Visibility.Hidden;
            newprodimagetext.Visibility = Visibility.Hidden;
            updateprod.Visibility = Visibility.Hidden;
            newprod.Visibility = Visibility.Hidden;
            delprod.Visibility = Visibility.Hidden;
            updtprod.Visibility = Visibility.Hidden;
            statuspd.Visibility = Visibility.Hidden;
            statusupdttxt.Visibility = Visibility.Hidden;
            updtstatusbttn.Visibility = Visibility.Hidden;
            updorders.Visibility = Visibility.Hidden; 
            maindg.ItemsSource = null;
            List<UsersM> orders = new List<UsersM>();
            Conn.Connection.Open();
            var sql = "SELECT * FROM `users` WHERE 1";
            var cmd = new MySqlCommand(sql, Conn.Connection);
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var order = new UsersM
                {
                    id = dr.GetInt32(dr.GetOrdinal("id")),
                    name = dr.IsDBNull(dr.GetOrdinal("name")) ? null : dr.GetString(dr.GetOrdinal("name")),
                    email = dr.IsDBNull(dr.GetOrdinal("email")) ? null : dr.GetString(dr.GetOrdinal("email")),
                    password = dr.IsDBNull(dr.GetOrdinal("password")) ? null : dr.GetString(dr.GetOrdinal("password")),
                    phone = dr.IsDBNull(dr.GetOrdinal("phone")) ? null : dr.GetString(dr.GetOrdinal("phone")),
                    address = dr.IsDBNull(dr.GetOrdinal("address")) ? null : dr.GetString(dr.GetOrdinal("address")),
                    role = dr.IsDBNull(dr.GetOrdinal("role")) ? null : dr.GetString(dr.GetOrdinal("role"))
                };
                orders.Add(order);
            };
            Conn.Connection.Close();
            maindg.ItemsSource = orders;
        }
    }
}