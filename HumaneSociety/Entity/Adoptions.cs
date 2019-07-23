using System;
using System.Collections.Generic;

namespace HumaneSociety.Entity
{
    public partial class Adoptions
    {
        public int ClientId { get; set; }
        public int AnimalId { get; set; }
        public string ApprovalStatus { get; set; }
        public int? AdoptionFee { get; set; }
        public bool? PaymentCollected { get; set; }

        public virtual Animals Animal { get; set; }
        public virtual Clients Client { get; set; }
    }
}
