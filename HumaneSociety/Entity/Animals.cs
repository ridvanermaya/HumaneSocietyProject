using System;
using System.Collections.Generic;

namespace HumaneSociety.Entity
{
    public partial class Animals
    {
        public Animals()
        {
            Adoptions = new HashSet<Adoptions>();
            AnimalShots = new HashSet<AnimalShots>();
            Rooms = new HashSet<Rooms>();
        }

        public int AnimalId { get; set; }
        public string Name { get; set; }
        public int? Weight { get; set; }
        public int? Age { get; set; }
        public string Demeanor { get; set; }
        public bool? KidFriendly { get; set; }
        public bool? PetFriendly { get; set; }
        public string Gender { get; set; }
        public string AdoptionStatus { get; set; }
        public int? CategoryId { get; set; }
        public int? DietPlanId { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Categories Category { get; set; }
        public virtual DietPlans DietPlan { get; set; }
        public virtual Employees Employee { get; set; }
        public virtual ICollection<Adoptions> Adoptions { get; set; }
        public virtual ICollection<AnimalShots> AnimalShots { get; set; }
        public virtual ICollection<Rooms> Rooms { get; set; }
    }
}
