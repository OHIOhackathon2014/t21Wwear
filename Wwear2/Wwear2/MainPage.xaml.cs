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

        string weatherCode;

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
            updateWeather();

            Clothes addMe = new Clothes("Wool Hat","hat","3xxx","head");
            Wardrobe.addClothes("head", addMe);
            Clothes cloth = Wardrobe.pickHat("3111");
            addMe = new Clothes("Unicorn Hat", "hat", "3xxx", "head");
            Wardrobe.addClothes("head", addMe);
            cloth = Wardrobe.pickHat("3211");
            Debug.WriteLine(cloth.ClothesName);
            Wardrobe.WardrobeSet();
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
            int windSpeed, humidity, precipitation;
            int.TryParse(fourcast.windspeedMph, out windSpeed);
            int.TryParse(fourcast.humidity, out humidity);
            int.TryParse(fourcast.precip, out precipitation);
            SuggestionEngine setWeather = new SuggestionEngine((int)fourcast.tempMinF, windSpeed, humidity, precipitation);
            weatherCode = setWeather.setValue();
            Debug.WriteLine("Weather Code = " + weatherCode);
            
        }

        private void Add_Item(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewClothing.xaml", UriKind.Relative));
        }

        private void suggestClothes(object sender, RoutedEventArgs e)
        {
            HeadSuggestion.Text = Wardrobe.pickHat(weatherCode).ClothesName;
            BodySuggestion.Text = Wardrobe.pickTop(weatherCode).ClothesName;
            LegSuggestion.Text = Wardrobe.pickBottoms(weatherCode).ClothesName;
            FeetSuggestion.Text = Wardrobe.pickFeet(weatherCode).ClothesName;
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

