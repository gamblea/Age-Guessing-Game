using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Microsoft.ProjectOxford.Face.Contract;
using Microsoft.ProjectOxford.Face;
using System.IO;

namespace Age_Guessing_Game
{
    class Computer : Player
    {
        private FaceServiceClient client;

        public Computer(ProgressBar progressBar, string key) : base(progressBar)
        {
            this.client = new FaceServiceClient(key);
        }

        public async void MakeGuess(Person person1, Person person2)
        {
            double person1AgeGuess = await GetAge(person1);
            double person2AgeGuess = await GetAge(person2);

            if (person1AgeGuess > person2AgeGuess)
            {
                CurrentGuess = Guess.Person1;
            }
            else
            {
                CurrentGuess = Guess.Person2;
            }
        }

        private async Task<double> GetAge(Person person)
        {
            StorageFile file = person.Picture;
            using (var fileStream = await file.OpenAsync(FileAccessMode.Read))
            {
                var requiredFaceAttributes = new FaceAttributeType[]
                    {
                    FaceAttributeType.Age
                    };

                var faces = await client.DetectAsync(fileStream.AsStream(), returnFaceAttributes: requiredFaceAttributes);

                Face face = faces.ElementAt(0);

                if (face == null || face.FaceAttributes.Age == 0.0)
                {
                    return -1;
                }

                return face.FaceAttributes.Age;
            }
        }


    }
}
