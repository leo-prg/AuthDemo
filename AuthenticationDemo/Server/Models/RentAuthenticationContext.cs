using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentAuthentication.Server.Services;
using RentAuthentication.Shared;

namespace RentAuthentication.Server.Models
{
    public class RentAuthenticationContext : IdentityDbContext<RentUser>
    {
        public RentAuthenticationContext()
        {
        }
        public RentAuthenticationContext(DbContextOptions<RentAuthenticationContext> options)
                : base(options)
        {
        }

    }
}
