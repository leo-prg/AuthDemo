using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentAuthentication.Shared;

namespace RentAuthentication.Server.Services
{
    public interface IUserService
    {
        UserDTO GetUser(string Id);
        IEnumerable<UserDTO> GetUsers();
    }
}
