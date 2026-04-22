using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SUEMS.Models;
using SUEMS.Services;

class Program
{
    static void Main()
    {
        AuthService auth = new AuthService();

        while (true)
        {
            auth.StartLogin();

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}