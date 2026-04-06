using GrocMart.Core.Dtos;
using GrocMart.Core.Requests;
using GrocMart.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GrocMart.Services.Services
{
    public sealed class UsersServices
    {
        private readonly AppDbContext _Dbcontext;
        public UsersServices(AppDbContext Dbcontext)
        {
            _Dbcontext = Dbcontext ?? throw new ArgumentNullException(nameof(Dbcontext));
        }
        public IEnumerable<UsersDto> GetUserslist()
        {
            IReadOnlyList<UsersDto> users = _Dbcontext.Users.Select(u => new UsersDto(u.Id, u.Name, u.PasswordHash)).ToList();
            return users;

        }
        public UsersDto? CreateUsersRequest(CreateUsersRequest request)
        {
            try
            {
                var user = new Persistence.Data.Users
                {
                    Name = request.Name,
                    PasswordHash = request.PasswordHash
                };
                _Dbcontext.Users.Add(user);
                _Dbcontext.SaveChanges();
                return new UsersDto(user.Id, user.Name, user.PasswordHash);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the user: {ex.Message}");
            }
            return null;
        }

        public UsersDto? Register(CreateUsersRequest request)
        {
            try
            {
                var existingUser = _Dbcontext.Users.FirstOrDefault(u => u.Name == request.Name);
                if (existingUser != null)
                {
             
                    return null; 
                }

                var user = new Persistence.Data.Users
                {
                    Name = request.Name,
                    PasswordHash = request.PasswordHash
                };
                _Dbcontext.Users.Add(user);
                _Dbcontext.SaveChanges();
                return new UsersDto(user.Id, user.Name, user.PasswordHash);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while registering the user: {ex.Message}");
            }
            return null;
        }

        public UsersDto? Login(LoginRequest request)
        {
            try
            {
                var user = _Dbcontext.Users.FirstOrDefault(u => u.Id == request.Id && u.PasswordHash == request.PasswordHash);

                if (user != null)
                {
                    return new UsersDto(user.Id, user.Name, user.PasswordHash);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while logging in: {ex.Message}");
            }
            //finally { Console.WriteLine("succesfull"); }

            return null;
        }
    }
}
