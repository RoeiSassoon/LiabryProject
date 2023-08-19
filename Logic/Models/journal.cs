using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class journal : BasicPaper
    {
        public journal(string paperName,string companyName, DateTime publichDate, Genre ganre, double price, double discount =0) : base(paperName,companyName, publichDate, ganre, price, discount)
        {
        }
    }
}
