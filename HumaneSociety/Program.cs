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
            var Shots = Query.GetPendingAdoptions();
            foreach (var item in Shots)
            {
                Console.WriteLine(item.AnimalId);
            }
            var db = new HumaneSocietyDb();
            
        }
    }
}