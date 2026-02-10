using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp3.EF;
using WpfApp3.EF.TableClasses;

namespace WpfApp3
{
    
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            AddGrid();
            
        }
        public Window1(Account account)
        {
            InitializeComponent();
            AddGrid();
            account1 = account;
            string s = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "Resources\\icon.ico");
            Uri iconUri = new Uri(s, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }
        public void Sd_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow mainWindow = new(account1);
            mainWindow.Show();
            this.Close();
        }
        Account account1 = new() { Name = "Default", Password = "Password" };

        public void AddGrid()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Subjects.RemoveRange(db.Subjects.Where(e => e.Name == null));
                List<Subjects> d = db.Subjects.ToList();
                dataGrid2.ItemsSource = d;
                db.Groups.RemoveRange(db.Groups.Where(e => e.Name == null));
                List<Groups> b = db.Groups.ToList();
                dataGrid.ItemsSource = b;
                comboS.ItemsSource = b;
                comboS.DisplayMemberPath = "Name";
                db.Students.RemoveRange(db.Students.Where(e => e.fullName == null));
                List<Student> q = db.Students.ToList();
                dataGrid3.ItemsSource = q;
                db.SaveChanges();

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
                    Subjects A = db.Subjects.FirstOrDefault(e => e.id == i);
                    if (A != null)
                    {
                        MessageBox.Show(A.Name);
                        if (Text3.Text != null)
                        {
                            A.Name = Text3.Text;
                        }
                        if (Text4.Text != "")
                        {
                            A.num = Convert.ToInt16(Text4.Text);
                        }
                        if (Text5.Text != "")
                        {
                            A.Cabinet = Tet1.Text;
                        }
                        var lessons = db.Lessons.Where(e => e.Name == A.Name).ToList();
                        foreach (Lesson lesson in lessons)
                        {
                            lessons[lesson.Id].Name = A.Name;
                        }
                        db.Lessons.UpdateRange(lessons);
                        db.Subjects.Update(A);
                        db.SaveChanges();
                        Text2.Clear();
                        Text3.Clear();
                        Text4.Clear();
                        Text5.Clear();

                        AddGrid();
                    }
                }
                else
                {
                    Text2.Background = Brushes.LightPink;
                    Text2.ToolTip = "Ошибка.";
                    Text3.Background = Brushes.LightPink;
                    Text3.ToolTip = "Ошибка.";
                    Text4.Background = Brushes.LightPink;
                    Text4.ToolTip = "Ошибка.";
                    Text5.Background = Brushes.LightPink;
                    Text5.ToolTip = "Ошибка.";
                }

            }
        }
        public void Add_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                int i = Convert.ToInt32(Text2.Text);
                if (db.Students.Any(e => e.id == i))
                {
                    Text2.Background = Brushes.White;
                    Text2.ToolTip = null;
                    Student? A = db.Students.FirstOrDefault(e => e.id == i);
                    if (A != null)
                    {
                        MessageBox.Show(A.fullName);
                        if (txtGroupName0.Text != null)
                        {
                            A.id = Convert.ToInt32(Text3.Text);
                        }
                        if (txtGroupName1.Text != null)
                        {
                            A.fullName = Text3.Text;
                        }
                        if (txtGroupName2.Text != null)
                        {
                            A.email = Text4.Text;
                        }
                        if (comboS.SelectedItem != null)
                        {
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                            A.group = db.Groups.First(e => e.Name == comboS.SelectedItem.ToString().Trim());
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
                        }
                        db.Students.Update(A);
                        db.SaveChanges();
                        Text2.Clear();
                        Text3.Clear();
                        Text4.Clear();
                        Text5.Clear();

                        AddGrid();
                    }
                }
                else
                {
                    Text2.Background = Brushes.LightPink;
                    Text2.ToolTip = "Предмета с данным ID не существует.";
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
                    if (A!=null) { if (Tet1.Text != null)
                        {
                            A.Name = Tet1.Text;
                            var lessons = db.Lessons.Where(e=>e.Group == A.Name).ToList();
                            foreach (Lesson lesson in lessons)
                            {
                                lessons[lesson.Id].Group = A.Name;
                            }
                            db.Lessons.UpdateRange(lessons);
                            var students = db.Students.Where(e => e.group == A).ToList();
                            foreach (Student lesson in students)
                            {
                                students[lesson.id].group = A;
                            }
                            db.Students.UpdateRange(students);
                            db.Groups.Update(A);
                            db.SaveChanges();
                            Text.Clear();
                            Tet1.Clear();

                        } 
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


        private void Text_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text != null)
            {
                if (!text.Text.All(char.IsDigit))
                {
                    text.Clear();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Groups a = db.Groups.FirstOrDefault(e => e.Id == Convert.ToInt32(Text.Text));
                if (a != null)
                {
                    db.Groups.Remove(a);
                    db.SaveChanges();
                }
            }
        }
        private void B_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Subjects a = db.Subjects.FirstOrDefault(e => e.id == Convert.ToInt32(Text.Text));
                if (a != null)
                {
                    db.Subjects.Remove(a);
                    db.SaveChanges();
                }
            }
        }
        private void Bu_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Student a = db.Students.FirstOrDefault(e => e.id == Convert.ToInt32(Text.Text));
                if (a != null)
                {
                    db.Students.Remove(a);
                    db.SaveChanges();
                }
            }
        }
    }
}
