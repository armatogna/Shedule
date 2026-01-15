using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using WpfApp3.EF;

namespace WpfApp3;
public partial class MainWindow : Window
{

    public List<string?> Subjects { get; set; } = new List<string?>();
    public List<Groups> Groups { get; set; } = new List<Groups>();
    public List<string?> Group { get; set; } = new List<string?>();

    public MainWindow()
    {
        InitializeComponent();
        string s = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\Debug\\net8.0-windows10.0.19041.0\\", "Resources\\ico4.bmp");
        Uri iconUri = new Uri(s, UriKind.RelativeOrAbsolute);
        this.Icon = BitmapFrame.Create(iconUri);
        AddSAndG();
    }

    private void AddSubject_Click(object sender, RoutedEventArgs e)
    {
        string subject = txtSubjectName.Text.Trim();
        comboSubject.Items.Add(subject);
        
        txtSubjectName.Clear();
    }

    public void AddSAndG()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var group = db.Groups.ToList();
            var subjects = db.Subjects.ToList();
            foreach (Subjects subject in subjects) { comboSubject.Items.Add(subject.Name); }
            foreach (Groups subject in group) { comboSubject.Items.Add(subject.Name); }
        }
    }


    public List<string> subjects2 = new List<string>();
    private void AddGroupSubject_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(txtFullNam.Text) || string.IsNullOrEmpty(txtGroupN.Text))
        {
            txtFullNam.Background = Brushes.LightPink;
            txtFullNam.ToolTip = "Поле не может быть пустым.";
            txtGroupN.Background = Brushes.LightPink;
            txtGroupN.ToolTip = "Поле не может быть пустым.";
        }
        else
        {
            txtFullNam.Background = Brushes.White;
            txtFullNam.ToolTip = null;
            txtGroupN.Background = Brushes.White;
            txtGroupN.ToolTip = null;
            if (!string.IsNullOrEmpty(comboSubject.SelectedItem as string) && !string.IsNullOrEmpty(comboSubjects.SelectedItem as string))
            {
                comboSubject.Background = Brushes.Gray;
                comboSubject.ToolTip = null;
                comboSubjects.Background = Brushes.Gray;
                comboSubjects.ToolTip = null;
                if (Groups != null)
                {
                    string groupName = comboSubjects.SelectedItem as string;
                    Groups fGroup = Groups.First(p => p.Name == groupName.ToUpper());
                    string name = (string)comboSubject.SelectedItem;
                    Subjects subjects1 = new Subjects()
                    {
                        Name = name.ToUpper(),
                        Hours = Convert.ToInt32(txtGroupN.Text.ToUpper().Trim()),
                        Cabinet = txtFullNam.Text.ToUpper().Trim(),
                        GroupId = fGroup.Id,
                        Weeks = Convert.ToInt32(txtGroupN.Text.ToUpper().Trim()) / 34
                    };
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        if (!db.Subjects.Any(e => e.Name == subjects1.Name && e.GroupId == subjects1.GroupId))
                        {
                            txtGroupN.Clear();
                            txtFullNam.Clear();
                            AddSubject(subjects1);
                            A_Button.ToolTip = null;
                            A_Button.Background = Brushes.White;
                        }
                        else
                        {
                            A_Button.ToolTip = "Такой урок уже существует";
                            A_Button.Background = Brushes.LightPink;
                        }
                    }
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

            Groups group = new Groups
            {
                Name = txtGroupName.Text.ToUpper().Trim(),
            };
            Groups.Add(group);
            txtGroupName.Clear();
            comboSubjects.Items.Add(group.Name);
            AddGroup(group);
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
        if (string.IsNullOrEmpty(txtFullNam5.Text))
        {
            txtFullNam5.Background = Brushes.LightPink;
            txtFullNam5.ToolTip = "Поле не может быть пустым.";
            q = 1;
        }
        else
        {
            txtFullNam5.Background = Brushes.White;
            txtFullNam5.ToolTip = null;
        }
        if (q != 1)
        {
            int s = Convert.ToInt32(txtFullName5.Text);
            int d = Convert.ToInt16(txtFullNam5.Text.Trim());
            Schedule schedule = new Schedule(s, d);
            schedule.Show();
            this.Close();
        }
    }
  
    private void AddGroup(Groups group)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            

                db.Groups.Add(group);


                db.SaveChanges();


               
            

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
   
}


