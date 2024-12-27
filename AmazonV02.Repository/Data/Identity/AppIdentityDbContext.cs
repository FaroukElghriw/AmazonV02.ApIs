using AmazonV02.Core.Entites.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Repository.Data.Identity
{
	public class AppIdentityDbContext:IdentityDbContext<AppUser>
	{
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options):base(options) 
        { 
        }
    }
}
