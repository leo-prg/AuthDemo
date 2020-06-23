using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentAuthentication.Server.Models;
using RentAuthentication.Shared;

namespace RentAuthentication.Server.Services
{
    public interface IUserAuthService
    {
        Task<LoginResult> RegisterUserAsync(UserRegistration user);
        Task<LoginResult> LoginUserAsync(Credentials user);
        RentUser GetUser(string Id);
        IEnumerable<RentUser> GetUsers();
    }
}
