using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Newtonsoft.Json;
using WpfApp3.EF;
using WpfApp3.EF.TableClasses;
namespace WpfApp3
{
    public partial class Schedule : Window
    {
        public Schedule(Account account)
        {
            string f = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "JSONs\\Save.json");

            Dop d = Load(f);
            Shed();
            
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
        
        public void Shed()
        {
            string f = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "JSONs\\Save.json");
            InitializeComponent();
            string s = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "Resources\\icon.ico");
            Uri iconUri = new Uri(s, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            AddLesson();
            
            using (ApplicationContext db = new ApplicationContext())
            {
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                foreach (Groups group in db.Groups.Where(h => h.GS.Any(i => i.Subject.lessons != null)))
                {
                    groups1.Add(group);
                    DataGrid dataGrid1 = new DataGrid();
                    dataGrid1.Width = 410;
                    dataGrid1.AutoGenerateColumns = false;
                    DataGridTextColumn column = new();
                    column.Header = "День недели";
                    column.Width = 80;
                    column.Binding = new Binding("Day");
                    dataGrid1.Columns.Add(column);
                    column.Header = "Номер";
                    column.Width = 50;
                    column.Binding = new Binding("numDay");
                    dataGrid1.Columns.Add(column);
                    column.Header = "Название";
                    column.Width = 200;
                    column.Binding = new Binding("Name");
                    dataGrid1.Columns.Add(column);
                    column.Header = "Кабинет";
                    column.Width = 70;
                    column.Binding = new Binding("Cabinet");
                    dataGrid1.Columns.Add(column);
                    dataGrid1.ItemsSource = db.Lessons.Where(r => r.group == group.Name);
                    StackPanel panel = new StackPanel();
                    TextBlock block = new TextBlock();
                    block.Text = group.Name;
                    block.FontSize = 18;
                    panel.Children.Add(block);
                    dataGrid3.ItemsSource = db.Groups.ToList();
                    panel.Children.Add(dataGrid1);
                    Stack.Children.Add(panel);
                    /*<StackPanel>
                            <TextBlock Text="" FontSize="18"/>
                            <DataGrid x:Name="dataGrid" AutoGenerateColumns="False" Width="410">
                            <DataGrid.Columns>
                                <DataGridTextColumn Header="День недели" Width="90" Binding="{Binding Day}" />
                                <DataGridTextColumn Header="Номер урока" Width="50" Binding="{Binding numDay}" />
                                <DataGridTextColumn Header="Название" Width="200" Binding="{Binding Name}" />
                                <DataGridTextColumn Header="Кабинет" Width="70" Binding="{Binding Cabinet}" />
                                </DataGrid.Columns>
                            </DataGrid>
                        </StackPanel>*/
                    App app = new App();
                    app.PgDump(account1);
                    dataGrid3.ItemsSource = db.Groups.ToList();
                }
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            }
        }
        Account account1 = new()
        {
            Name = "Default",
            Password = "Password"
        };
        public Schedule(int b, int c, Account account)
        {
            string f = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "JSONs\\Save.json");
            Dop dop = new Dop()
            {
                b = b,
                c = c
            };
            Save(dop, f);
            Text.Text = $"{b}-{c}";
            Shed();
            account1 = account;
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
                foreach (Lesson lesson in db.Lessons.Where(j=>j.group == group.Name))
                {
                    if (lesson.NumDay == 1) { tableRow.Cells.Add(new TableCell(new Paragraph(new Run(lesson.Day)))); }
                    
                    tableRow.Cells.Add(new TableCell(new Paragraph(new Run(lesson.NumDay.ToString()))));
                    tableRow.Cells.Add(new TableCell(new Paragraph(new Run(lesson.group))));
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
        List<Lesson> list = new List<Lesson>();
        private async void AddLesson()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                list = db.Lessons.ToList();
                int id = db.Groups.Where(t => t.Name != null).Count();
                if (id != 0 && db.Subjects != null && db.Groups != null)
                {
                    List<Groups> groups = db.Groups.Where(t => t.Name != null).ToList();
                    groups = groups.Where(i => i.AccountId == account1.Id).ToList();
                    List<string> dayz = new List<string> { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
                    int e = 1;
                    int t = 5;
                    
                        foreach (string daq in dayz)
                        {
                            for (int i = e; i < t; i++)
                            {
                            foreach (Groups group in groups)
                            {
                                if (daq == "Понедельник" && i == 1)
                                {

                                    Lesson lesson = new()
                                    {

                                        Day = daq,
                                        NumDay = 1,
                                        group = group.Name.Trim().ToUpper(),
                                        Name = "Разговоры о важном",
                                        AccountId = account1.Id
                                    };
                                    db.Lessons.Add(lesson);
                                    db.SaveChanges();
                                    i = 2;
                                    break;
                                }
                                if (daq == "Четверг" && i == 4)
                                {

                                    
                                    Lesson lesson = new()
                                    {
                                        Day = daq,
                                        NumDay = 4,
                                        group = group.Name.Trim().ToUpper(),
                                        Name = "Классный час",
                                        AccountId = account1.Id
                                    };
                                    //t++;
                                    db.Lessons.Add(lesson);
                                    db.SaveChanges();
                                    break;
                                }
                                if (daq == "Пятница")
                                {
                                    t = 4;

                                }


                                /*Groups? group = null;
                                while (group == null) { group = await db.Groups.FindAsync(y); y++; }*/
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                                List<Subjects> dq = await db.Subjects.Where(e => e.GS.Any(q => q.Group == group)).ToListAsync();
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                                List<Subjects> sq = [];
                                    Random random = new Random();
                                    foreach (Subjects item in dq)
                                    {
                                        if (item.num > 0)
                                        {

                                            sq.Add(item);
                                        }
                                    }
                                    if (sq == null)
                                    {
                                        break;
                                    }
                                    int w = sq.Count -1;
                                Subjects subject = sq[random.Next(w)];
                                Lesson lesson1 = new()
                                {
                                    Day = daq,
                                    NumDay = i,
                                    group = group.Name.Trim().ToUpper(),
                                    Name = subject.Name,
                                    Cabinet = subject.Cabinet,
                                    AccountId = account1.Id
                                };
                                if (!db.Lessons.Any(r=>r.Cabinet == lesson1.Cabinet) && !db.Lessons.Any(r => r.Day == lesson1.Day) && !db.Lessons.Any(r => r.NumDay == lesson1.NumDay))
                                {
                                    subject.num--;
                                    db.Subjects.Update(subject);
                                    db.Lessons.Add(lesson1);
                                    db.SaveChanges();
                                }
                                MessageBoxResult message = MessageBox.Show("Вы хотите сохранить результат?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                                if (message == MessageBoxResult.Yes) 
                                {
                                    App app = new App();
                                    app.PgDump(account1);
                                }
                                else
                                {
                                    db.Lessons.UpdateRange(list);
                                    AddLesson();
                                }
                            } 
                        }
                    }
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
                    dataGrid2.ItemsSource = db.Students.Where(i => i.group == group);
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
            word.CreateWord();
            MimeMessage message = new MimeMessage();
            //message.From.Add()
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Word word = new Word();
            word.CreateWord();
        }
    }
}
