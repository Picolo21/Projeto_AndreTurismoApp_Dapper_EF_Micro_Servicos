using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_AndreTurismoApp.AddressService.Data;
using Projeto_AndreTurismoApp.Models;
using Projeto_AndreTurismoApp.Models.DTO;
using Projeto_AndreTurismoApp.Services;

namespace Projeto_AndreTurismoApp.AddressService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly Projeto_AndreTurismoAppAddressServiceContext _context;
        private readonly PostOfficeService _postOfficeService;

        public AddressesController(Projeto_AndreTurismoAppAddressServiceContext context, PostOfficeService postOfficeService)
        {
            _context = context;
            _postOfficeService = postOfficeService;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            return await _context.Address
                .Include(x => x.City)
                .ToListAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }

            var address = await _context.Address
                .Include(x => x.City)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Address>> PutAddress(int id, Address address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return address;
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            if (_context.Address == null)
            {
                return Problem("Entity set 'Projeto_AndreTurismoAppAddressServiceContext.Address' is null.");
            }

            // Chamar o serviço de consulta de endereço ViaCEP
            AddressDTO addressDTO = _postOfficeService.GetAddress(address.PostalCode).Result;
            var addressComplet = new Address(addressDTO);
            _context.Address.Add(addressComplet);

            await _context.SaveChangesAsync();

            return addressComplet;
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return (_context.Address?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
