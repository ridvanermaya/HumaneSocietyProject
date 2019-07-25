using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HumaneSociety.Entity;

namespace HumaneSociety
{
    class Program
    {
        static void Main(string[] args)
        {
            Animals animal = new Animals();
            animal.AnimalId = 1;
            Query.RemoveAnimal(animal);
            var db = new HumaneSocietyDb();
            
        }
    }
}