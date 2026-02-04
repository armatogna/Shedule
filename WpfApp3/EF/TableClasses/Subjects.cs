using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp3.EF.TableClasses;

namespace WpfApp3.EF
{
    public class Subjects
    {
        public int id { get; set; }
        public string? Name { get; set; }
        public string? Cabinet { get; set; }
        public int num { get; set; } = 0;
        public ICollection<GroupSubjects>? GS { get; set; } = new List<GroupSubjects>();
        public ICollection<Lesson>? lessons { get; set; }
        
    }

}
