using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;

namespace Wwear2
{
    class Camera
    {
        private System.Windows.Controls.Image ImageBox;

        public void startCamera(System.Windows.Controls.Image imagebox)
        {
            ImageBox = imagebox;
            CameraCaptureTask cameraTask = new CameraCaptureTask();
            cameraTask.Completed += new EventHandler<PhotoResult>(cameraComplete);
            cameraTask.Show();
        }

        public void cameraComplete(object sender, PhotoResult result){

            System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();

            if (result.TaskResult == TaskResult.OK)
            {
                bmp.SetSource(result.ChosenPhoto);
                ImageBox.Source = bmp;
            }
        }
    }
}
