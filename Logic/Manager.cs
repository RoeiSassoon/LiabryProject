using Logic.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.AI.MachineLearning;

namespace Logic
{
    public class Manager
    {
        private List<BasicPaper> PaperList = new List<BasicPaper>()
        {
            new Book("The Prince","Machiavelli","Vivala Itali", DateTime.Now,Genre.History, 22),
            new journal("Hurry Poter","Bloomsbury",DateTime.Now, Genre.Action ,20),
           new journal("Hurry Poter2","Bloomsbury",DateTime.Now, Genre.Action ,20)

        };
        public List<BasicPaper> DisplayList()
        {
            return PaperList;
        }
        public void AddItem(BasicPaper paper)
        {
            PaperList.Add(paper);
        }
        public void DeletdItem(BasicPaper paper)
        {
            PaperList.Remove(paper);
            int prviousItemID = 0;
            foreach (var item in PaperList)
            {
                if (item.id - prviousItemID == 2)
                    item.id--;
                prviousItemID++;
            }
            BasicPaper.count--;

        }
        public void Edit(BasicPaper item, string paperName, Genre genre, double price        /// edit method
          , DateTime publichDate, string companyName, double discount = 0, string auther = "")
        {
            item.EditItem(paperName, companyName, publichDate, genre, price, discount);

            if (item is Book)
                ((Book)item).AuthorName = auther;

        }

        Dictionary<string, Func<BasicPaper, string, bool>> conditions = new Dictionary<string, Func<BasicPaper, string, bool>>()
        {
            { "Name", (basicItem, idToDiscount) =>  basicItem.PaperName == idToDiscount },
            { "Author", (basicItem, idToDiscount) => basicItem is Book && (basicItem as Book).AuthorName == idToDiscount },
            { "Company Name", (basicItem, idToDiscount) =>   basicItem.CompanyName == idToDiscount },

            { "Genre", (basicItem, idToDiscount) =>  {
                        Genre genre = (Genre)Enum.Parse(typeof(Genre), idToDiscount);
                        return basicItem.Genre == genre;}
            }
        };
        public List<BasicPaper> SearchBy(string SearchBy, string idTosearch)
        {
            if (idTosearch == null)
                return PaperList;
            List<BasicPaper> list = new List<BasicPaper>();
            foreach (BasicPaper item in PaperList)
            {
                if (conditions[SearchBy](item, idTosearch))
                    list.Add(item);

            }
            return list;
        }

        public void DiscountBy(string discountBy, string idToDiscount, int discount)
        {
            foreach (BasicPaper item in PaperList)
            {
                if (conditions[discountBy](item, idToDiscount) && item.Discount < discount)
                    item.Discount = discount;
            }
        }
        public void EndOfDiscountBy(string discountBy, string idToDiscount, int discount)
        {
            foreach (BasicPaper item in PaperList)
            {
                if (conditions[discountBy](item, idToDiscount) && item.Discount == discount)
                    item.Discount = 0;
            }
        }
        public void Rent(BasicPaper paper)
        {
            paper.IsRented = true;
            paper.EndOfRend = DateTime.Now.AddDays(14);
        }
        public void ReturnBook(BasicPaper paper)
        {
      
            paper.IsRented = false;
        }
        public bool IsLate(BasicPaper paper)
        {
            if (paper.IsRented && DateTime.Now > paper.EndOfRend)
                return true;
             return false;
            

        }
        public string DaliyUpdate()
        {
            int rentedCount = 0;
            int countBook = 0;
            int countJounral = 0;
            foreach (var item in PaperList)
            {
                if (item.IsRented)
                    rentedCount++;
                if (item is Book)
                    countBook++;
                else
                    countJounral++;
            }
            return $"There are {PaperList.Count} Papers.\n {countBook} books and {countJounral} jounrals.\n Rented papers {rentedCount}";
        }
        /*
                public void DiscountByCompanyName(string CompanyName, int discount)
                {
                    var item = PaperList.Where(s => s.CompanyName == CompanyName).ToList();
                    foreach (var i in item)
                    {
                        if (i.CompanyName == CompanyName && i.Discount < discount)
                            i.Discount = discount;
                    }
                }
                public void DiscountByPublishedDate(DateTime publishedDate, int discount)
                {
                    foreach (var item in PaperList)
                    {
                        if (item.PublishedDate == publishedDate && item.Discount < discount)
                            item.Discount = discount;
                    }
                }
                public void DiscountByAuthor(string Author, int discount)
                {
                    foreach (var item in PaperList)
                    {
                        if (item is Book)
                        {
                            Book b = (Book)item;
                            if (b.AuthorName == Author && item.Discount < discount)
                                item.Discount = discount;
                        }
                    }
                }
                public void DiscountByGenre(Genre genre, int discount)
                {
                    foreach (var item in PaperList)
                    {
                        if (item.Genre == genre && item.Discount < discount)
                            item.Discount = discount;
                    }
                }

                public void EndDiscountByCompanyName(string CompanyName, int discount)
                {
                    foreach (var item in PaperList)
                    {
                        if (item.CompanyName == CompanyName && item.Discount == discount)
                            item.Discount = 0;
                    }
                }
                public void EndDiscountByPublishedDate(DateTime publishedDate, int discount)
                {
                    foreach (var item in PaperList)
                    {
                        if (item.PublishedDate == publishedDate && item.Discount == discount)
                            item.Discount = 0;
                    }
                }
                public void EndDiscountByAuthor(string Author, int discount)
                {
                    foreach (var item in PaperList)
                    {
                        if (item is Book)
                        {
                            Book b = (Book)item;
                            if (b.AuthorName == Author && item.Discount == discount)
                                item.Discount = 0;
                        }
                    }
                }
                public void EndDiscountByGenre(Genre genre, int discount)
                {
                    foreach (var item in PaperList)
                    {
                        if (item.Genre == genre && item.Discount == discount)
                            item.Discount = 0;
                    }
                }

                public List<BasicPaper> SearchItembyName(string name)                /// search items by name method
                {
                    return PaperList.FindAll(s => s.PaperName == name);
                }
                public List<BasicPaper> SearchItembyAuther(string nameofauther)           /// search items by Author method
                {
                    List<BasicPaper> item = PaperList.Where(s => s is Book).ToList();

                    List<BasicPaper> list = new List<BasicPaper>();
                    foreach (var it in item)
                    {
                        Book i = (Book)it;
                        if (i.AuthorName == nameofauther)
                            list.Add(i);
                    }

                    return list;

                }
                public List<BasicPaper> SearchitembyGenre(Genre genre)               /// search items by genre method
                {
                    return PaperList.FindAll(s => s.Genre == genre);
                }
                public List<BasicPaper> SearchItembyComp(string CompanyName)          /// search items by comp name 
                {
                    return PaperList.FindAll(s => s.CompanyName == CompanyName);
                }
        */
    }

}
