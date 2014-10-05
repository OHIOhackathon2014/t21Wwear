using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;

namespace Wwear2
{
    public partial class NewClothing : PhoneApplicationPage
    {
        PhotoChooserTask photoChooserTask;
        List<String> ItemsListProperty1 { set; get; }
        List<String> ItemsListProperty2{ set; get; }
        List<String> ItemsListProperty3 { set; get; }
        List<String> ItemsListProperty4 { set; get; }


        public NewClothing()
        {
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

        private void Confirm_Click(object sender, EventArgs e)
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

            } else if((bool)torsoRadio.IsChecked){

                Clothes newCloth = new Clothes(name, "body", tagString, "body");
                Wardrobe.addClothes("body", newCloth);

            } else if((bool)legRadio.IsChecked){

                Clothes newCloth = new Clothes(name, "legs", tagString, "legs");
                Wardrobe.addClothes("legs", newCloth);

            } else if((bool)feetRadio.IsChecked){

                Clothes newCloth = new Clothes(name, "feet", tagString, "feet");
                Wardrobe.addClothes("feet", newCloth);

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

            if (p1 >= 4)
            {
                newCode = newCode + 'x';
            } else { newCode = newCode +p1.ToString(); }

            if (p2 >= 3)
            {
                newCode = newCode + 'x';
            } else { newCode = newCode +p2.ToString(); }

            if (p3 >= 3)
            {
                newCode = newCode + 'x';
            } else { newCode = newCode +p3.ToString(); }

            if (p4 >= 3)
            {
                newCode = newCode + 'x';
            } else { newCode = newCode +p4.ToString(); }
            

            return newCode;
        }

    }
}