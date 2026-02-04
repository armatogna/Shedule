using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Newtonsoft.Json;
using PgDump;
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
            Shed(account);
            
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
        
        public void Shed(Account account)
        {
            string f = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "JSONs\\Save.json");
            InitializeComponent();
            string s = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "Resources\\icon.ico");
            Uri iconUri = new Uri(s, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            account1 = account;
            MessageBox.Show($"{account1.Name}{account1.Id.ToString()}");
            AddLesson();
            
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
                //string q = "";
                //foreach (int id in subId) { q+=" "+id.ToString(); }
                //MessageBox.Show(gro.Count.ToString());
                //Groups[] groups = db.Groups.Where(h => h.GS.Any(i => subId.Contains(i.Subject))).ToArray();
                //MessageBox.Show(groups.Count().ToString());
                //int[] t = db.Groups.Select(p => p.Id).ToArray();

                //q = "";

                //foreach (int id in t) { q += " "+id.ToString(); }
                //MessageBox.Show(q.ToString());
                foreach (Groups group in gro)
                {
                    MessageBox.Show("Вот");
                    groups1.Add(group);
                    DataGrid dataGrid1 = new DataGrid();
                    dataGrid1.Width = 410;
                    dataGrid1.AutoGenerateColumns = false;
                    DataGridTextColumn column = new();
                    column.Header = "День недели";
                    column.Width = 80;
                    column.Binding = new Binding("Day");
                    if (dataGrid1.Columns.Contains(column)) { dataGrid1.Columns.Remove(column); }
                    dataGrid1.Columns.Add(column);
                    DataGridTextColumn column1 = new();
                    column1.Header = "Номер";
                    column1.Width = 50;
                    column1.Binding = new Binding("numDay");
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
                    db.Lessons.Where(r => r.group == group.Name).Load();
                    dataGrid1.ItemsSource = db.Lessons.Local.ToObservableCollection();
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
                    
                    //PgDump(account1);
                    dataGrid3.ItemsSource = db.Groups.ToList();
                }
            }
        }
        /*public async void PgDump(Account account)
        {
            ConnectionOptions options = new ConnectionOptions("localhost", 5432, "postgres", "ksanox", "postgres");
            PgClient client = new PgClient(options);

            FileOutputProvider outputProvider = new FileOutputProvider("C:\\Program Files\\PostgreSQL\\17\\pgAdmin 4\\runtime\\dump.tar");

            await client.DumpAsync(outputProvider, timeout: TimeSpan.FromMinutes(1));
            /*string PDPath = @"C:\ProgramFiles\PostgreSQL\17\bin\pg_dump.exe";
            string db = "postgres";
            string user = "postgres";
            string pass = "ksanox";
            string dump = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Dump.tar");
            string command = $"\"{PDPath}\"-U{user}-d{db}-f{dump}";
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = $"/C{command}";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            try
            {


                using (ApplicationContext dd = new ApplicationContext())
                {
                    string dump = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Dump.tar");
                    byte[] data = File.ReadAllBytes(dump);
                    account.Data = data;
                    dd.Accounts.Update(account);


                    MessageBox.Show($"Сохранение прошло успешно.");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }*/
        Account account1 = new()
        {
            Name = "Default",
            Password = "Password"
        };
        public Schedule(int r, int t, Account account)
        {

            string f = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "JSONs\\Save.json");
            
            Dop dop = new Dop()
            {
                b = r,
                c = t
            };
            Save(dop, f);
            if (Text != null && r != null && t != null)
            {
                Text.Text = r + "-" + t;
            }
            account1 = account;
            MessageBox.Show($"{account1.Name}{account1.Id.ToString()}");
            Shed(account);
            
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
        List<Subjects> list = new();
        private async void AddLesson()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                int id = db.Groups.Where(t => t.Name != null).Count();
                if (id != 0 && db.Subjects != null)
                {
                    List<Groups> groups = db.Groups.Where(t => t.Name != null).ToList();
                    MessageBox.Show(groups.Count.ToString());
                    groups = groups.Where(i => i.AccountId == account1.Id).ToList();

                    MessageBox.Show(account1.Id.ToString());
                    MessageBox.Show(groups.Count.ToString());
                    List<string> dayz = new List<string> { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
                    
                    
                        foreach (string daq in dayz)
                        {
                        int e = 1;
                        int t = 5;
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
                                    MessageBox.Show("пон");
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
                                List<Subjects> dq = await db.Subjects.Where(e => e.GS.Any(q => q.Group == group.Id)).ToListAsync();
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.

                                MessageBox.Show(dq.Count.ToString()+"dq");
                                List<Subjects> sq = [];
                                    Random random = new Random();
                                    foreach (Subjects item in dq)
                                    {
                                        if (item.num > 0)
                                        {

                                            sq.Add(item);
                                        }
                                    }
                                    if (sq.Count==0)
                                    {
                                        break;
                                    }
                                    int w = sq.Count -1;
                                MessageBox.Show(sq.Count.ToString()+"sq");
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
                                    //App app = new App();
                                    //PgDump(account1);
                                    Word word = new Word();
                                    word.CreateWord();
                                }
                                else
                                {
                                    db.Subjects.UpdateRange(list);
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
