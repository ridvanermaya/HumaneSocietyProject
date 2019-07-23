using System;
using System.Collections.Generic;

namespace HumaneSociety.Entity
{
    public partial class Categories
    {
        public Categories()
        {
            Animals = new HashSet<Animals>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Animals> Animals { get; set; }
    }
}
