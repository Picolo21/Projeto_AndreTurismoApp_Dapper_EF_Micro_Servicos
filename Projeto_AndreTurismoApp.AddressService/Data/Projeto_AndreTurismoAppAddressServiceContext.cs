using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto_AndreTurismoApp.Models;

namespace Projeto_AndreTurismoApp.AddressService.Data
{
    public class Projeto_AndreTurismoAppAddressServiceContext : DbContext
    {
        public Projeto_AndreTurismoAppAddressServiceContext (DbContextOptions<Projeto_AndreTurismoAppAddressServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Projeto_AndreTurismoApp.Models.Address> Address { get; set; } = default!;
    }
}
