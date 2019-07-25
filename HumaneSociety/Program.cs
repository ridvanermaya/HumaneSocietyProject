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
            animal.AnimalId = 6;
            Clients c = new Clients();
            c.ClientId = 1;
            Query.Adopt(animal, c);
            
            var db = new HumaneSocietyDb();
            
        }
    }
}