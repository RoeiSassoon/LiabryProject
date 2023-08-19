using Logic;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class AddEditItem : Page
    {
        Manager m1;


        bool isEditing = false;
        public AddEditItem()
        {
            this.InitializeComponent();
            GenreBox.Items.Add("Name");
            GenreBox.Items.Add("Auther");
            GenreBox.Items.Add("Company Name");
            GenreBox.Items.Add("Genre");
        }

        private void EditItemButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == null || CompanyNameTextBox.Text == null)// To contine
            { }// to throw error

            if(AuthorTextBox.Visibility == Visibility.Collapsed)
            {

                m1.AddItem(new journal(NameTextBox.Text, CompanyNameTextBox.Text, DatePublishPicker.Date.Date, (Genre)Enum.Parse(typeof(Genre),(GenreBox.SelectedValue).ToString()), double.Parse(PriceTextBox.Text)));
            }
        }

        private void JounralButton_Checked(object sender, RoutedEventArgs e)
        {
            BookButton.IsChecked = false;
            
            AuthorTextBox.Visibility = Visibility.Collapsed;
            authorBlock.Visibility = Visibility.Collapsed;
        }

        private void BookButton_Checked(object sender, RoutedEventArgs e)
        {
            JounralButton.IsChecked = false ;

            AuthorTextBox.Visibility = Visibility.Visible;
            authorBlock.Visibility = Visibility.Visible;
        }
    }
}
