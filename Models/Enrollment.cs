using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEMS.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public Enrollment() { }

        public Enrollment(int studentId, int courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
        }
    }
}