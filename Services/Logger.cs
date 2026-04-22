using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace SUEMS.Services
{
    public class Logger
    {
        private string filePath = "log.txt";

        public void Log(string message)
        {
            string log = $"{DateTime.Now} - {message}";

            File.AppendAllText(filePath, log + Environment.NewLine);
        }
    }
}