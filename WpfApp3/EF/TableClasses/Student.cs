namespace WpfApp3.EF
{
    public class Student
    {
        public int id { get; set; }
        public string? fullName { get; set; }
       
        public string? email { get; set; }
        public Groups? group { get; set; }
    }
}
