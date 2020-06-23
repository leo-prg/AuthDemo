using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using RentAuthentication.Client.Services;
using RentAuthentication.Client.Repository;
using Microsoft.AspNetCore.Components.Authorization;

namespace RentAuthentication.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<IClientUserService, ClientUserService>();
            builder.Services.AddScoped<CurrentUserInfo>();

            builder.Services.AddScoped<IGenericRepository, GenericRepository>();

            await builder.Build().RunAsync();
        }
    }
}
