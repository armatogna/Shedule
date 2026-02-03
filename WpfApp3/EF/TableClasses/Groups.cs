using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp3.EF.TableClasses;

namespace WpfApp3.EF
{
    public class Groups
    {
        public int Id { get; set; }
        public required string Name { get; set; } 
        public ICollection<GroupSubjects>? GS { get; set; } = new List<GroupSubjects>();
        public ICollection<Student>? students { get; set; }

        public int? AccountId { get; set; }
        public Groups()
        {
            
            students = new HashSet<Student>();
        }
    }
}
