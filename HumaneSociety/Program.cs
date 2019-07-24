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
            Query.UpdateAnimal(2, new Dictionary<int, string>(){ [1] = "5", [2] = "AAA", [3] = "6"});
            PointOfEntry.Run();
            var db = new HumaneSocietyDb();
            
        }
    }
}