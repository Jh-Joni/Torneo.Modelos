using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Torneo.Modelos;

namespace Modelos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GesTorneosController : ControllerBase
    {
        private readonly ModelosAPIContext _context;

        public GesTorneosController(ModelosAPIContext context)
        {
            _context = context;
        }

        // GET: api/GesTorneos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GesTorneo>>> GetGesTorneo()
        {
            return await _context.GesTorneos.ToListAsync();
        }

        // GET: api/GesTorneos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GesTorneo>> GetGesTorneo(int id)
        {
            var gesTorneo = await _context.GesTorneos.FindAsync(id);

            if (gesTorneo == null)
            {
                return NotFound();
            }

            return gesTorneo;
        }

        // PUT: api/GesTorneos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGesTorneo(int id, GesTorneo gesTorneo)
        {
            if (id != gesTorneo.Id)
            {
                return BadRequest();
            }

            _context.Entry(gesTorneo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GesTorneoExists(id))
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

        // POST: api/GesTorneos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GesTorneo>> PostGesTorneo(GesTorneo gesTorneo)
        {
            _context.GesTorneos.Add(gesTorneo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGesTorneo", new { id = gesTorneo.Id }, gesTorneo);
        }

        // DELETE: api/GesTorneos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGesTorneo(int id)
        {
            var gesTorneo = await _context.GesTorneos.FindAsync(id);
            if (gesTorneo == null)
            {
                return NotFound();
            }

            _context.GesTorneos.Remove(gesTorneo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GesTorneoExists(int id)
        {
            return _context.GesTorneos.Any(e => e.Id == id);
        }
    }
}
