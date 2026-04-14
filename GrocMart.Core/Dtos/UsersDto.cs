using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Core.Dtos
{
    public sealed class UsersDto(int Id, string? Name)
    {
        public int Id { get; } = Id;
        public string? Name { get; } = Name;

    }

}
