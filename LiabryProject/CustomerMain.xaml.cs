using Logic;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels; 
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LiabryProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerMain : Page
    {
        Manager m1;
        bool IsLibrarian;

        public CustomerMain()
        {

            this.InitializeComponent();
            SearchByBox.Items.Add("View all");
            SearchByBox.Items.Add("Name");
            SearchByBox.Items.Add("Auther");
            SearchByBox.Items.Add("Company Name");
            SearchByBox.Items.Add("Genre");




            GenreBox.Items.Add(Genre.Action);
            GenreBox.Items.Add(Genre.Commedy);
            GenreBox.Items.Add(Genre.Documentary);
            GenreBox.Items.Add(Genre.History);
            GenreBox.Items.Add(Genre.Horror);
            GenreBox.IsEnabled = false;
        }
        public void IsCustomer()
        {
            var x = Visibility.Collapsed;
            DeleteButton.Visibility = x;
            AddItemButton.Visibility = x;
            AddSaleButton.Visibility = x;
            StopSaleButton.Visibility = x;
            EditButton.Visibility = x;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Tuple<Manager, bool> info = e.Parameter as Tuple<Manager, bool>;
            m1 = info.Item1;
            IsLibrarian = info.Item2;
            ListView.ItemsSource = m1.DisplayList();
            if (!IsLibrarian)
                IsCustomer();
        }




        private void SearchByBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView != null && m1 != null)
            {
                GenreBox.IsEnabled = false;
                switch (SearchByBox.SelectedIndex)
                {
                    case 0:
                        ListView.ItemsSource = m1.DisplayList();
                        break;
                    case 1:
                        ListView.ItemsSource = m1.SearchBy("Name", SearchByTextBox.Text);
                        break;
                    case 2:
                        ListView.ItemsSource = m1.SearchBy("Author", SearchByTextBox.Text);
                        break;
                    case 3:
                        ListView.ItemsSource = m1.SearchBy("Company Name", SearchByTextBox.Text);
                        break;
                    case 4:
                        GenreBox.IsEnabled = true;
                        break;
                }
            }
        }

        private void GenreBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView != null && m1 != null)
                ListView.ItemsSource = m1.SearchBy("Genre", (GenreBox.SelectedValue.ToString()));

        }

        private void RentButton_Click(object sender, RoutedEventArgs e)
        {
            BasicPaper paper = (BasicPaper)ListView.SelectedItem;
            MessageDialog x;
            if (paper != null)
            {
                if (paper.IsRented)
                    x = new MessageDialog("faild,item is Already Reanted");

                else
                {
                    m1.Rent(paper);
                    x = new MessageDialog("item Rented sucssfuly");
                }
                x.ShowAsync();
            }
        }

        private void RetrunButton_Click(object sender, RoutedEventArgs e)
        {
            BasicPaper paper = (BasicPaper)ListView.SelectedItem;
            MessageDialog x;
            if (paper != null)
            {
                if (paper.IsRented)
                {
                    if (m1.IsLate(paper))
                        x = new MessageDialog("item Retruned LATE!!! sucssfuly  ");
                    else
                        x = new MessageDialog("item Retruned sucssfuly ");

                    m1.ReturnBook(paper);

                }
                else
                    x = new MessageDialog("falid,item is not rented");
                x.ShowAsync();

            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsLibrarian)
            {
                m1.DeletdItem((BasicPaper)ListView.SelectedItem);
                ListView.ItemsSource = null;
                ListView.ItemsSource = m1.DisplayList();
            }
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddSaleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StopSaleButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
