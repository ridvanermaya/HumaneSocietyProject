using System;
using System.Collections.Generic;

namespace HumaneSociety.Entity
{
    public partial class Addresses
    {
        public Addresses()
        {
            Clients = new HashSet<Clients>();
        }

        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public int? UsstateId { get; set; }
        public int? Zipcode { get; set; }

        public virtual USStates Usstate { get; set; }
        public virtual ICollection<Clients> Clients { get; set; }
    }
}
