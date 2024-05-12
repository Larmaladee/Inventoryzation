using System;
using System.Collections.Generic;

namespace InventorizationApi.Models
{
    public partial class EquipmentMovement
    {
        public int MovementId { get; set; }
        public int EquipmentId { get; set; }
        public int? PreviousClassId { get; set; }
        public int? NewClassId { get; set; }
        public int? PreviousEquipmentStatusId { get; set; }
        public int? NewEquipmentStatusId { get; set; }
        public DateTime MovementDate { get; set; }

    }
}
