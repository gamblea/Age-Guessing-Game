using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

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
            get
            {
                return currentPerson;
            }
            set
            {
                currentPerson = value;
                UpdatePerson();
            }
        }
        
        private void UpdatePerson()
        {

            pictureFrame.Source = new BitmapImage(currentPerson.PictureSource); //currentPerson.PictureSource
            pictureHeader.Text = currentPerson.Name;
        }

    }
}
