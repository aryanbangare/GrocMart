using GrocMart.Core.Dtos;
using GrocMart.Core.Requests;
using GrocMart.Services.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace GrocMart.web.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersGroup(this IEndpointRouteBuilder endpoint)
        {
            return endpoint
            .MapGroup("Users");
        }
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder endpoint)
        {
            ArgumentNullException.ThrowIfNull(endpoint);
            IEndpointRouteBuilder userGroup = UsersEndpoints.MapUsersGroup(endpoint);

            userGroup.MapGet("", GetUsers);
            userGroup.MapPost("", CreateUsersRequest);
            userGroup.MapPost("register", Register);
            userGroup.MapPost("login", Login);
            userGroup.MapDelete("/{Id}", DeleteUsers);
            return endpoint;
        }
        private static Ok<IEnumerable<UsersDto>> GetUsers(UsersServices UsersService)
        {
            IEnumerable<UsersDto> Users = UsersService.GetUserslist();
            return TypedResults.Ok(Users);

        }
        private static IResult CreateUsersRequest(CreateUsersRequest request, UsersServices UsersService)
        {
            var result = UsersService.CreateUsersRequest(request);
            return result is not null
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest("Failed to create user");
        }
        private static IResult Register(CreateUsersRequest request, UsersServices UsersService)
        {
            var result = UsersService.Register(request);
            return result is not null
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest("Failed to register user");
        }
        private static async Task<IResult> Login(
            HttpContext context, LoginRequest request, UsersServices UsersService)
        {
            var result = UsersService.Login(request);

            if (result is not null)
            {

                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, result.Name),
                     new Claim(ClaimTypes.NameIdentifier, result.Id.ToString())
                };


                var identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(identity);
                await context.SignInAsync("Cookies", principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                });
                return result is not null
                    ? TypedResults.Ok(result)
                    : TypedResults.BadRequest("Failed to login user");
            }
            return null;
        }

        public static IResult DeleteUsers(int Id, UsersServices UsersService)
        {
            try
            {
                UsersService.DeleteUsers(Id);
                return TypedResults.Ok($"User with Id {Id} deleted successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return TypedResults.NotFound(ex.Message);
            }
        }
    }
}
