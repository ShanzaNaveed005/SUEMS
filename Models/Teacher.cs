using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEMS.Models
{
    public class Teacher : Person
    {
        public string EmployeeId { get; set; }
        public string Specialization { get; set; }

        public Teacher() { }

        public Teacher(string name, int age, string empId, string spec)
            : base(name, age)
        {
            EmployeeId = empId;
            Specialization = spec;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[TEACHER] {Name} | EmpID: {EmployeeId} | Spec: {Specialization}");
        }
    }
}