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
using WpfApp3.EF;
using WpfApp3.EF.TableClasses;

namespace WpfApp3
{
    public partial class OpenWindow : Window
    {
        Account account1 = new()
        {
            Name = "Default",
            Password = "Password"
        };
        /*public OpenWindow()
        {
            InitializeComponent();
            
            string s=AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "Resources\\icon.ico");
            Uri iconUri = new Uri(s, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }*/
        public OpenWindow(Account account)
        {
            InitializeComponent();
            string s = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "Resources\\icon.ico");
            Uri iconUri = new Uri(s, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            account1 = account;
        }
        public bool a = false;
        public void Schedule_Click(object sender, RoutedEventArgs e)
        {
            Schedule schedule = new Schedule(account1);
            schedule.Show();
            this.Close();
            
        }
        public void Main_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    List<Subjects> entity = db.Subjects.ToList();
                    List<Student> ent = db.Students.ToList();
                    db.Subjects.RemoveRange(entity);
                    db.Students.RemoveRange(ent);
                    var w = db.Groups.ToList();
                    db.Groups.RemoveRange(w);
                    db.SaveChanges();


                    transaction.Commit();
                }

            }
            MainWindow schedule = new MainWindow(account1);
            schedule.Show();
            this.Close();
        }
        public void S_Click(object sender, RoutedEventArgs e)
        {
            Window1 mainWindow = new(account1);
            mainWindow.Show();
            this.Close();
        }
    }
}
