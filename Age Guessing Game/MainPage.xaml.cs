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
using System.Threading.Tasks;

using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Age_Guessing_Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        PeopleManager peopleManager;
        List<DisplayBox> displayBoxes;
        Player player1;
        Computer computer;
        BitmapImage chekMark;
        BitmapImage xMark;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            peopleManager = new PeopleManager();
            await peopleManager.AddPeopleFromAssets();

            displayBoxes = new List<DisplayBox> { new DisplayBox(pictureFrame: image1, pictureHeader: image1Header), new DisplayBox(pictureFrame: image2, pictureHeader: image2Header) };

            player1 = new Player(playerProgressBar);
            computer = new Computer(computerProgressBar, "1e01fd74e3794b40bd6fdcee3ef81bda");

            chekMark = await getBitMapFromAssets("chekMark.png");
            xMark = await getBitMapFromAssets("xMark.png");

             startNewRound();
        }

        

        private async Task<BitmapImage> getBitMapFromAssets(string fileName)
        {
            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder assets = await appInstalledFolder.GetFolderAsync("Assets");

            StorageFile iconToDisplay = await assets.GetFileAsync(fileName);

            using (var stream = await iconToDisplay.OpenAsync(FileAccessMode.Read))
            {
                BitmapImage image = new BitmapImage();
                await image.SetSourceAsync(stream);

                return image;
            }
        }

        private async void person1Button_Click(object sender, RoutedEventArgs e)
        {
            person1Button.IsEnabled = false;
            person1Button.Visibility = Visibility.Collapsed;
            await personSelected(Guess.Person1);
            person1Button.IsEnabled = true;
            person1Button.Visibility = Visibility.Visible;
            startNewRound();
        }

        private async void person2Button_Click(object sender, RoutedEventArgs e)
        {
            person2Button.IsEnabled = false;
            person2Button.Visibility = Visibility.Collapsed;
            await personSelected(Guess.Person2);
            person2Button.IsEnabled = true;
            person2Button.Visibility = Visibility.Visible;
            startNewRound();
        }

        private async Task personSelected(Guess guess)
        {
            player1.CurrentGuess = guess;
           
            Person person1 = displayBoxes[0].CurentPerson;
            Person person2 = displayBoxes[1].CurentPerson;

            Guess correctGuess = Guess.None;

            if (person1.RealAge > person2.RealAge)
            {
                correctGuess = Guess.Person1;
            }
            else if (person2.RealAge > person1.RealAge)
            {
                correctGuess = Guess.Person2;
            }

            bool playerCorrect = player1.EvaluateGuess(correctGuess);

            if (playerCorrect)
            {
                image1Above.Source = chekMark;
                image2Above.Source = chekMark;
            }
            else
            {
                image1Above.Source = xMark;
                image2Above.Source = xMark;
            }

            int longDelay = 900;
            int shortDelay = 100;

            if (guess == Guess.Person1)
            {

                image1Above.Opacity = 100.0;
                //await Task.Delay(longDelay);
                //image1Above.Opacity = 0.0;
                //await Task.Delay(shortDelay);
            }
            if (guess == Guess.Person2)
            {
                image2Above.Opacity = 100.0;
                //await Task.Delay(longDelay);
                //image2Above.Opacity = 0.0;
                //await Task.Delay(shortDelay);
            }

            await computer.MakeGuess(person1, person2);
            bool computerCorrect = computer.EvaluateGuess(correctGuess);

            if (computerCorrect)
            {
                computer.Score++;
            }

            if (playerCorrect)
            {
                player1.Score++;
            }

            image1Above.Opacity = 0.0;
            image2Above.Opacity = 0.0;





            //StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            //StorageFolder assets = await appInstalledFolder.GetFolderAsync("Assets");


        }

        

        private void startNewRound()
        {
            if (player1.Score == playerProgressBar.Maximum || computer.Score == computerProgressBar.Maximum)
            {
                if (player1.Score == computer.Score)
                {
                    string text = "It's a Tie!" + Environment.NewLine + "Click to Play Again";
                    gameOverButton.Content = text;
                    gameOverButton.Visibility = Visibility.Visible;
                    gameOverButton.Opacity = 100;
                }
                else if (player1.Score > computer.Score)
                {
                    string text = "You Win!" + Environment.NewLine + "Click to Play Again";
                    gameOverButton.Content = text;
                    gameOverButton.Visibility = Visibility.Visible;
                    gameOverButton.Opacity = 100;
                }
                else if (computer.Score > player1.Score)
                {
                    string text = "You Loss!" + Environment.NewLine + "Click to Play Again";
                    gameOverButton.Content = text;
                    gameOverButton.Visibility = Visibility.Visible;
                    gameOverButton.Opacity = 100;
                }

                else
                {
                    gameOverButton.Content = "Error";
                }

                return;
            }


            bool samePersonOrAge = true;

            Person[] twoPeople = peopleManager.GetTwoPeople();

            while (samePersonOrAge)
            {
                samePersonOrAge = false;
                Person personOne = twoPeople[0];
                Person personTwo = twoPeople[1];

                if (personOne.Equals(personTwo) || personOne.SameAge(personTwo))
                {
                    samePersonOrAge = true;
                    System.Diagnostics.Debug.WriteLine("Try again");
                    twoPeople = peopleManager.GetTwoPeople(); 
                }
            }

            for (int i = 0; i < twoPeople.Count(); i++)
            {
                Person person = twoPeople[i];

                System.Diagnostics.Debug.WriteLine(person.Name);

                displayBoxes[i].CurentPerson = person;
            }
        }

        private void gameOverButton_Click(object sender, RoutedEventArgs e)
        {
            player1.Score = 0;
            computer.Score = 0;

            gameOverButton.Opacity = 0.0;
            gameOverButton.Content = "";
            gameOverButton.Visibility = Visibility.Collapsed;

            startNewRound();
        }
    }
}
