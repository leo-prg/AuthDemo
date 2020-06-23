using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using RentAuthentication.Client.Exceptions;
using RentAuthentication.Client.Helpers;
using RentAuthentication.Client.Repository;
using RentAuthentication.Shared;

namespace RentAuthentication.Client.Services
{
    

    public class ClientUserService : IClientUserService
    {
        private string apiURI = "https://localhost:44377/";

        private readonly IGenericRepository _genericRepository;
        private readonly AuthenticationStateProvider _authProvider;

        public event EventHandler<UserAuthenticatedArgs> UserAuthenticatedEvent;
        public ClientUserService(IGenericRepository genericRepository,
            AuthenticationStateProvider authProvider)
        {
            _genericRepository = genericRepository;
            _authProvider = authProvider;
            _authProvider.AuthenticationStateChanged += _authProvider_AuthenticationStateChanged;
        }

        private void _authProvider_AuthenticationStateChanged(Task<Microsoft.AspNetCore.Components.Authorization.AuthenticationState> task)
        {
            if (task.Result.User.Identity.IsAuthenticated)
            {
                var claimsIdentity = (ClaimsIdentity)task.Result.User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                UserAuthenticatedEvent?.Invoke(this, new UserAuthenticatedArgs(userId));
            }
            else
            {
                UserAuthenticatedEvent?.Invoke(this, new UserAuthenticatedArgs(""));
            }
        }

        public async Task<LoginResult> LoginUser(Credentials user)
        {
            try
            {
                LoginResult lr = await _genericRepository.PostAsync<Credentials, LoginResult>(apiURI+"api/login", user);
                await ((IAuthenticationStateProvider)_authProvider).MarkUserAsAuthenticated(lr);
                return lr;
            } catch (HttpRequestExceptionEx e)
            {
                Debug.WriteLine(e.HttpCode);
                await LogoutUser();
                return new LoginResult();
            }
        }
        public async Task LogoutUser()
        {
            await ((IAuthenticationStateProvider)_authProvider).MarkUserAsLoggedOut();
        }
        public async Task<LoginResult> RegisterUser(UserRegistration user)
        {
            try {
                LoginResult lr = await _genericRepository.PostAsync<UserRegistration, LoginResult>(apiURI+"api/register", user);
                await ((IAuthenticationStateProvider)_authProvider).MarkUserAsAuthenticated(lr);
                return lr;
            }
            catch (HttpRequestExceptionEx e)
            {
                Debug.WriteLine(e.HttpCode);
                await LogoutUser();
                return new LoginResult();
            }
        }
        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            try
            {
                return await _genericRepository.GetAsync<IEnumerable<UserDTO>>(apiURI+"api/users");
            }
            catch (HttpRequestExceptionEx e)
            {
                Debug.WriteLine(e.HttpCode);
                await LogoutUser();
                return new List<UserDTO>();
            }

        }
        public async Task<UserDTO> GetUserInfo(string Id)
        {
            try { 
            return await _genericRepository.GetAsync<UserDTO>(apiURI+$"api/user/{Id}");

            }
            catch (HttpRequestExceptionEx e)
            {
                Debug.WriteLine(e.HttpCode);
                await LogoutUser();
                return new UserDTO();
            }
        }

        public async Task<WeatherForecast[]> GetForecasts()
        {
            return await _genericRepository.GetAsync<WeatherForecast[]>(apiURI+"WeatherForecast");
        }
    }
}
