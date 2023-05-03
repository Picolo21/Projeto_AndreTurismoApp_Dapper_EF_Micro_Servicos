using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_AndreTurismoApp.Models;
using Projeto_AndreTurismoApp.Models.DTO;
using Projeto_AndreTurismoApp.Services;
using Projeto_AndreTurismoApp.TicketService.Data;

namespace Projeto_AndreTurismoApp.TicketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly Projeto_AndreTurismoAppTicketServiceContext _context;
        private readonly PostOfficeService _postOfficeService;

        public TicketsController(Projeto_AndreTurismoAppTicketServiceContext context, PostOfficeService postOfficeService)
        {
            _context = context;
            _postOfficeService = postOfficeService;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicket()
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }
            return await _context.Ticket
                .Include(x => x.Origin)
                    .ThenInclude(x => x.City)
                .Include(x => x.Destination)
                    .ThenInclude(x => x.City)
                .Include(x => x.Customer)
                    .ThenInclude(x => x.Address)
                        .ThenInclude(x => x.City)
                .ToListAsync();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }
            var ticket = await _context.Ticket
                .Include(x => x.Origin)
                    .ThenInclude(x => x.City)
                .Include(x => x.Destination)
                    .ThenInclude(x => x.City)
                .Include(x => x.Customer)
                    .ThenInclude(x => x.Address)
                        .ThenInclude(x => x.City)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            if (_context.Ticket == null)
            {
                return Problem("Entity set 'Projeto_AndreTurismoAppTicketServiceContext.Ticket' is null.");
            }
            AddressDTO addressOrigin = _postOfficeService.GetAddress(ticket.Origin.PostalCode).Result;
            var addressOriginComplet = new Address(addressOrigin);

            AddressDTO addressDestination = _postOfficeService.GetAddress(ticket.Destination.PostalCode).Result;
            var addressDestinationComplet = new Address(addressDestination);

            AddressDTO addressCustomer = _postOfficeService.GetAddress(ticket.Customer.Address.PostalCode).Result;
            var addressCustomerComplet = new Address(addressCustomer);

            ticket.Origin = addressOriginComplet;
            ticket.Destination = addressDestinationComplet;
            ticket.Customer.Address = addressCustomerComplet;

            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return (_context.Ticket?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
