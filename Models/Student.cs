using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SUEMS.Core;
using SUEMS.Interfaces;

namespace SUEMS.Models
{
    public class Student : Person, ILogin
    {
        public string RollNumber { get; set; }
        public string Department { get; set; }

        // extra OOP state
        private bool isLoggedIn = false;

        public Student() { }

        public Student(string name, int age, string roll, string dept)
            : base(name, age)
        {
            RollNumber = roll;
            Department = dept;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[STUDENT] {Name} | Roll: {RollNumber} | Dept: {Department}");
        }

        // Interface implementation
        public bool Login(string username, string password)
        {
            if (username == RollNumber && password == "123")
            {
                isLoggedIn = true;
                Console.WriteLine("Student logged in successfully");
                return true;
            }

            Console.WriteLine("Login failed");
            return false;
        }

        public void Logout()
        {
            isLoggedIn = false;
            Console.WriteLine("Student logged out");
        }
    }
}