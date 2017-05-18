using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.IO;
using Windows.UI.Popups;

namespace Age_Guessing_Game
{

    class Person
    {
        public Person(Uri pictureSource, string name, int realAge)
        {
            this.PictureSource = pictureSource;
            this.Name = name;
        }

        public string Name { get; set; }

        public Uri PictureSource { get; set; }

        public int RealAge { get; set; }

        public int AiGuessAge { get; set; }

        public async Task CalculateAiGuess()
        {
            int age = 0;

            VisionServiceClient client = new VisionServiceClient("bcdbb1dc3e304fe580a20f1bf6afe68d");

            AnalysisResult analysisResult;
            var features = new VisualFeature[] {VisualFeature.Faces};

            var file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(PictureSource);

            var fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

            analysisResult = await client.AnalyzeImageAsync(fileStream.AsStream(), features);

            foreach (Face face in analysisResult.Faces)
            {
                age = face.Age;
            }
            var message = new MessageDialog("The age has been set for " + Name + "The age is " + age);
            await message.ShowAsync();

            AiGuessAge = age;
        }


    }
}
