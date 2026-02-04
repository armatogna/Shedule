namespace WpfApp3.EF
{
    public class Student
    {
        public int id { get; set; }
        public string? fullName { get; set; }
       
        public string? email { get; set; }
        public required Groups group { get; set; }
    }
}
