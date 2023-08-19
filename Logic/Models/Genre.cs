using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    [Flags]
    public enum Genre
    {
        Action =1, 
        Horror = 2,
        Commedy = 4,
        Documentary = 8,
        History = 16
    }
}
