using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto_AndreTurismoApp.Models;

namespace Projeto_AndreTurismoApp.CityService.Data
{
    public class Projeto_AndreTurismoAppCityServiceContext : DbContext
    {
        public Projeto_AndreTurismoAppCityServiceContext (DbContextOptions<Projeto_AndreTurismoAppCityServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Projeto_AndreTurismoApp.Models.City> City { get; set; } = default!;
    }
}
