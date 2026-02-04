using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApp3.EF.TableClasses
{
    public class GroupSubjects
    {
        public int Id { get; set; }
        public required int Group { get; set; }
        public required int Subject { get; set; }
    }
}
