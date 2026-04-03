using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Core.Requests
{
    public  class CreateUsersRequest
    {
        public string Name { get; set; }
        public string PasswordHash { get; set; }

    }
}
