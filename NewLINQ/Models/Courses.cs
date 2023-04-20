using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLINQ.Models
{
    internal class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; } = null!;
        public Teacher Teacher { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
