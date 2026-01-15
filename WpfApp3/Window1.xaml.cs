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

namespace WpfApp3
{
    
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            AddGrid();
        }
        public void Sd_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow mainWindow = new();
            mainWindow.Show();
            this.Close();
        }
        public void AddGrid()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var d = db.Subjects.ToList();
                db.Subjects.RemoveRange(db.Subjects.Where(e=>e.Name == null));
                dataGrid2.ItemsSource = d;
                var b = db.Groups.ToList();
                db.Groups.RemoveRange(db.Groups.Where(e => e.Name == null));
                dataGrid.ItemsSource = b;
            }
        }
        public void S_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                int i = Convert.ToInt32(Text2.Text);
                if (db.Subjects.Any(e => e.id == i))
                {
                    Text2.Background = Brushes.White;
                    Text2.ToolTip = null;
                    Subjects? A = db.Subjects.FirstOrDefault(e => e.id == i);
                    MessageBox.Show(A.Name);
                    if (Text3.Text != null)
                    {
                        A.Name = Text3.Text;
                    }
                    if (Text4.Text != "")
                    {
                        A.Hours = Convert.ToInt16(Text4.Text);
                    }
                    if (Text5.Text != "")
                    {
                        A.Cabinet = Tet1.Text;
                    }
                    db.Subjects.Update(A);
                    db.SaveChanges();
                    Text2.Clear();
                    Text3.Clear();
                    Text4.Clear();
                    Text5.Clear();

                    AddGrid();
                }
                else
                {
                    Text2.Background = Brushes.LightPink;
                    Text2.ToolTip = "Класса с данным ID не существует.";
                }

            }
        }
        public void A_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                int i = Convert.ToInt32(Text.Text);
                if (db.Groups.Any(e=>e.Id == i))
                {
                    Text.Background = Brushes.White;
                    Text.ToolTip = null;
                    Groups? A = db.Groups.FirstOrDefault(e => e.Id == i);
                    if (Tet1.Text != null)
                    {
                        A.Name = Tet1.Text;
                        db.Groups.Update(A);
                        db.SaveChanges();
                        Text.Clear();
                        Tet1.Clear();
                    }
                }
                else
                {
                    Text.Background = Brushes.LightPink;
                    Text.ToolTip = "Класса с данным ID не существует.";
                }
                AddGrid();
            }
            }
    }
}
