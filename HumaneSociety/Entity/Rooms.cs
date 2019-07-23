using System;
using System.Collections.Generic;

namespace HumaneSociety.Entity
{
    public partial class Rooms
    {
        public int RoomId { get; set; }
        public int? RoomNumber { get; set; }
        public int? AnimalId { get; set; }

        public virtual Animals Animal { get; set; }
    }
}
