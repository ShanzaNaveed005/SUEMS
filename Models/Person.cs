using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SUEMS.Core;

namespace SUEMS.Models
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person() { }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"ID: {Id}, Name: {Name}, Age: {Age}");
        }
    }
}