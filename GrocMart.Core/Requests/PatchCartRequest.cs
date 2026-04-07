using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Core.Requests
{
    public sealed class PatchCartRequest
    {
        public int Quantity { get; set; }
    }
}
