using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;

namespace Age_Guessing_Game
{
    class DisplayBox
    {
        private Image pictureFrame;
        private TextBlock pictureHeader;

        public DisplayBox(Image pictureFrame, TextBlock pictureHeader)
        {
            this.pictureFrame = pictureFrame;
            this.pictureHeader = pictureHeader;
        }

        private Person currentPerson;

        public Person CurentPerson
        {
            get { return currentPerson; }
            set
            {
                currentPerson = value;
                UpdateDisplayBox(currentPerson);
            }
        }
      
        private async void UpdateDisplayBox(Person person)
        {
            StorageFile  fileOfPicture = person.Picture;

            using (var stream = await fileOfPicture.OpenAsync(FileAccessMode.Read))
            {
                BitmapImage img = new BitmapImage();
                await img.SetSourceAsync(stream);

                pictureFrame.Source = img;
                pictureHeader.Text = person.Name;
            }
        }

    }
}
