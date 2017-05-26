using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Age_Guessing_Game
{
    class PeopleManager
    {
        private List<Person> people;

        public List<Person> People
        {
            get { return people; } 
        }


        public PeopleManager()
        {
            people = new List<Person>();
        }   

        public async void AddPeopleFromAssets()
        {
            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder assets = await appInstalledFolder.GetFolderAsync("Assets");
            StorageFolder pictures = await assets.GetFolderAsync("Pictures");
            var files = await pictures.GetFilesAsync();

            foreach (var file in files)
            {
                if (file != null)
                {
                    string fullFileName = file.DisplayName;

                    int index = fullFileName.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });

                    string name = fullFileName.Substring(0, index);
                    int age = Int32.Parse(fullFileName.Substring(index));

                    people.Add(new Person(file, name, age));
                }
            }
        }
    }
}
