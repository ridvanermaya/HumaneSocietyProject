using System;
using System.Collections.Generic;

namespace HumaneSociety.Entity
{
    public partial class Clients
    {
        public Clients()
        {
            Adoptions = new HashSet<Adoptions>();
        }

        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? AddressId { get; set; }
        public string Email { get; set; }

        public virtual Addresses Address { get; set; }
        public virtual ICollection<Adoptions> Adoptions { get; set; }
    }
}
