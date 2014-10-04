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
        List<String> ItemsListProperty { set; get; }
        PhotoChooserTask photoChooserTask;

        public NewClothing()
        {
            InitializeComponent();
            ItemsListProperty = new List<string>();
            //ListboxTest.DataContext = ItemsListProperty;
            ItemsListProperty.Add("hi");
            ItemsListProperty.Add("Hello Again");
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
                MessageBox.Show(e.ChosenPhoto.Length.ToString());
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                imageFrame.Source = bmp;
            }
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            //add to database and clear all parameters
            
        }

    }
}