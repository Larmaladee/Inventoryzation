using System;
using System.Collections.Generic;

namespace InventorizationApi.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public int IdRole { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
