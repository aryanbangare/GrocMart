using GrocMart.Core.Dtos;
using GrocMart.Core.Requests;
using GrocMart.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GrocMart.Persistence.Data;

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
            IReadOnlyList<UsersDto> users = _Dbcontext.Users.Select(u => new UsersDto(u.Id, u.Name)).ToList();
            return users;
                                                                                                                                                                                                                                                                                                                                                                                           
        }
        public UsersDto? CreateUsersRequest(CreateUsersRequest request)
        {
            try
            {
                var user = new Persistence.Data.Users
                {
                    Name = request.Name,
                    Password = request.Password
                };
                _Dbcontext.Users.Add(user);
                _Dbcontext.SaveChanges();
                return new UsersDto(user.Id, user.Name);
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
                    Password = request.Password
                };
                _Dbcontext.Users.Add(user);
                _Dbcontext.SaveChanges();
                return new UsersDto(user.Id, user.Name);
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
                var user = _Dbcontext.Users
                    .FirstOrDefault(u => u.Name == request.Name 
                      && u.Password == request.Password);

                if (user != null)
                {                                                                               
                    return new UsersDto(user.Id, user.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while logging in:{ex.Message}");
            }
         
            return null;
        }
        public void DeleteUsers(int id)
        {
            Users? user = _Dbcontext.Users.Find(id);
            if (user is null)
            {
                throw new InvalidOperationException($"User with Id {id} not found.");
            }
            _Dbcontext.Users.Remove(user);
            _Dbcontext.SaveChanges();
        }
    }
}
