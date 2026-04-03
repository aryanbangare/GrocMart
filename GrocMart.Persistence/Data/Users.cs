using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Persistence.Data
{
     public sealed class Users
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PasswordHash { get; set; }
    }
}
