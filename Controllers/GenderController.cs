using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using genderapi.Models;

namespace genderapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Gender
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gender>>> GetGender()
        {
          if (_context.Gender == null)
          {
              return NotFound();
          }
            return await _context.Gender.ToListAsync();
        }

        // GET: api/Gender/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gender>> GetGender(int id)
        {
          if (_context.Gender == null)
          {
              return NotFound();
          }
            var gender = await _context.Gender.FindAsync(id);

            if (gender == null)
            {
                return NotFound();
            }

            return gender;
        }

        // PUT: api/Gender/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGender(int id, Gender gender)
        {
            if (id != gender.id)
            {
                return BadRequest();
            }

            _context.Entry(gender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenderExists(id))
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

        // POST: api/Gender
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Gender>> PostGender(Gender gender)
        {
          if (_context.Gender == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Gender'  is null.");
          }
            _context.Gender.Add(gender);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGender", new { id = gender.id }, gender);
        }

        // DELETE: api/Gender/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGender(int id)
        {
            if (_context.Gender == null)
            {
                return NotFound();
            }
            var gender = await _context.Gender.FindAsync(id);
            if (gender == null)
            {
                return NotFound();
            }

            _context.Gender.Remove(gender);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenderExists(int id)
        {
            return (_context.Gender?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
