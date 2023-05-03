using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto_AndreTurismoApp.Models;

namespace Projeto_AndreTurismoApp.TicketService.Data
{
    public class Projeto_AndreTurismoAppTicketServiceContext : DbContext
    {
        public Projeto_AndreTurismoAppTicketServiceContext (DbContextOptions<Projeto_AndreTurismoAppTicketServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Projeto_AndreTurismoApp.Models.Ticket> Ticket { get; set; } = default!;
    }
}
