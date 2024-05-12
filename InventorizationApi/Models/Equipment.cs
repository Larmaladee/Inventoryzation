using System;
using System.Collections.Generic;

namespace InventorizationApi.Models
{
    public partial class Equipment
    {
        public int EquipmentId { get; set; }
        public int IdEquipmentStatus { get; set; }
        public int IdEquipmentCategory { get; set; }
        public int? IdClass { get; set; }
        public string Name { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;

    }
}
