
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore.Storage;
using WpfApp3.EF;
using WpfApp3.EF.TableClasses;

namespace WpfApp3;
public partial class MainWindow : Window
{

    public List<Groups> Groups { get; set; } = new List<Groups>();
    
    
    Account account1 = new()
    {
        Name = "Default",
        Password = "Password"
    };
    public MainWindow()
    {
        InitializeComponent();
        string s = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "Resources\\icon.ico");
        Uri iconUri = new Uri(s, UriKind.RelativeOrAbsolute);
        this.Icon = BitmapFrame.Create(iconUri);
        
        //AddSAndG();
    }
    List<Groups> groups1 = new();
    public MainWindow(Account account)
    {
        InitializeComponent();
        string s = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "Resources\\icon.ico");
        Uri iconUri = new Uri(s, UriKind.RelativeOrAbsolute);
        this.Icon = BitmapFrame.Create(iconUri);
        account1 = account;
        using (ApplicationContext db = new ApplicationContext())
        {
            //List<string> list = new();

            //List<Groups> groups = db.Groups.ToList();
            groups1 = [.. db.Groups.Where(i => i.AccountId == account1.Id)];
            comboS.ItemsSource = groups1.Select(i=>i.Name);

            //MessageBox.Show(groups.Count.ToString());
            //MessageBox.Show(groups1.Count.ToString());
            foreach (Groups subject in groups)
            {
                comboSubjects.Items.Add(subject.Name);
                //comboS.Items.Add(subject.Name);
                //comboS.Items.Refresh();
                //codmboS.Items.Add(subject.Name);
            }
        }
            AddSAndG();
    }
    public List<string> sub = new();
    public List<string> gro = new();
    List<Groups> groups = new();
    List<Subjects> subjects = new();
    List<Student> students = new();
    public void AddSAndG()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            groups = db.Groups.ToList();
            subjects = db.Subjects.ToList();
            students = db.Students.ToList();
            foreach (Groups subject in groups) { gro.Add(subject.Name); }
            foreach (string gr in gro) { comboSubjects.Items.Add(gr); }
        }
    }

    private void Text_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        TextBox text = sender as TextBox;
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
        if (!text.Text.All(char.IsDigit))
        {
            text.Clear();
        }
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
    }
    public List<string> subjects2 = new List<string>();
    private void AddGroupSubject_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(txtFullNam.Text) || string.IsNullOrEmpty(comboSubject.Text) || string.IsNullOrEmpty(txtFullNam1.Text))
        {
            txtFullNam.Background = Brushes.LightPink;
            txtFullNam.ToolTip = "Поле не может быть пустым.";
            comboSubject.Background = Brushes.LightPink;
            comboSubject.ToolTip = "Поле не может быть пустым.";
        }
        else
        {
            comboSubject.Background = Brushes.White;
            comboSubject.ToolTip = null;
            txtFullNam.Background = Brushes.Gray;
            txtFullNam.ToolTip = null;
            
            if (!string.IsNullOrEmpty(comboSubject.Text) && !string.IsNullOrEmpty(comboSubjects.SelectedItem as string))
            {
                comboSubject.Background = Brushes.White;
                comboSubject.ToolTip = null;
                comboSubjects.Background = Brushes.Gray;
                comboSubjects.ToolTip = null;
                List<Groups> groups = new List<Groups>();
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (string item in List.Items)
                    {
                        if (db.Groups.Any(e => e.Name == item))
                        {
                            groups.Add(db.Groups.First(e => e.Name == item)); 
                        }
                    } 
                }
                if (Groups != null && !gro.Contains(comboSubject.Text) && comboSubjects.SelectedItem!=null)
                {
#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
                    string groupName = comboSubjects.SelectedItem as string;
#pragma warning restore CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
                              //Groups fGroup = Groups.First(p => p.Name == groupName.ToUpper());

                    using (ApplicationContext db = new ApplicationContext())
                    {
                    string name = (string)comboSubject.Text;
                    Subjects subjects1 = new Subjects()
                    {
                        Name = name.ToUpper(),
                        Cabinet = txtFullNam.Text.ToUpper().Trim(),
                        num = Convert.ToInt32(txtFullNam1.Text.Trim())
                    };
                        db.Subjects.Add(subjects1);
                    db.SaveChanges();
                        subjects1 = db.Subjects.FirstOrDefault(r => r.Name == subjects1.Name);
                        MessageBox.Show(subjects1.id.ToString());
                    foreach (Groups item in groups)
                    {
                        GroupSubjects groupSubject = new()
                        {
                            Group = item.Id,
                            Subject = subjects1.id
                        };
                            //MessageBox.Show(groupSubject.Subject.ToString() + " " + groupSubject.Group.ToString());
                            item.GS.Add(groupSubject);
                            //MessageBox.Show(groupSubject.Subject.ToString() + " " + groupSubject.Group.ToString());
                            subjects1.GS.Add(groupSubject);
                           /* GroupSubjects groupSubject1 = new()
                            {
                                Group = item.Id,
                                Subject = subjects1.id
                            };*/
                            //MessageBox.Show(groupSubject.Subject.ToString() + " " + groupSubject.Group.ToString());
                            db.groupSubjects.Add(groupSubject);
                            db.Groups.Update(item);
                            db.Subjects.Update(subjects1);
                            db.SaveChanges();

                        }
                        

                    
                    
                        if (!subjects.Contains(subjects1))
                        {
                            txtFullNam.Clear();
                            comboSubject.Clear();
                            //AddSubject(subjects1);
                            A_Button.ToolTip = null;
                            A_Button.Background = Brushes.White;
                        }
                        else
                        {
                            A_Button.ToolTip = "Такой предмет уже существует";
                            A_Button.Background = Brushes.LightPink;
                        }
                        //db.Subjects.Add(subjects1);
                        //db.SaveChanges();
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка!");
                    comboSubject.Clear();
                }
            }
            else
            {
                comboSubject.Background = Brushes.LightPink;
                comboSubject.ToolTip = "Поле не может быть пустым.";
                comboSubjects.Background = Brushes.LightPink;
                comboSubjects.ToolTip = "Поле не может быть пустым.";
            }

        }
        comboSubject.Clear();
        txtFullNam.Clear();
        txtFullNam1.Clear();
    }
    private void AddGroup_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(txtGroupName.Text))
        {
            txtGroupName.Background = Brushes.LightPink;

            txtGroupName.ToolTip = "Поле не может быть пустым.";
        }
        else
        {
            txtGroupName.Background = Brushes.White;

            txtGroupName.ToolTip = null;

#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            Groups group = new Groups
            {
                Name = txtGroupName.Text.ToUpper().Trim(),
                AccountId = account1.Id
            };
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
            Groups.Add(group);
            comboSubjects.Items.Add(group.Name);
            comboS.ItemsSource+=group.Name;
            
            if (!groups.Any(u=>u.Name == group.Name))
            {
                txtGroupName.Clear();
                AddGroup(group);
                A_But.ToolTip = null;
                A_But.Background = Brushes.White;
            }
            else
            {
                A_But.ToolTip = "Такая группа уже существует";
                A_But.Background = Brushes.LightPink;
            }
            
        }
    }
    private void AddStudent_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("1");
        if (string.IsNullOrEmpty(txtGroupName1.Text))
        {
            txtGroupName1.Background = Brushes.LightPink;

            txtGroupName1.ToolTip = "Поле не может быть пустым.";
        }
        else
        {
            txtGroupName1.Background = Brushes.White;

            txtGroupName1.ToolTip = null;
        }
        if (string.IsNullOrEmpty(txtGroupName2.Text))
        {
            txtGroupName2.Background = Brushes.LightPink;

            txtGroupName2.ToolTip = "Поле не может быть пустым.";
        }
        else
        {


            txtGroupName2.Background = Brushes.White;

            txtGroupName2.ToolTip = null;
        }
        MessageBox.Show("2");
        if (string.IsNullOrEmpty(comboS.SelectedItem as string))
        {
            MessageBox.Show(comboS.SelectedItem.ToString());
        }
            if (!string.IsNullOrEmpty(comboS.SelectedItem as string))
        {
            comboS.Background = Brushes.Gray;
            comboS.ToolTip = null;
            MessageBox.Show("3");
            if (comboS.SelectedItem != null)
            {

                using (ApplicationContext db = new ApplicationContext())
                {
                    Groups fGroup = db.Groups.FirstOrDefault(p => p.Name == comboS.SelectedItem.ToString());
                
                if (fGroup == null) { MessageBox.Show("Группа не найдена");  }
                Student student = new()
                {
                    fullName = txtGroupName1.Text.ToUpper().Trim(),
                    email = txtGroupName2.Text.ToUpper().Trim(),
                    group = fGroup
                };

                    MessageBox.Show("4");
                    txtGroupName1.Clear();
                txtGroupName2.Clear();
                
                    if (!students.Contains(student))
                    {
                        txtGroupName1.Clear();
                        txtGroupName2.Clear();
                        db.Students.Add(student);
                        db.SaveChanges();
                        Button.ToolTip = null;
                        Button.Background = Brushes.White;
                    }
                    else
                    {
                        Button.ToolTip = "Такой уже существует";
                        Button.Background = Brushes.LightPink;
                    }


                    MessageBox.Show("5");
                }
            }
            else
            {
                Button.ToolTip = "Такой группы не существует";
                Button.Background = Brushes.LightPink;
            }
        }
        else
        {
            comboS.Background = Brushes.LightPink;
            comboS.ToolTip = "Поле не может быть пустым.";
        }
    }
    
    private void A_Click(object sender, RoutedEventArgs e)
    {
        int q = 0;
        if (string.IsNullOrEmpty(txtFullName5.Text))
        {
            txtFullName5.Background = Brushes.LightPink;
           txtFullName5.ToolTip = "Поле не может быть пустым.";
            q = 1;
        }
        else
        {
            txtFullName5.Background = Brushes.White;
            txtFullName5.ToolTip = null;
        }
        
        if (q != 1)
        {
            int s = Convert.ToInt32(txtFullName5.Text);
            int d = Convert.ToInt32(txtFullName1.Text);
            if (s != 0 && d!=0 && account1!=null)
            {
                Schedule schedule = new Schedule(s, d, account1);
            schedule.Show();
            this.Close();

            }
            
        }
    }
  
    private void AddGroup(Groups group)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                db.Groups.Add(group);

                MessageBox.Show("Access");
                db.SaveChanges();
                transaction.Commit();
            }

        }
    }
   
    private void AddSubject(Subjects subject)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            
                db.Subjects.Add(subject);


                db.SaveChanges();


        }
    }

    private void B_Click(object sender, RoutedEventArgs e)
    {
        if (!List.Items.Contains(comboSubjects.SelectedItem.ToString()))
        {
        List.Items.Add(comboSubjects.SelectedItem.ToString());

        }
        else { MessageBox.Show("Группа уже добавлена"); }
    }

    private void txtFullNam_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
}


