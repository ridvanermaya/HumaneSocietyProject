using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HumaneSociety.Entity;

namespace HumaneSociety
{
    public static class Query
    {        
        static HumaneSocietyDb db;

        static Query()
        {
            db = new HumaneSocietyDb();
        }

        internal static List<USStates> GetStates()
        {
            List<USStates> allStates = db.Usstates.ToList();
            return allStates;
        }
            
        internal static Clients GetClient(string userName, string password)
        {
            Clients client = db.Clients.Where(c => c.UserName == userName && c.Password == password).Single();
            return client;
        }

        internal static List<Clients> GetClients()
        {
            List<Clients> allClients = db.Clients.ToList();
            return allClients;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int stateId)
        {
            Clients newClient = new Clients();

            newClient.FirstName = firstName;
            newClient.LastName = lastName;
            newClient.UserName = username;
            newClient.Password = password;
            newClient.Email = email;

            Addresses addressFromDb = db.Addresses.Where(a => a.AddressLine1 == streetAddress && a.Zipcode == zipCode && a.UsstateId == stateId).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if (addressFromDb == null)
            {
                Addresses newAddress = new Addresses();
                newAddress.AddressLine1 = streetAddress;
                newAddress.City = null;
                newAddress.UsstateId = stateId;
                newAddress.Zipcode = zipCode;                

                db.Addresses.Add(newAddress);
                db.SaveChanges();

                addressFromDb = newAddress;
            }
            // attach AddressId to clientFromDb.AddressId
            newClient.AddressId = addressFromDb.AddressId;

            db.Clients.Add(newClient);
            db.SaveChanges();
        }

        internal static void UpdateClient(Clients clientWithUpdates)
        {
            // find corresponding Client from Db
            Clients clientFromDb = null;

            try
            {
                clientFromDb = db.Clients.Where(c => c.ClientId == clientWithUpdates.ClientId).Single();
            }
            catch(InvalidOperationException e)
            {
                Console.WriteLine("No clients have a ClientId that matches the Client passed in.");
                Console.WriteLine("No update have been made.");
                return;
            }
            
            // update clientFromDb information with the values on clientWithUpdates (aside from address)
            clientFromDb.FirstName = clientWithUpdates.FirstName;
            clientFromDb.LastName = clientWithUpdates.LastName;
            clientFromDb.UserName = clientWithUpdates.UserName;
            clientFromDb.Password = clientWithUpdates.Password;
            clientFromDb.Email = clientWithUpdates.Email;

            // get address object from clientWithUpdates
            Addresses clientAddress = clientWithUpdates.Address;

            // look for existing Address in Db (null will be returned if the address isn't already in the Db
            Addresses updatedAddress = db.Addresses.Where(a => a.AddressLine1 == clientAddress.AddressLine1 && a.UsstateId == clientAddress.UsstateId && a.Zipcode == clientAddress.Zipcode).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if(updatedAddress == null)
            {
                Addresses newAddress = new Addresses();
                newAddress.AddressLine1 = clientAddress.AddressLine1;
                newAddress.City = null;
                newAddress.UsstateId = clientAddress.UsstateId;
                newAddress.Zipcode = clientAddress.Zipcode;                

                db.Addresses.Add(newAddress);
                db.SaveChanges();

                updatedAddress = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            clientFromDb.AddressId = updatedAddress.AddressId;
            
            // submit changes
            db.SaveChanges();
        }
        
        internal static void AddUsernameAndPassword(Employees employee)
        {
            Employees employeeFromDb = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();

            employeeFromDb.UserName = employee.UserName;
            employeeFromDb.Password = employee.Password;

            db.SaveChanges();
        }

        internal static Employees RetrieveEmployeeUser(string email, int employeeNumber)
        {
            Employees employeeFromDb = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).FirstOrDefault();

            if (employeeFromDb == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return employeeFromDb;
            }
        }

        internal static Employees EmployeeLogin(string userName, string password)
        {
            Employees employeeFromDb = db.Employees.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();
            return employeeFromDb;
        }

        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            Employees employeeWithUserName = db.Employees.Where(e => e.UserName == userName).FirstOrDefault();
            return employeeWithUserName == null;
        }


        //// TODO Items: ////
        
        // TODO: Allow any of the CRUD operations to occur here
        internal static void RunEmployeeQueries(Employees employee, string crudOperation)
        {
            var foundEmployee = db.Employees.Where(x => x.EmployeeId == employee.EmployeeId).FirstOrDefault();
            switch (crudOperation.ToLower())
            {
                case "delete" :
                    var query = db.Animals.Where(x => x.EmployeeId == employee.EmployeeId).Select(x => x);
                    
                    foreach (var item in query)
                    {
                        item.EmployeeId = null;
                    }

                    db.Employees.Remove(foundEmployee);
                    db.SaveChanges();
                    break;
                case "create" :
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    break;
                case "read" :
                    UserInterface.DisplayEmployeeInfo(employee);
                    break;
                case "update" :
                    if (employee.FirstName != null) {
                        foundEmployee.FirstName = employee.FirstName;
                    }
                    if (employee.LastName != null) {
                        foundEmployee.LastName = employee.LastName;
                    }
                    if (employee.Email != null) {
                        foundEmployee.Email = employee.Email;
                    }
                    if (employee.Password != null) {
                        foundEmployee.Password = employee.Password;
                    }
                    if (employee.EmployeeNumber != null) {
                        foundEmployee.EmployeeNumber = employee.EmployeeNumber;
                    }

                    db.SaveChanges();
                    break;
            }
        }

        // TODO: Animal CRUD Operations
        internal static void AddAnimal(Animals animal)
        {
            Animals newAnimal = new Animals();

            newAnimal.Name = animal.Name;
            newAnimal.Weight = animal.Weight;
            newAnimal.Age = animal.Age;
            newAnimal.Demeanor = animal.Demeanor;
            newAnimal.KidFriendly = animal.KidFriendly;
            newAnimal.PetFriendly = animal.PetFriendly;
            newAnimal.Gender = animal.Gender;
            newAnimal.AdoptionStatus = animal.AdoptionStatus;
            newAnimal.CategoryId = animal.CategoryId;
            newAnimal.DietPlanId = animal.DietPlanId;
            newAnimal.EmployeeId = animal.EmployeeId;

            db.Add(newAnimal);
            db.SaveChanges();

            Console.Clear();
        }

        // Creates an animal
        // Additional method
        internal static Animals CreateAnAnimal(string name, int weight, int age, string demeanor, bool? kidFriendly, bool? petFriendly, string gender, string adoptionStatus, int categoryId, int dietPlanId, int employeeId)
        {
            Animals newAnimal = new Animals();

            newAnimal.Name = name;
            newAnimal.Weight = weight;
            newAnimal.Age = age;
            newAnimal.Demeanor = demeanor;
            newAnimal.KidFriendly = kidFriendly;
            newAnimal.PetFriendly = petFriendly;
            newAnimal.Gender = gender;
            newAnimal.AdoptionStatus = adoptionStatus;
            newAnimal.DietPlanId = dietPlanId;
            newAnimal.EmployeeId = employeeId;

            return newAnimal;
        }

        internal static Animals GetAnimalByID(int id)
        {
            Animals foundAnimal = db.Animals.Where(i => i.AnimalId == id).FirstOrDefault();
            return foundAnimal;
        }

        internal static bool? StringToBool(string x){
            if(x == "null"){
                return null;
            }
            if(x == "1"){
                return true;
            }
            if(x.ToLower()=="true" || x.ToLower()=="yes"){
                return true;
            } 

            return false;
        }

        internal static void UpdateAnimal(int animalId, Dictionary<int, string> updates)
        {            
            var query = (from animal in db.Animals
                        where animal.AnimalId == animalId
                        select animal).FirstOrDefault();

            foreach (var update in updates)
            {
                switch(update.Key){
                    case 1:
                        query.CategoryId = Int32.Parse(updates[1]);
                        break;
                    case 2:
                        query.Name = updates[2];
                        break;
                    case 3:
                        query.Age = Int32.Parse(updates[3]);
                        break;
                    case 4:
                        query.Demeanor = updates[4];
                        break;
                    case 5:
                        query.KidFriendly = StringToBool(updates[5]);
                        break;
                    case 6:
                        query.PetFriendly = StringToBool(updates[6]);
                        break;
                    case 7:
                        query.Weight = Int32.Parse(updates[7]);
                        break;
                    case 8:
                        query.AnimalId = Int32.Parse(updates[8]);
                        break;
                }
            }

            db.SaveChanges();
        }

        internal static void RemoveAnimal(Animals animal)
        {
            var queryRoom = db.Rooms.Where(a => a.AnimalId == animal.AnimalId).FirstOrDefault();
            if (queryRoom != null) {
                try
                {
                    if(queryRoom != null) {
                        queryRoom.AnimalId = null;
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            

            var queryShot = db.AnimalShots.Where(a => a.AnimalId == animal.AnimalId).Select(a => a);
            try
            {
                foreach (var item in queryShot)
                {
                    db.AnimalShots.Remove(item);
                }

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var queryAdoptions = db.Adoptions.Where(a => a.AnimalId == animal.AnimalId).Select(a => a);
            try
            {
                foreach (var item in queryAdoptions)
                {
                    db.Adoptions.Remove(item);
                }

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var queryAnimal = db.Animals.Where(a => a.AnimalId == animal.AnimalId).Select(a => a);
            if (queryAnimal != null)
            {
                foreach (var item in queryAnimal)
                {
                    db.Animals.Remove(item);
                }
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        // TODO: Animal Multi-Trait Search
        internal static IQueryable<Animals> SearchForAnimalsByMultipleTraits(Dictionary<int, string> updates) // parameter(s)?
       {
           //get Whole db of Animals
           IQueryable<Animals> foundAnimals = db.Animals;

            foreach(var update in updates){
                switch(update.Key){
                    case 1:
                        var categoryId = GetCategoryId(updates[1]);
                        foundAnimals = foundAnimals.Where((x) => x.CategoryId == categoryId);
                    break;
                    case 2:
                        foundAnimals = foundAnimals.Where(x => x.Name == updates[2]);
                    break; 
                    case 3:
                        foundAnimals = foundAnimals.Where(x => x.Age == Int32.Parse(updates[3]));
                    break;
                    case 4:
                        foundAnimals = foundAnimals.Where(x => x.Demeanor == updates[4]);
                    break;         
                    case 5:
                        foundAnimals = foundAnimals.Where(x => x.KidFriendly == StringToBool(updates[5]));
                    break;
                    case 6:
                        foundAnimals = foundAnimals.Where(x => x.PetFriendly == StringToBool(updates[6]));
                    break;
                    case 7:
                        foundAnimals = foundAnimals.Where(x => x.Weight == Int32.Parse(updates[7]));
                    break;
                    case 8:
                        foundAnimals = foundAnimals.Where(x => x.AnimalId == Int32.Parse(updates[8]));
                    break;
                }
            }

            return foundAnimals;
       }
         
        // TODO: Misc Animal Things
        internal static int GetCategoryId(string categoryName)
        {
            var foundCategoryId = db.Categories.Where(c => c.Name == categoryName).Select(x => x.CategoryId).FirstOrDefault();
            return foundCategoryId;
        }
        
        internal static Rooms GetRoom(int animalId)
        {
            var foundRoom = db.Rooms.Where(r => r.AnimalId == animalId).FirstOrDefault();
            return foundRoom;
        }

        internal static void UpdateRoom(int animalId, int roomNumber) {
            var query = db.Rooms.Where(x => x.RoomNumber == roomNumber).FirstOrDefault();
            query.AnimalId = animalId;
            db.SaveChanges();
        }
        
        internal static int GetDietPlanId(string dietPlanName)
        {
            var foundPlanId = db.DietPlans.Where(d => d.Name == dietPlanName).Select(x => x.DietPlanId).FirstOrDefault();
            return foundPlanId;
        }

        // TODO: Adoption CRUD Operations
        internal static void Adopt(Animals animal, Clients client)
        {
            var newAdoption = new Adoptions();

            newAdoption.ApprovalStatus = "In Progress";
            newAdoption.PaymentCollected = true;
            newAdoption.ClientId = client.ClientId;
            newAdoption.AnimalId = animal.AnimalId;
            newAdoption.AdoptionFee = 50;

            db.Adoptions.Add(newAdoption);
            db.SaveChanges();
        }

        internal static IQueryable<Adoptions> GetPendingAdoptions()
        {
            var query = db.Adoptions.Where(a => a.ApprovalStatus == "In Progress").Select(a => a);
            return query;
        }

        internal static void UpdateAdoption(bool isAdopted, Adoptions adoption)
        {
            var foundAdoption = db.Adoptions.Where(x => x.AnimalId == adoption.AnimalId && x.ClientId == adoption.ClientId).FirstOrDefault();
            if(isAdopted){
                foundAdoption.ApprovalStatus = "Approved/Adopted";
            } else{
                foundAdoption.ApprovalStatus = "Denied";
            }

            db.SaveChanges();
        }

        internal static void RemoveAdoption(int animalId, int clientId)
        {
            var query = db.Adoptions.Where(x => x.AnimalId == animalId && x.ClientId == clientId).FirstOrDefault();
            db.Adoptions.Remove(query);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // TODO: Shots Stuff
        internal static IQueryable<AnimalShots> GetShots(Animals animal)
        {
            var query = db.AnimalShots.Where(a => a.AnimalId == animal.AnimalId).Select(a => a);
            return query;
        }

        internal static void UpdateShot(string shotName, Animals animal)
        {
            var foundShot = db.Shots.Where(x => x.Name == shotName).FirstOrDefault();
            var newAnimalShot = new AnimalShots();

            newAnimalShot.AnimalId = animal.AnimalId;
            newAnimalShot.ShotId = foundShot.ShotId;
            newAnimalShot.DateReceived = DateTime.Now;
            
            db.AnimalShots.Add(newAnimalShot);
            db.SaveChanges();
        }

        // Bonus User Story
        internal static dynamic GetCsvData(string filePath)
        {
           string[] allLines = File.ReadAllLines(filePath);
           var query = from line in allLines
                       let data = line.Split(',')
                       select new
                       {
                           Name = data[0],
                           Age = data[1],
                           Weight = data[2],
                           Demeanor = data[3],
                           KidFriendly = data[4],
                           PetFriendly = data[5],
                           Gender = data[6],
                           AdoptionStatus = data[7],
                           CategoryId = data[8],
                           DietPlanId = data[9],
                           EmployeeId = data[10]
                       };

           return query;
        }

        internal static string RemoveAllChars(string s, string c)
        {
           int found = s.IndexOf(c);
           while(found != -1){
               s = s.Remove(found, 1);
               found = s.IndexOf(c);
           }

           return s;
        }

        internal static int? ToNullableInt(string s){
            if(s == "null") {
                return null;
            }
            try {
                return Int32.Parse(s);
            } 
                catch(Exception E){
                return null;
            }
        }

        internal static void AddAnimalsFromCsv(string filePath)
        {
            var csv = GetCsvData(filePath);

            foreach (var item in csv)
            {
                string name = RemoveAllChars(item.Name, " ");
                name = RemoveAllChars(name, "\"");
                string demeanor = RemoveAllChars(item.Demeanor, " ");
                demeanor = RemoveAllChars(demeanor, "\"");
                string gender = RemoveAllChars(item.Gender, " ");
                gender = RemoveAllChars(gender, "\"");
                string adoptionStatus = RemoveAllChars(item.AdoptionStatus, " ");
                adoptionStatus = RemoveAllChars(adoptionStatus, "\"");
                Animals newAnimal = new Animals();

                newAnimal.Name = name;
                newAnimal.Weight = Int32.Parse(item.Weight);
                newAnimal.Age = Int32.Parse(item.Age);
                newAnimal.Demeanor = demeanor;
                newAnimal.KidFriendly = StringToBool(item.KidFriendly);
                newAnimal.PetFriendly = StringToBool(item.PetFriendly);
                newAnimal.Gender = gender;
                newAnimal.AdoptionStatus = adoptionStatus;
                newAnimal.CategoryId = ToNullableInt(item.CategoryId);
                newAnimal.DietPlanId = ToNullableInt(item.DietPlanId);
                newAnimal.EmployeeId = ToNullableInt(item.EmployeeId);

                db.Animals.Add(newAnimal);
                db.SaveChanges();
            }
        }
    }
}