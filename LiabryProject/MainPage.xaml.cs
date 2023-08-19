using Logic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LiabryProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Manager manager = new Manager();

        public MainPage()
        {
            this.InitializeComponent();
            ImComboBox.Items.Add("Customer");
            ImComboBox.Items.Add("Librarian");
          
          

        }
        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            bool IsLibrarian;
            Tuple<Manager, bool> Info;
            if (UserNameTextBox.Text == "AAAA" && PasswordTextBox.Text == "1234" && ImComboBox.SelectedValue.ToString() == "Customer")
            {
                 IsLibrarian = false;
            }
            else if (UserNameTextBox.Text == "BBBB" && PasswordTextBox.Text == "4321" && ImComboBox.SelectedValue.ToString() == "Librarian")
            {
                IsLibrarian = true;
            }

            else
            { ErrorBlock.Text = $"Invalid Password or UserName or your not a {ImComboBox.SelectedValue}. Please Try again"; 
                return; 
            }
            Info = new Tuple<Manager, bool>(manager, IsLibrarian);
            Frame.Navigate(typeof(CustomerMain), Info);

        }



    }
}
