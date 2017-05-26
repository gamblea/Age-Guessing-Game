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
using Windows.Storage;

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

        }

        private void person1Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void person2Button_Click(object sender, RoutedEventArgs e)
        {
            PeopleManager man = new PeopleManager();
            man.AddPeopleFromAssets();
            foreach (Person person in man.People)
            {
                var m = new MessageDialog(person.RealAge.ToString());
                await m.ShowAsync();
            }
        }

        
    }
}
