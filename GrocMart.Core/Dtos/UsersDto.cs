using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Core.Dtos
{
    public sealed class UsersDto(int Id, string? Name, string? PasswordHash)
    {
        public int Id { get; } = Id;
        public string? Name { get; } = Name;
        public string? PasswordHash { get; } = PasswordHash;

    }
}
