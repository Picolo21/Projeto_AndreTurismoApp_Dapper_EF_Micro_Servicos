using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_AndreTurismoApp.Models;
using Projeto_AndreTurismoApp.Models.DTO;
using Projeto_AndreTurismoApp.PackageService.Data;
using Projeto_AndreTurismoApp.Services;

namespace Projeto_AndreTurismoApp.PackageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly Projeto_AndreTurismoAppPackageServiceContext _context;
        private readonly PostOfficeService _postOfficeService;

        public PackagesController(Projeto_AndreTurismoAppPackageServiceContext context, PostOfficeService postOfficeService)
        {
            _context = context;
            _postOfficeService = postOfficeService;
        }

        // GET: api/Packages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackage()
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            return await _context.Package
                .Include(x => x.Hotel)
                    .ThenInclude(x => x.Address)
                        .ThenInclude(x => x.City)
                .Include(x => x.Ticket)
                    .ThenInclude(x => x.Origin)
                        .ThenInclude(x => x.City)
                .Include(x => x.Ticket)
                    .ThenInclude(x => x.Destination)
                        .ThenInclude(x => x.City)
                .Include(x => x.Ticket)
                    .ThenInclude(x => x.Customer)
                        .ThenInclude(x => x.Address)
                            .ThenInclude(x => x.City)
                .Include(x => x.Customer)
                    .ThenInclude(x => x.Address)
                        .ThenInclude(x => x.City)
                .ToListAsync();
        }

        // GET: api/Packages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(int id)
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            var package = await _context.Package.FindAsync(id);

            if (package == null)
            {
                return NotFound();
            }

            return package;
        }

        // PUT: api/Packages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackage(int id, Package package)
        {
            if (id != package.Id)
            {
                return BadRequest();
            }

            _context.Entry(package).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Packages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Package>> PostPackage(Package package)
        {
            if (_context.Package == null)
            {
                return Problem("Entity set 'Projeto_AndreTurismoAppPackageServiceContext.Package'  is null.");
            }
            AddressDTO addressHotel = _postOfficeService.GetAddress(package.Hotel.Address.PostalCode).Result;
            var addressHotelComplet = new Address(addressHotel);

            AddressDTO addressTicketOrigin = _postOfficeService.GetAddress(package.Ticket.Origin.PostalCode).Result;
            var addressTicketOriginComplet = new Address(addressTicketOrigin);

            AddressDTO addressTicketDestination = _postOfficeService.GetAddress(package.Ticket.Destination.PostalCode).Result;
            var addressTicketDestinationComplet = new Address(addressTicketDestination);

            AddressDTO addressTicketCustomer = _postOfficeService.GetAddress(package.Ticket.Customer.Address.PostalCode).Result;
            var addressTicketCustomerComplet = new Address(addressTicketCustomer);

            AddressDTO addressCustomer = _postOfficeService.GetAddress(package.Customer.Address.PostalCode).Result;
            var addressCustomerComplet = new Address(addressCustomer);

            package.Hotel.Address = addressHotelComplet;
            package.Ticket.Origin = addressTicketOriginComplet;
            package.Ticket.Destination = addressTicketDestinationComplet;
            package.Ticket.Customer.Address = addressTicketCustomerComplet;
            package.Customer.Address = addressCustomerComplet;

            _context.Package.Add(package);

            await _context.SaveChangesAsync();

            return package;
        }

        // DELETE: api/Packages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            var package = await _context.Package.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }

            _context.Package.Remove(package);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageExists(int id)
        {
            return (_context.Package?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
