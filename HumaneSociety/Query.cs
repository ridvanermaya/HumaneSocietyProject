using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
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
        }

        // Creates an animal
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

        internal static void UpdateAnimal(int animalId, Dictionary<int, string> updates)
        {            
            var query = (from animal in db.Animals
                        where animal.AnimalId == animalId
                        select animal).FirstOrDefault();

            Console.WriteLine(query);
            //Console.WriteLine(query.Category.Name);

            Func<string, bool?> stringToBool = (string x) => {
                if(x=="true"){
                    return true;
                } else{
                    return false;
                }
            };

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
                        query.KidFriendly = stringToBool(updates[5]);
                        break;
                    case 6:
                        query.PetFriendly = stringToBool(updates[6]);
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
            throw new NotImplementedException();
        }
        
        // TODO: Animal Multi-Trait Search
        internal static IQueryable<Animals> SearchForAnimalsByMultipleTraits(Dictionary<int, string> updates) // parameter(s)?
       {
           string category = updates[1];
           string name = updates[2];
           //get Whole db of Animals
           IQueryable<Animals> foundAnimals = db.Animals;
           int? age;
           try {
               age = Int32.Parse(updates[3]);
           } catch (Exception E)
           {
               age = null;
           }
           string demeanor = updates[4];
           int? weight; 
           try
           {
               weight = Int32.Parse(updates[7]);
           }
           catch (System.Exception)
           {
               weight =  null;
           }
           
           int? ID;
           try
           {
               ID = Int32.Parse(updates[8]);
           } catch (Exception E)
           {
               ID = null;
           }
           if (!string.IsNullOrEmpty(category))
           {
               //get All the animals of tnis category
               foundAnimals = foundAnimals.Where((x) => x.Category.Name == category);
           }
           if (!string.IsNullOrEmpty(name))
           {
               foundAnimals = foundAnimals.Where((x) => x.Name == name);
           }
           if (age != null)
           {
           }
           if (!string.IsNullOrEmpty(demeanor))
           {
           }
           if (ID != null)
           {
               foundAnimals = null;
               foundAnimals.AsEnumerable().Concat(new[] { GetAnimalByID((int)ID) });
            }
        //    if (foundAnimals == null)
        //    {
        //        throw new NotImplementedException();
        //    }
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
        
        internal static int GetDietPlanId(string dietPlanName)
        {
            var foundPlanId = db.DietPlans.Where(d => d.Name == dietPlanName).Select(x => x.DietPlanId).FirstOrDefault();
            return foundPlanId;
        }

        // TODO: Adoption CRUD Operations
        internal static void Adopt(Animals animal, Clients client)
        {
            throw new NotImplementedException();
        }

        internal static IQueryable<Adoptions> GetPendingAdoptions()
        {
            throw new NotImplementedException();
        }

        internal static void UpdateAdoption(bool isAdopted, Adoptions adoption)
        {
            throw new NotImplementedException();
        }

        internal static void RemoveAdoption(int animalId, int clientId)
        {
            throw new NotImplementedException();
        }

        // TODO: Shots Stuff
        internal static IQueryable<AnimalShots> GetShots(Animals animal)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateShot(string shotName, Animals animal)
        {
            throw new NotImplementedException();
        }
    }
}