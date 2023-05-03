using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto_AndreTurismoApp.Models;

namespace Projeto_AndreTurismoApp.HotelService.Data
{
    public class Projeto_AndreTurismoAppHotelServiceContext : DbContext
    {
        public Projeto_AndreTurismoAppHotelServiceContext (DbContextOptions<Projeto_AndreTurismoAppHotelServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Projeto_AndreTurismoApp.Models.Hotel> Hotel { get; set; } = default!;
    }
}
