using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEMS.Interfaces
{
    public interface ILogin
    {
        bool Login(string username, string password);
        void Logout();
    }
}