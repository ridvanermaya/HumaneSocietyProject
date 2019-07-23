using System;
using System.Collections.Generic;

namespace HumaneSociety.Entity
{
    public partial class AnimalShots
    {
        public int AnimalId { get; set; }
        public int ShotId { get; set; }
        public DateTime? DateReceived { get; set; }

        public virtual Animals Animal { get; set; }
        public virtual Shots Shot { get; set; }
    }
}
