using System;
using System.Collections.Generic;

namespace InventorizationApi.Models
{
    public partial class EquipmentCategory
    {
        public int EquipmentCategoryId { get; set; }
        public string Category { get; set; } = null!;

    }
}
