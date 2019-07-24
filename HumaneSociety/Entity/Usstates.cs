using System;
using System.Collections.Generic;

namespace HumaneSociety.Entity
{
    public partial class USStates
    {
        public USStates()
        {
            Addresses = new HashSet<Addresses>();
        }

        public int UsstateId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public virtual ICollection<Addresses> Addresses { get; set; }
    }
}
