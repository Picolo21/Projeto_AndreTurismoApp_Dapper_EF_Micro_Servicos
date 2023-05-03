using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_AndreTurismoApp.HotelService.Data;
using Projeto_AndreTurismoApp.Models;
using Projeto_AndreTurismoApp.Models.DTO;
using Projeto_AndreTurismoApp.Services;

namespace Projeto_AndreTurismoApp.HotelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly Projeto_AndreTurismoAppHotelServiceContext _context;
        private readonly PostOfficeService _postOfficeService;

        public HotelsController(Projeto_AndreTurismoAppHotelServiceContext context, PostOfficeService postOfficeService)
        {
            _context = context;
            _postOfficeService = postOfficeService;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotel()
        {
            if (_context.Hotel == null)
            {
                return NotFound();
            }
            return await _context.Hotel
                .Include(x => x.Address)
                    .ThenInclude(x => x.City)
                .ToListAsync();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            if (_context.Hotel == null)
            {
                return NotFound();
            }
            var hotel = await _context.Hotel
                .Include(x => x.Address)
                    .ThenInclude(x => x.City)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            if (_context.Hotel == null)
            {
                return Problem("Entity set 'Projeto_AndreTurismoAppHotelServiceContext.Hotel'  is null.");
            }

            AddressDTO addressDTO = _postOfficeService.GetAddress(hotel.Address.PostalCode).Result;
            var addressComplet = new Address(addressDTO);

            hotel.Address = addressComplet;

            _context.Hotel.Add(hotel);
            await _context.SaveChangesAsync();

            return hotel;
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (_context.Hotel == null)
            {
                return NotFound();
            }
            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return (_context.Hotel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
