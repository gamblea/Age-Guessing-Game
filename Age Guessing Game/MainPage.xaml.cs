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
using Windows.UI.Popups;

using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Age_Guessing_Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        List<Person> People;
        DisplayBox box1;

        public MainPage()
        {
            this.InitializeComponent();
            People = new List<Person>();
            string folder = @"ms-appx:///Assets/Pictures/";
            People.Add(new Person(new Uri(folder + "john.jpg"), "John", 22));
            People.Add(new Person(new Uri(folder + "bob.jpg"), "Bob", 19));

            box1 = new DisplayBox(image1, image1Header);
            box1.CurentPerson = People[0];

            InitializeFrames();

            //Person p = box1.CurentPerson;
           // p.CalculateAiGuess();
            //int age = box1.CurentPerson.AiGuessAge;
            
            
            //image1.Source = new BitmapImage(new Uri("ms-appx:///Assets/Pictures/bob.jpg")); // "Assets\bob.jpg";
            //image1.Source = new BitmapImage(People[0].PictureSource);
            image2.Source = new BitmapImage(People[1].PictureSource);
        }

        private async void person1Button_Click(object sender, RoutedEventArgs e)
        {
            People[0].CalculateAiGuess();
            
            var mes = new MessageDialog("button 1 " + People[0].PictureSource.AbsolutePath);
            await mes.ShowAsync();
        }

        private async void person2Button_Click(object sender, RoutedEventArgs e)
        {
            var mes = new MessageDialog("button 1 " + People[0].AiGuessAge);
            await mes.ShowAsync();
        }

        private async void InitializeFrames()
        {
            Person p = box1.CurentPerson;
            await p.CalculateAiGuess();
            int age = box1.CurentPerson.AiGuessAge;
            var mes = new MessageDialog(age.ToString());
            await mes.ShowAsync();
        }
    }
}
