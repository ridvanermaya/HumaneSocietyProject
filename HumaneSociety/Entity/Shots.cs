using System;
using System.Collections.Generic;

namespace HumaneSociety.Entity
{
    public partial class Shots
    {
        public Shots()
        {
            AnimalShots = new HashSet<AnimalShots>();
        }

        public int ShotId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AnimalShots> AnimalShots { get; set; }
    }
}
