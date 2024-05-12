using System;
using System.Collections.Generic;

namespace InventorizationApi.Models
{
    public partial class Application
    {
        public int ApplicationId { get; set; }
        public int IdUser { get; set; }
        public int IdApplicationStatus { get; set; }
        public int? IdEquipment { get; set; }
        public string Description { get; set; } = null!;
    }
}
