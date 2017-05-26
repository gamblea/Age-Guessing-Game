using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.IO;
using Windows.UI.Popups;
using Microsoft.ProjectOxford.Face.Contract;
using Microsoft.ProjectOxford.Face;
using Windows.Storage;

namespace Age_Guessing_Game
{

    class Person
    { 
        public Person(StorageFile picture, string name, int realAge)
        {
            this.Picture = picture;
            this.Name = name;
            this.RealAge = realAge;
        }

        public string Name { get; set; }

        public StorageFile Picture { get; set; }

        public int RealAge { get; set; }
    }
}
