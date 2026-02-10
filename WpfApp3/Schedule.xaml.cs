using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Newtonsoft.Json;
using Npgsql;
using WpfApp3.EF;
using WpfApp3.EF.TableClasses;
namespace WpfApp3
{
    public partial class Schedule : Window
    {
        public Schedule(Account account, bool t)
        {
            string f = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "JSONs\\Save.json");
            string s = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "Resources\\icon.ico");
            Uri iconUri = new Uri(s, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

            Dop d = Load(f);
            Shed(account, t);
            Text.Text = d.c + " " + d.b;
            account1 = account;
        }

        public static void Save(Dop d, string path)
        {
            string js = JsonConvert.SerializeObject(d, Formatting.Indented);
            File.WriteAllText(path, js);
        }
        public Dop? Load(string path)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<Dop>(json);

            }
            return null;
        }
        
        List<Groups> groups1 = new();
        DataGrid dataGrid1 = new();
        public void Shed(Account account, bool t)
        {
            InitializeComponent();
            string f = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "JSONs\\Save.json");
            string s = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "Resources\\icon.ico");
            Uri iconUri = new Uri(s, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            account1 = account;
            //MessageBox.Show($"{account1.Name}{account1.Id.ToString()}");
            if (t==true) { AddLesson(); }
            
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Subjects> d = db.Subjects.Where(r=>r.lessons != null).ToList();
                List<int> subId = d.Select(p=>p.id).ToList();
                List<GroupSubjects> groupSubjects = db.groupSubjects.Where(y=>subId.Contains(y.Subject)).ToList();
                List<Groups> gr = db.Groups.ToList();
                List<Groups> gro = db.Groups.ToList();

                foreach (Groups group in gr)
                {
                    if (group.GS==null) { break; }
                    List<GroupSubjects> GS = group.GS.ToList();
                    foreach(GroupSubjects group1 in GS)
                    {
                        if(groupSubjects.Contains(group1) && !gro.Contains(group))
                        {
                            gro.Add(group);
                        }
                    }
                }
                List<Groups> groups = new();
                using (ApplicationContext ddb = new ApplicationContext())
                {
                    d = ddb.Subjects.Where(r => r.lessons != null).ToList();
                    List<int> subId1 = d.Select(p => p.id).ToList();

                    groups = ddb.Groups.Where(h => h.GS.Any(i => subId.Contains(i.Subject))).ToList();
                }
                foreach (Groups group in groups)
                {

                    DataGrid dataGrid1 = new();
                    groups1.Add(group);
                    dataGrid1.Width = 410;
                    dataGrid1.AutoGenerateColumns = false;
                    DataGridTextColumn column = new();
                    column.Header = "День недели";
                    column.Width = 80;
                    column.Binding = new Binding("Day");
                    column.SortMemberPath = "Day";
                    if (dataGrid1.Columns.Contains(column)) { dataGrid1.Columns.Remove(column); }
                    dataGrid1.Columns.Add(column);
                    DataGridTextColumn column1 = new();
                    column1.Header = "Номер";
                    column1.Width = 50;
                    column1.Binding = new Binding("NumDay");
                    if (dataGrid1.Columns.Contains(column1)) { dataGrid1.Columns.Remove(column); }
                    dataGrid1.Columns.Add(column1);
                    DataGridTextColumn column2 = new();
                    column2.Header = "Название";
                    column2.Width = 200;
                    column2.Binding = new Binding("Name");
                    if (dataGrid1.Columns.Contains(column2)) { dataGrid1.Columns.Remove(column); }
                    dataGrid1.Columns.Add(column2);
                    DataGridTextColumn column3 = new();
                    column3.Header = "Кабинет";
                    column3.Width = 70;
                    column3.Binding = new Binding("Cabinet");
                    if (dataGrid1.Columns.Contains(column3)) { dataGrid1.Columns.Remove(column); }
                    dataGrid1.Columns.Add(column3);

                    //db.Lessons.Where(r => r.group == group.Name).Load();
                    //dataGrid1.ItemsSource = db.Lessons.Local.ToObservableCollection();
                    List<Lesson> tr = db.Lessons.Where(r => r.Group == group.Name).ToList();

                    dataGrid1.ItemsSource = tr;
                    dataGrid1.Items.SortDescriptions.Add(new SortDescription("Day", ListSortDirection.Ascending));
                    StackPanel panel = new StackPanel();
                    panel.Height = 700;
                    TextBlock block = new TextBlock();
                    block.Text = group.Name;
                    block.FontSize = 18;
                    panel.Children.Add(block);
                    dataGrid3.ItemsSource = db.Groups.ToList();
                    panel.Children.Add(dataGrid1);
                    Stack.Children.Add(panel);
                }
            }
        }
        Account account1 = new()
        {
            Name = "Default",
            Password = "Password"
        };
        public Schedule(int r, int t, Account account)
        {
            InitializeComponent();
            string f = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "JSONs\\Save.json");
            string s = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "Resources\\icon.ico");
            Uri iconUri = new Uri(s, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

            Dop dop = new Dop()
            {
                b = r,
                c = t
            };
            Save(dop, f);
            Text.Text = r + "-" + t;
            account1 = account;
            //MessageBox.Show($"{account1.Name}{account1.Id.ToString()}");
            Shed(account, true);
        }

        

        private FlowDocument Print(List<Groups> groups)
        {
            FlowDocument document = new();
            document.PageHeight = 11.69 * 96;
            document.PageWidth = 8.27 * 96;
            document.PagePadding = new Thickness(50);
            foreach (Groups group in groups)
            {
                Table table = CreateTable(group);
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run($"Группа: {group.Name}"));
                document.Blocks.Add(paragraph);
                document.Blocks.Add(table);
            }
            return document;
        }

        private Table CreateTable(Groups group)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Table table = new Table();
                for (int i = 0; i < 4; i++) { table.Columns.Add(new TableColumn()); }
                TableRow tableRow = new();
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run("День"))));
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run("Номер"))));
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run("Название"))));
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run("Кабинет"))));
                table.RowGroups.Add(new TableRowGroup());
                table.RowGroups[0].Rows.Add(tableRow);
                foreach (Lesson lesson in db.Lessons.Where(j=>j.Group == group.Name))
                {
                    if (lesson.NumDay == 1) { tableRow.Cells.Add(new TableCell(new Paragraph(new Run(lesson.Day)))); }
                    
                    tableRow.Cells.Add(new TableCell(new Paragraph(new Run(lesson.NumDay.ToString()))));
                    tableRow.Cells.Add(new TableCell(new Paragraph(new Run(lesson.Group))));
                    tableRow.Cells.Add(new TableCell(new Paragraph(new Run(lesson.Cabinet))));
                    table.RowGroups.Add(new TableRowGroup());
                    table.RowGroups[0].Rows.Add(tableRow);
                }
                return table;
            } 
        }
        /*public void Search_Click(object sender, RoutedEventArgs e)
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
           

        }*/
        public void S_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            this.Close();
        }
        public void A_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow open = new(account1);
            open.Show();
            this.Close();
        }
        List<Subjects> list = new();
        private void AddLesson()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                int id = db.Groups.Where(t => t.Name != null).Count();
                if (id != 0 && db.Subjects != null)
                {
                    List<Groups> groups = db.Groups.Where(t => t.Name != null).ToList();
                    //MessageBox.Show(groups.Count.ToString());
                    groups = groups.Where(i => i.AccountId == account1.Id).ToList();
                    groups = groups.Where(i => i.GS != null).ToList();
                    //MessageBox.Show(account1.Id.ToString());
                    //MessageBox.Show(groups.Count.ToString());
                    List<string> dayz = new List<string> { "1.Понедельник", "2.Вторник", "3.Среда", "4.Четверг", "5.Пятница" };

                    int j = 0;
                        foreach (string daq in dayz)
                        {

                        int e = 1;
                        int t = 5;
                            for (int i = e; i < t; i++)
                            {

                            foreach (Groups group in groups)
                            {
                                bool tr = false;
                                /* switch (daq)
                                 {
                                     case "Понедельник":

                                         break;
                                 }*/
                                using (var connection = new NpgsqlConnection("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=ksanox"))
                                {
                                    connection.Open();

                                    // Добавление записи для понедельника
                                    if (daq == "1.Понедельник" && i == 1)
                                    {
                                        tr = true;
                                        using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO public.\"Lessons\" (\"Day\", \"NumDay\", \"Group\", \"Name\", \"AccountId\") VALUES (@Day, @NumDay, @Group, @Name, @AccountId)", connection))
                                        {
                                            command.Parameters.AddWithValue("@Day", "1.Понедельник (1)");
                                            command.Parameters.AddWithValue("@NumDay", 1);
                                            command.Parameters.AddWithValue("@Group", group.Name.Trim().ToUpper());
                                            command.Parameters.AddWithValue("@Name", "Разговоры о важном");
                                            command.Parameters.AddWithValue("@AccountId", account1.Id);

                                            command.ExecuteNonQuery();
                                        }
                                    }

                                    // Добавление записи для четверга
                                    if (daq == "4.Четверг" && i == 4)
                                    {
                                        tr = true;
                                        using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO public.\"Lessons\" (\"Day\", \"NumDay\", \"Group\", \"Name\", \"AccountId\") VALUES (@Day, @NumDay, @Group, @Name, @AccountId)", connection))
                                        {
                                            command.Parameters.AddWithValue("@Day", "4.Четверг (4)");
                                            command.Parameters.AddWithValue("@NumDay", 4);
                                            command.Parameters.AddWithValue("@Group", group.Name.Trim().ToUpper());
                                            command.Parameters.AddWithValue("@Name", "Классный час");
                                            command.Parameters.AddWithValue("@AccountId", account1.Id);

                                            command.ExecuteNonQuery();
                                        }
                                    }
                                    connection.Close();
                                }
                                if (daq == "5.Пятница")
                                {
                                    t = 4;

                                }
                                if (tr == false) { AddDay(daq, i, group); }
                            }

                        }
                    }
                    //MessageBox.Show(j.ToString());
                }
                MessageBoxResult message = MessageBox.Show("Вы хотите сохранить результат?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                            if (message == MessageBoxResult.Yes)
                            {
                                //App app = new App();
                                //PgDump(account1);
                                Word word = new Word();
                    word.CreateWord(documentViewer);
                            }
                            else
                            {
                    db.Subjects.UpdateRange(list);
                    db.SaveChanges();
                                AddLesson();
                }

            }

            


            }

        private async void AddDay(string daq, int i, Groups group)
        {
            int v = 30;
            while (v > 0)
            {


                using (ApplicationContext db = new())
                {
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                    List<Subjects> dq = await db.Subjects.Where(e => e.GS.Any(q => q.Group == group.Id)).ToListAsync();
                    if (dq == null)
                    {
                        //MessageBox.Show(dq.Count.ToString() + "ошибкаdq");
                        break;
                    }

#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.


                    List<Subjects> sq = new();
                    Random random = new Random();
                    foreach (Subjects item in dq)
                    {
                        //ih++;
                        if (item.num > 0)
                        {
                            //ij++;
                            sq.Add(item);
                        }
                    }
                    //MessageBox.Show(ih.ToString() + "dq + "+ij);

                    if (sq.Count == 0)
                    {
                        //MessageBox.Show(sq.Count.ToString() + "ошибкаsq");
                        break;
                    }
                    int w = sq.Count - 1;
                    //MessageBox.Show(sq.Count.ToString()+"sq");
                    Subjects subject = sq[random.Next(w)];
                    Lesson lesson1 = new()
                    {
                        Day = daq+" ("+i+")",
                        NumDay = i,
                        Group = group.Name.Trim().ToUpper(),
                        Name = subject.Name,
                        Cabinet = subject.Cabinet,
                        AccountId = account1.Id
                    };
                    //MessageBox.Show(lesson1.Name + " " + lesson1.Cabinet);
                    /*if (db.Lessons.Any(r => r.Cabinet == lesson1.Cabinet) && db.Lessons.Any(r => r.Day == lesson1.Day) && db.Lessons.Any(r => r.NumDay == lesson1.NumDay) && db.Lessons.Any(r => r.group == lesson1.group))
                    {
                        break;
                    }
                    else
                    {
                    */
                    subject.num--;
                    db.Subjects.Update(subject);
                    db.Lessons.Add(lesson1);
                    db.SaveChanges();
                    v = 0;
                    //tr.Add(lesson1);
                    //dataGrid1.Items.Add(lesson1);
                    //j++;
                    //}
                    //MessageBox.Show("Успех");
                }
            }
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            { 
                
                Groups group = db.Groups.FirstOrDefault(i => i.Name == Text1.Text.Trim().ToUpper());
                if (group != null)
                {
                    List<Student> students = db.Students.Where(i => i.group == group).ToList();
                    foreach (Student student in students)
                    {
                    dataGrid2.Items.Add(student);

                    }
                }
                else 
                {
                    MessageBox.Show("Данной группы не существует");
                }
                Text1.Clear();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new();
            if (dialog.ShowDialog() == true)
            {
                FlowDocument document = Print(groups1);
                dialog.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, "Таблица уроков");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Word word = new Word();
            word.CreateWord(documentViewer);
            MimeMessage message = new MimeMessage();
            //message.From.Add()
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Word word = new Word();
            word.CreateWord(documentViewer);
        }
    }
}
