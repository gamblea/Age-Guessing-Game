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
        public Person(StorageFile picture, string name, double realAge)
        {
            this.Picture = picture;
            this.Name = name;
            this.RealAge = realAge;
        }

        public string Name { get; set; }

        public StorageFile Picture { get; set; }

        public double RealAge { get; set; }

        public bool SameAge(Person person)
        {
            if (RealAge == person.RealAge)
            {
                return true;
            }

            return false;
        }

        // override object.Equals
        public bool Equals(Person person)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            System.Diagnostics.Debug.WriteLine("Equals: " + Name);
            System.Diagnostics.Debug.WriteLine("Equals: " + person.Name);


            if (person == null || GetType() != person.GetType())
            {
                return false;
            }

            if (Name == person.Name && RealAge == person.RealAge)
            {
                return true;
            }

            // TODO: write your implementation of Equals() here
            return false;
        }

    }
}
