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
        private Random random;

        public List<Person> People
        {
            get { return people; } 
        }


        public PeopleManager()
        {
            people = new List<Person>();
            random = new Random();
        }   

        public async Task AddPeopleFromAssets()
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

                    //int index = fullFileName.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
                    int index = fullFileName.IndexOfAny(new char[] { '-', '_' });

                    string name = fullFileName.Substring(0, index);
                    double age = Convert.ToDouble(fullFileName.Substring(index + 1));

                    people.Add(new Person(file, name, age));
                }
            }
        }

        public Person[] GetTwoPeople()
        {
            return people.OrderBy(x => random.Next()).Take(2).ToArray();
        }
    }
}
