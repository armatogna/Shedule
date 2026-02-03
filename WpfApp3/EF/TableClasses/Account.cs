using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.EF.TableClasses
{
    public class Account
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public ICollection<Groups>? Groups { get; set; }

        public ICollection<Lesson>? Lessons { get; set; }
        public byte[]? Data { get; set; }
    }
}
