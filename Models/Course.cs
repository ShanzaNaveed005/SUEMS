using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEMS.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int CreditHours { get; set; }

        // Association: Course ↔ Teacher
        public Teacher AssignedTeacher { get; set; }

        public Course() { }

        public Course(int id, string name, int credits)
        {
            CourseId = id;
            CourseName = name;
            CreditHours = credits;
        }

        public void ShowCourse()
        {
            Console.WriteLine($"Course: {CourseName} | Credits: {CreditHours}");

            if (AssignedTeacher != null)
                Console.WriteLine($"Taught by: {AssignedTeacher.Name}");
        }
    }
}