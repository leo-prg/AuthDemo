using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentAuthentication.Server.Models;
using RentAuthentication.Shared;

namespace RentAuthentication.Server.Services
{
    public class UserService : IUserService
    {
        private IUserAuthService _userAuthService;
        public UserService(IUserAuthService userAuthService)
        {
            _userAuthService = userAuthService;
        }
        public UserDTO GetUser(string Id)
        {
            RentUser user = _userAuthService.GetUser(Id);
            return new UserDTO() {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            };

        }

        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable<RentUser> users = _userAuthService.GetUsers();
            List<UserDTO> userDTOs = new List<UserDTO>();
            foreach(RentUser user in users)
            {
                userDTOs.Add(new UserDTO()
                {
                    Id=user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName
                });
            }
            return userDTOs;
        }
    }
}
