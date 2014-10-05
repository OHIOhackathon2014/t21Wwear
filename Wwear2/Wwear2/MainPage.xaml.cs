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
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;

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

            Clothes addMe = new Clothes("Wool Hat","hat","xxx2","head");
            Wardrobe.addClothes("head", addMe);
            Clothes cloth = Wardrobe.pickHat("3111");
            addMe = new Clothes("Unicorn Hat", "hat", "2xx2", "head");
            Wardrobe.addClothes("head", addMe);
            cloth = Wardrobe.pickHat("2002");
            Debug.WriteLine(cloth.ClothesName);
            Wardrobe.WardrobeSet();

            //FROM OTHER PAGE

            InitializeComponent();
            ItemsListProperty1 = new List<string>();
            ItemsListProperty2 = new List<string>();
            ItemsListProperty3 = new List<string>();
            ItemsListProperty4 = new List<string>();

            //ListboxTest.DataContext = ItemsListProperty;
            ItemsListProperty1.Add("Hot");
            ItemsListProperty1.Add("Normal");
            ItemsListProperty1.Add("Cold");
            ItemsListProperty1.Add("Freezing");
            ItemsListProperty1.Add("I don't care");
            ItemsListProperty2.Add("No precip.");
            ItemsListProperty2.Add("Rain");
            ItemsListProperty2.Add("Snow");
            ItemsListProperty2.Add("I don't care");
            ItemsListProperty3.Add("No wind");
            ItemsListProperty3.Add("breeze");
            ItemsListProperty3.Add("windy");
            ItemsListProperty3.Add("I don't care");
            ItemsListProperty4.Add("No humidity");
            ItemsListProperty4.Add("Mild humidity");
            ItemsListProperty4.Add("High humidity");
            ItemsListProperty4.Add("I don't care");
            param1.ItemsSource = ItemsListProperty1;
            param2.ItemsSource = ItemsListProperty2;
            param3.ItemsSource = ItemsListProperty3;
            param4.ItemsSource = ItemsListProperty4;


            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
            photoChooserTask.PixelHeight = 400;
            photoChooserTask.PixelWidth = 400;
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

        //FROM OTHER PAGE

        PhotoChooserTask photoChooserTask;
        List<String> ItemsListProperty1 { set; get; }
        List<String> ItemsListProperty2{ set; get; }
        List<String> ItemsListProperty3 { set; get; }
        List<String> ItemsListProperty4 { set; get; }



        private void Add_Item(object sender, RoutedEventArgs e)
        {
            photoChooserTask.Show();
        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                imageFrame.Source = bmp;
            }
        }

        private void shell_Clicked(object sender, EventArgs e)
        {
            //add to database and clear all parameters
            string name, tagString;
            int firstPref, secondPref, thirdPref, fourthPref;
            firstPref = param1.SelectedIndex;
            secondPref = param2.SelectedIndex;
            thirdPref = param3.SelectedIndex;
            fourthPref = param4.SelectedIndex;

            name = newItemName.Text;
            tagString = genWeatherCode(firstPref,secondPref,thirdPref,fourthPref);

            //Generate the new clothes entry
            if((bool)headRadio.IsChecked){

                Clothes newCloth = new Clothes(name,"head",tagString,"head");
                Wardrobe.addClothes("head", newCloth);
                Debug.WriteLine("Added new head: " + newCloth.ClothesName);

            } else if((bool)torsoRadio.IsChecked){

                Clothes newCloth = new Clothes(name, "body", tagString, "body");
                Wardrobe.addClothes("body", newCloth);
                Debug.WriteLine("Added mew body " + newCloth.ClothesName);

            } else if((bool)legRadio.IsChecked){

                Clothes newCloth = new Clothes(name, "legs", tagString, "legs");
                Wardrobe.addClothes("legs", newCloth);
                Debug.WriteLine("Added mew legs " + newCloth.ClothesName);

            } else if((bool)feetRadio.IsChecked){

                Clothes newCloth = new Clothes(name, "feet", tagString, "feet");
                Wardrobe.addClothes("feet", newCloth);
                Debug.WriteLine("Added mew feet " + newCloth.ClothesName);

            }

            //Clear the form data
            newItemName.Text = "";
            imageFrame.Source = null;
            param1.SelectedIndex = -1;
            param2.SelectedIndex = -1;
            param3.SelectedIndex = -1;
            param4.SelectedIndex = -1;
            headRadio.IsChecked = true;
        }

        private string genWeatherCode(int p1, int p2, int p3, int p4)
        {
            string newCode = "";

            if (p1 > 4)
            {
                newCode = newCode + 'x';
            } else { newCode = newCode +p1.ToString(); }

            if (p2 > 3)
            {
                newCode = newCode + 'x';
            } else { newCode = newCode +p2.ToString(); }

            if (p3 > 3)
            {
                newCode = newCode + 'x';
            } else { newCode = newCode +p3.ToString(); }

            if (p4 > 3)
            {
                newCode = newCode + 'x';
            } else { newCode = newCode +p4.ToString(); }
            

            return newCode;
        }        
    }
}

