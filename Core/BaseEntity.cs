using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SUEMS.Core
{
    public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public BaseEntity()
    {
        CreatedAt = DateTime.Now;
    }

    public abstract void DisplayInfo();
}
}