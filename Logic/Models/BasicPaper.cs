using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic.Models
{
    public abstract class BasicPaper
    {
        public int id { get; set; }
        public string PaperName { get; set; }
        public string CompanyName { get; set; }
        public DateTime PublishedDate { get; set; }
        public Genre Genre { get; set; }

        public static int count = 0;
        public bool IsRented { get; set; }
        public DateTime EndOfRend { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }

        public BasicPaper(string paperName, string companyName, DateTime publichDate, Genre genre, double price, double discount)
        {
            EditItem(paperName, companyName, publichDate, genre, price, discount);
            count++;
            id = count;
        }
        public void EditItem(string paperName, string companyName, DateTime publichDate, Genre genre, double price, double discount)
        {
            if (paperName == null)
                throw new ArgumentNullException("Paper Name cannot be null");
            if (companyName == null)
                throw new ArgumentNullException("company Name cannot be null");
            if (genre == 0)
                throw new ArgumentNullException("genre  cannot be null");
            if (publichDate == null)
                throw new ArgumentNullException("Publish date cannot be null");

            CompanyName = companyName;
            if (publichDate > DateTime.Now)
                throw new IndexOutOfRangeException("Cannot exsit befor publishing");
            PublishedDate = publichDate;
            double EnumLenth = Enum.GetValues(typeof(Genre)).Length;
            if (((int)genre) < 0 || ((int)genre) > Math.Pow(2, EnumLenth) - 1)
                throw new ArgumentOutOfRangeException($"out of range Genre");
            Genre = genre;
            PaperName = paperName;
            if (price <= 0)
                throw new ArgumentException($"inserted price {price} is negtive/0. Price Cannot be negtive.");
            Price = price;
            Discount = discount;
        }
        public override bool Equals(object obj)
        {
            BasicPaper BP = obj as BasicPaper;
            return id == BP.id;
        }
        public override string ToString()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            return $"{PaperName},rent price for 14 days before discount:{Price:c} and after discount {(1 - Discount / 100) * Price:c}";
        }


    }
}
