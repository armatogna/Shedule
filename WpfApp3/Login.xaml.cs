using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
using WpfApp3.EF.TableClasses;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            string s = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "Resources\\icon.ico");
            Uri iconUri = new Uri(s, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool tr = false;
            if (t1.Text == "")
            {
                t1.Background = Brushes.LightPink;

                t1.ToolTip = "Поле не может быть пустым.";
                tr = true;
            }
            else
            {
                t1.Background = Brushes.White;

                t1.ToolTip = null;
            }
            if (string.IsNullOrEmpty(t2.Text))
            {
                t2.Background = Brushes.LightPink;

                t2.ToolTip = "Поле не может быть пустым.";
                tr = true;
            }
            else
            {
                t2.Background = Brushes.White;

                t2.ToolTip = null;
            }
            if (tr == false)
            {
                Account y = new() { Name = "", Password=""};
                using (ApplicationContext db = new ApplicationContext())
                {
                    List<Account> accounts = db.Accounts.Where(i => i.Name == t1.Text).ToList();
                    y = accounts.FirstOrDefault(k => k.Password == t2.Text);
                    MessageBox.Show($"{y.Name}{y.Id.ToString()}");
                }
                if (y.Name != "")
                {
                    OpenWindow openWindow = new OpenWindow(y);
                    openWindow.Show();
                    this.Close();

                }
            }

        }
        Account account = new()
        {
            Name = "Default",
            Password = "Password"
        };
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            bool tr = false;
            if (string.IsNullOrEmpty(t3.Text))
            {
                t3.Background = Brushes.LightPink;

                t3.ToolTip = "Поле не может быть пустым.";
                tr = true;
            }
            else
            {
                t3.Background = Brushes.White;

                t3.ToolTip = null;
            }
            if (string.IsNullOrEmpty(t4.Text))
            {
                t4.Background = Brushes.LightPink;

                t4.ToolTip = "Поле не может быть пустым.";
                tr = true;
            }
            else
            {
                t4.Background = Brushes.White;

                t4.ToolTip = null;
            }
            if (tr == false)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Account account1 = new()
                    {
                        Name = t3.Text,
                        Password = t4.Text
                    };
                    db.Accounts.Add(account1);
                    db.SaveChanges();
                    account = account1;
                }
                OpenWindow openWindow = new OpenWindow(account);
                openWindow.Show();
                this.Close();
            }
        }
    }
}
