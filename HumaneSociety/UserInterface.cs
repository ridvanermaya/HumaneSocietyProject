using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HumaneSociety.Entity;

namespace HumaneSociety
{
    public static class UserInterface
    {
        public static void DisplayUserOptions(List<string> options)
        {
            foreach(string option in options)
            {
                Console.WriteLine(option);
            }
        }
        public static void DisplayUserOptions(string options)
        {
            Console.WriteLine(options);
        }
        public static string GetUserInput()
        {
            string input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "reset":
                    PointOfEntry.Run();
                    Environment.Exit(1);
                    break;
                case "exit":
                    Environment.Exit(1);
                    break;
                default:
                    break;
            }

            return input;
        }
        public static string GetStringData(string parameter, string target)
        {
            string data;
            DisplayUserOptions($"What is {target} {parameter}?");
            data = GetUserInput();
            return data;
        }

        internal static bool? GetBitData(List<string> options)
        {
            DisplayUserOptions(options);
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool? GetBitData()
        {
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static bool? GetBitData(string target, string parameter)
        {
            DisplayUserOptions($"Is {target} {parameter}?");
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static void DisplayAnimals(List<Animals> animals)
        {
            foreach(Animals animal in animals)
            {
                Console.WriteLine(animal.AnimalId + " " + animal.Name + " " + animal.Category.Name);
            }
        }

        internal static int GetIntegerData()
        {
            try
            {
                int data = int.Parse(GetUserInput());
                return data;
            }
            catch
            {
                Console.Clear();
                DisplayUserOptions("Incorrect input please enter an integer number.");
                return GetIntegerData();
            }
        }

        public static int GetIntegerData(string parameter, string target)
        {
            try
            {
                int data = int.Parse(GetStringData(parameter, target));
                return data;
            }
            catch
            {
                Console.Clear();
                DisplayUserOptions("Incorrect input please enter an integer number.");
                return GetIntegerData(parameter, target);
            }
        }

        internal static void DisplayClientInfo(Clients client)
        {
            List<string> info = new List<string>() { client.FirstName, client.LastName, client.Email, client.Address.Usstate.Name };
            DisplayUserOptions(info);
            Console.ReadLine();
        }

        internal static void DisplayEmployeeInfo(Employees employee)
        {
            Console.WriteLine($"First Name: {employee.FirstName}" +
            $"\nLast Name: {employee.LastName}" + 
            $"\nUsername: {employee.UserName}" +
            $"\nEmail: {employee.Email}" +
            $"\nEmployee Number: {employee.EmployeeNumber}");
            Console.ReadLine();
        }


        public static void DisplayAnimalInfo(Animals animal)
        {
            Rooms animalRoom = Query.GetRoom(animal.AnimalId);
            List<string> info = new List<string>() {"ID: " + animal.AnimalId, animal.Name, animal.Age + "years old", "Demeanour: " + animal.Demeanor, "Kid friendly: " + BoolToYesNo(animal.KidFriendly), "pet friendly: " + BoolToYesNo(animal.PetFriendly), $"Location: " + animalRoom.RoomId, "Weight: " + animal.Weight.ToString(),  "Food amoumnt in cups:" + animal.DietPlan.FoodAmountInCups};
            DisplayUserOptions(info);
            Console.ReadLine();
        }

        private static string BoolToYesNo(bool? input)
        {
            if (input == true)
            {
                return "yes";
            }
            else
            {
                return "no";
            }
        }

        public static bool GetBitData(string option)
        {
            DisplayUserOptions(option);
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int promptForOptions(string question, List<string> options)
        {
            int choice = 0;

            string s = "";

            Console.WriteLine(question);
            s = Console.ReadLine();

            Func<int> find = () =>
            {
                for (int i = 0; i < options.Count; i++)
                {
                    if (options[i].Equals(s, StringComparison.OrdinalIgnoreCase)) { return i; }
                }

                return -1;
            };

            choice = find();

            while (choice == -1)
            {
                Console.WriteLine($"I don't know what a {s} is try something else");
                Console.WriteLine(question);
                s = Console.ReadLine();
                choice = find();
            }

            return choice;
        }

        public static Dictionary<int, string> GetAnimalSearchCriteria()
        {
            Dictionary<int, string> searchParameters = new Dictionary<int, string>();
            bool isSearching = true;
            while (isSearching)
            {
                Console.Clear();
                List<string> options = new List<string>() { "Select Search Criteia: (Enter number and choose finished when finished)", "1. Category", "2. Name", "3. Age", "4. Demeanor", "5. Kid friendly", "6. Pet friendly", "7. Weight", "8. ID", "9. Finished" };
                DisplayUserOptions(options);
                string input = GetUserInput();
                if (input.ToLower() == "9" || input.ToLower() == "finished")
                {
                    isSearching = false;
                    continue;
                }
                else
                {
                    searchParameters = EnterSearchCriteria(searchParameters, input);
                }
            }
            return searchParameters;
        }
        public static Dictionary<int, string> EnterSearchCriteria(Dictionary<int, string> searchParameters, string input)
        {
            Console.Clear();
            for (int i = 0; i < 9; i++)
            {
                searchParameters.Add(i, "");
            }

            switch (input)
            {
                case "1":
                    searchParameters[1] = GetStringData("category", "the animal's");
                    break;
                case "2":
                    searchParameters[2] = GetStringData("name", "the animal's");
                    break;
                case "3":
                    searchParameters[3] = GetIntegerData("age", "the animal's").ToString();
                    break;
                case "4":
                    searchParameters[4] = GetStringData("demeanor", "the animal's");
                    break;
                case "5":
                    searchParameters[5] = GetBitData("the animal", "kid friendly").ToString();
                    break;
                case "6":
                    searchParameters[6] = GetBitData("the animal", "pet friendly").ToString();
                    break;
                case "7":
                    searchParameters[7] = GetIntegerData("weight", "the animal's").ToString();
                    break;
                case "8":
                    searchParameters[8] = GetIntegerData("ID", "the animal's").ToString();
                    break;
                default:
                    DisplayUserOptions("Input not recognized please try agian");
                    break;
            }
            return searchParameters;
        }
    }
}
