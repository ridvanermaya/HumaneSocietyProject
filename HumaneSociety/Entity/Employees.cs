using System;
using System.Collections.Generic;

namespace HumaneSociety.Entity
{
    public partial class Employees
    {
        public Employees()
        {
            Animals = new HashSet<Animals>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? EmployeeNumber { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Animals> Animals { get; set; }
    }
}
