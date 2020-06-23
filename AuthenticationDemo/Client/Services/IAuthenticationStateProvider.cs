using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentAuthentication.Shared;

namespace RentAuthentication.Client.Services
{
    public interface IAuthenticationStateProvider
    {
        Task MarkUserAsAuthenticated(LoginResult loginResult);
        Task MarkUserAsLoggedOut();
    }
}
