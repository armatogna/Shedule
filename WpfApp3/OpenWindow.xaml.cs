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

namespace WpfApp3
{
    public partial class OpenWindow : Window
    {
        
        public OpenWindow()
        {
            InitializeComponent();
            
            string s=AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "Resources\\ico4.bmp");
            Uri iconUri = new Uri(s, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }
        public bool a = false;
        public void Schedule_Click(object sender, RoutedEventArgs e)
        {
            Schedule schedule = new Schedule();
            schedule.Show();
            this.Close();
            
        }
        public void Main_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    var entity = db.Subjects.ToList();
                    var ent = db.Lessons.ToList();
                    db.Subjects.RemoveRange(entity);
                    db.SaveChanges();
                    db.Lessons.RemoveRange(ent);
                    db.SaveChanges();
                    var w = db.Groups.ToList();
                    db.Groups.RemoveRange(w);
                    db.SaveChanges();


                    transaction.Commit();
                }

            }
            MainWindow schedule = new MainWindow();
            schedule.Show();
            this.Close();
        }
        public void S_Click(object sender, RoutedEventArgs e)
        {
            Window1 mainWindow = new();
            mainWindow.Show();
            this.Close();
        }
    }
}
