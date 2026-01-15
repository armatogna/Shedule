using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using WpfApp3.EF;
using Newtonsoft.Json;
using WpfApp3.EF.TableClasses;
using System.IO;
namespace WpfApp3
{
    public partial class Schedule : Window
    {
        public Schedule()
        {
            string f = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "JSONs\\Save.json");

            Dop d = Load(f);
            InitializeComponent();
            string s = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "Resources\\ico4.bmp");
            Uri iconUri = new Uri(s, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            if (d != null)
            {
                int fw = d.b + 1;
                Text.Text = $"{d.b}-{fw}";
            }
        }
        public static void Save(Dop d, string path)
        {
            string js = JsonConvert.SerializeObject(d, Formatting.Indented);
            File.WriteAllText(path, js);
        }
        public Dop Load(string path)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<Dop>(json);

            }
            return null;
        }

        public Schedule(int b, int c)
        {
            Dop dop = new Dop()
            {
                b = b,
                c = c
            };
            string f = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "JSONs\\Save.json");
            Save(dop, f);
            InitializeComponent();
            string s = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "Resources\\ico4.bmp");
            Uri iconUri = new Uri(s, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            AddLessons(c);

        }
        public void Search_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(search.Text))
            {
                search.Background = Brushes.LightPink;
                search.ToolTip = "Поле не может быть пустым.";
            }
            else
            {
                search.Background = Brushes.White;
                search.ToolTip = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                var d = db.Lessons.Where(i => i.group == search.Text).ToList();
                dataGrid.ItemsSource = d;
                if(d == null) { MessageBox.Show("Совпадений нет!"); }
            }
            }
           

        }
        public void S_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            this.Close();
        }
        public void A_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow open = new();
            open.Show();
            this.Close();
        }
        private async void AddLessons(int asc)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                
                    

                    int id = db.Groups.Count();
                    
                    if (id != 0)
                    {
                        for (int i = 0; i <= id; i++)
                        {
                                Groups? group = null;
                                while (group == null) { group = await db.Groups.FindAsync(i); i++; }
                            int w = await db.Subjects.Where(e => e.GroupId == group.Id).CountAsync();

                            Random rnd = new();
                            List<Subjects> sq = await db.Subjects.Where(e => e.GroupId == group.Id).ToListAsync();

                            int se = 0;
                            foreach (Subjects subjects in sq)
                            {
    
                                    se += subjects.Hours;
                                    
                            }
                            double set = se / 170;
                            int e = se / 170;
                            if (set > e) { se = e + 1; }
                            if (e > asc) { e = asc+1; }
                            List<string> days = new List<string> { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
                            int t = 0;
                           
                            foreach (string day in days)
                            {


                                for (i = 1; i < e; i++)
                                {
                                    if (sq.Count != 0 && sq != null)
                                    {
                                        int rint = rnd.Next(0, w);

                                        Subjects s = sq[rint];

                                        s.idL = rint;
                                        int y = w;
                                        while (s.Weeks == 0 && sq != null)
                                        {

                                            sq.Remove(s);

                                        w -= 1;
                                        y--;
                                            if (y <= 0) { break; }
                                            rint = rnd.Next(0, w);
                                            s = sq[rint];
                                            s.idL = rint;
                                        }
                                    while (db.Lessons.Any(e => e.day == day && e.num == i && e.kab == s.Cabinet) && sq != null)
                                    {

                                        w -= 1;
                                        y--;
                                        if (y <= 0) { break; }
                                        rint = rnd.Next(0, w);
                                        s = sq[rint];
                                        s.idL = rint;
                                    }
                                    if (y <= 0) { continue; }
                                        Lessons lesson = new()
                                        {
                                            day = day,
                                            num = i,
                                            group = group.Name,
                                            lesson = s.Name,
                                            kab = s.Cabinet
                                        };
                                        t++;
                                        db.Lessons.Add(lesson);
                                        sq[s.idL].Weeks -= 1;
                                    }
                                }
                                /*if (day == "Понедельник")
                                {
                                    if (sq.Count != 0 && sq != null)
                                    {
                                        int rint = rnd.Next(0, w);
                                        Subjects s = sq[rint];

                                        while (s.Weeks == 0 && sq != null && sq.Count != 0)
                                        {
                                            sq.Remove(s);
                                            w -= 1;
                                            if (sq.Count != 0)
                                            {
                                                rint = rnd.Next(0, w);
                                            MessageBox.Show(rint.ToString());
                                            MessageBox.Show(sq.Count.ToString());
                                            s = sq[rint];

                                                s.idL = rint;
                                            }
                                        }
                                        if (sq.Count != 0 && sq != null)
                                        {
                                            Lessons lesson1 = new()
                                            {
                                                day = day,
                                                num = i,
                                                group = group.Name,
                                                lesson = s.Name,
                                                kab = s.Cabinet
                                            };
                                            db.Lessons.Add(lesson1);
                                            sq[s.idL].Weeks -= 1;
                                        }
                                    }
                                }*/
                            }
                        }
                    }

                    db.SaveChanges();

                   
                }

            
        }
       

        
    }
}
