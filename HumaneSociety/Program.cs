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
            // Animals animal = new Animals();
            // animal.AnimalId = 6;
            // Clients c = new Clients();
            // c.ClientId = 1;
            // Adoptions a = new Adoptions();
            // a.ClientId = 1;
            // a.AnimalId = 6;
            // Query.UpdateAdoption(true, a);
            Query.RemoveAdoption(2, 1);
            
            var db = new HumaneSocietyDb();
            
        }
    }
}