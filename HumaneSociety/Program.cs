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
            // Clients c = new Clients();
            // c.ClientId = 1;
            // Adoptions a = new Adoptions();
            // a.ClientId = 1;
            // a.AnimalId = 6;
            // Query.UpdateAdoption(true, a);
            // Query.UpdateShot("DA2P", animal);
            // var query = Query.SearchForAnimalsByMultipleTraits(new Dictionary<int, string>(){[8] = "2"});
            // foreach(var q in query){
            //     Console.WriteLine(q.Name);
            // }
            // var db = new HumaneSocietyDb();
        }
    }
}