using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.EF.TableClasses
{
    public class Lesson
    {
        public int Id { get; set; }
        public string? Day { get; set; }
        public int NumDay {  get; set; }
        public string? group {  get; set; }
        public string? Name { get; set; }
        public string? Cabinet { get; set; }
        public int AccountId { get; set; }
    }
}
