using System;
using System.Collections.Generic;

namespace HumaneSociety.Entity
{
    public partial class DietPlans
    {
        public DietPlans()
        {
            Animals = new HashSet<Animals>();
        }

        public int DietPlanId { get; set; }
        public string Name { get; set; }
        public string FoodType { get; set; }
        public int? FoodAmountInCups { get; set; }

        public virtual ICollection<Animals> Animals { get; set; }
    }
}
