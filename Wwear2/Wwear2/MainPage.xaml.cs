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
using System.Device.Location;
using Windows.Devices.Geolocation;
using System.Diagnostics;

namespace Wwear2
{
    public partial class MainPage : PhoneApplicationPage
    {

        Forecast fourcast;
        string hat = "Baseball Cap";
        string body = "T-Shirt";
        string legs = "Sweat Pants";
        string shoes = "Boots";

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

            getLocation();
        }

        private async void getLocation()
        {
            Geolocator locator = new Geolocator();
            string lat, lon;

            if (!locator.LocationStatus.Equals(PositionStatus.Disabled))
            {
               
                Geoposition position = await locator.GetGeopositionAsync();
                
                lat = position.Coordinate.Point.Position.Latitude.ToString();
                lon = position.Coordinate.Point.Position.Longitude.ToString();
                fourcast.GetForecast(lat,lon);
            }
            else
            {
                MessageBox.Show("GeoLocation services are turned off.", AppResources.ApplicationTitle, MessageBoxButton.OK);
                return;
            }
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

            getLocation();
            updateWeather();
        }

        private void Add_Item(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewClothing.xaml", UriKind.Relative));
        }

        private void suggestClothes(object sender, RoutedEventArgs e)
        {
            //Suggest Hat
            if (fourcast.tempMinF > 30)
            {
                HeadSuggestion.Text = hat;
            }
            else
            {
                HeadSuggestion.Text = "No Hat Bro";
            }
            //Suggest body
            if (fourcast.tempMinF < 50)
            {
                BodySuggestion.Text = body;
            }
            else
            {
                BodySuggestion.Text = "No shirts Bro";
            }
            //Suggest pants
            if (fourcast.tempMinF < 30)
            {
                LegSuggestion.Text = legs;
            }
            else
            {
                LegSuggestion.Text = "No Pants Bro";
            }

            //Suggest feet
            if (fourcast.tempMinF < 30)
            {
                FeetSuggestion.Text = shoes;
            }
            else
            {
                FeetSuggestion.Text = "No boots pour vous";
            }
        }

        private void updateWeather(){
            string precipitation = "No precipitation";

            if (fourcast.precip != null)
            {
                precipitation = fourcast.precip + "%";
            }

            WeatherBox.Text = "Low: " + fourcast.tempMinF.ToString() + "\n" +
                "High: " + fourcast.tempMaxF.ToString() + "\n" +
                "Wind Speed (mph): " + fourcast.windspeedMph + "\n" +
                "Precipitation Rate: " + precipitation + " \n" +
                "Humidity Rate: " + fourcast.humidity + "%";
        }
    }
}

