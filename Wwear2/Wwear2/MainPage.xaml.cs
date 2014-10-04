using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Wwear2.Resources;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Wwear2
{
    public partial class MainPage : PhoneApplicationPage
    {

        Forecast fourcast;
            
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            fourcast = new Forecast();
            //Check for network connection
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("Network Unavailable");
                return;
            }
            else
            {
                
                fourcast.GetForecast();
            }
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            

        }


        //// Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = "refresh";
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    //ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    //ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WeatherBox.Text = "Low: " + fourcast.tempMinC + "\n" + 
                "High: " + fourcast.tempMaxC + "\n" + 
                "Wind Speed (mph): " + fourcast.windspeedMph;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            


        }

        private void Add_Item(object sender, EventArgs e)
        {
            Camera camera = new Camera();
            camera.startCamera(cameraImage);
        }
    }
}