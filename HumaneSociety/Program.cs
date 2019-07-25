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
            var db = new HumaneSocietyDb();
            // Query.AddAnimalsFromCsv("../animals.csv");
            
            
            // var csv = Query.GetCsvData("../animals.csv");
            // foreach (var item in csv)
            // {
            //     string noQuotes= Query.RemoveAllChars(item.Name, "\"");
            //     Console.WriteLine(Query.RemoveAllChars(noQuotes, " "));
            // }
            // PointOfEntry.Run();
        }
    }
}