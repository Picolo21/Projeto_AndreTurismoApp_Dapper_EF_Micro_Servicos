using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto_AndreTurismoApp.Models;

namespace Projeto_AndreTurismoApp.PackageService.Data
{
    public class Projeto_AndreTurismoAppPackageServiceContext : DbContext
    {
        public Projeto_AndreTurismoAppPackageServiceContext (DbContextOptions<Projeto_AndreTurismoAppPackageServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Projeto_AndreTurismoApp.Models.Package> Package { get; set; } = default!;
    }
}
