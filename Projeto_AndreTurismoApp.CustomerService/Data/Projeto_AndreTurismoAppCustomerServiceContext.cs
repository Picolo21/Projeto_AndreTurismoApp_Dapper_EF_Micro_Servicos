using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto_AndreTurismoApp.Models;

namespace Projeto_AndreTurismoApp.CustomerService.Data
{
    public class Projeto_AndreTurismoAppCustomerServiceContext : DbContext
    {
        public Projeto_AndreTurismoAppCustomerServiceContext (DbContextOptions<Projeto_AndreTurismoAppCustomerServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Projeto_AndreTurismoApp.Models.Customer> Customer { get; set; } = default!;
    }
}
