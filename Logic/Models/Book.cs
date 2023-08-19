using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Book : BasicPaper
    {
        public string AuthorName { get;  set; }
        public Book(string paperName, string authorName,string companyName, DateTime publichDate, Genre ganre, double price, double discount = 0) : base(paperName,companyName, publichDate, ganre, price, discount)
        {
            if (authorName == null)
                throw new ArgumentNullException("Author name is null");
            AuthorName = authorName;
        }
        public override string ToString()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            return $"{PaperName} written by {AuthorName},rent price for 14 days before discount:{Price:c} and after discount {(1 - Discount / 100) * Price:c}";
        }

    }
}
