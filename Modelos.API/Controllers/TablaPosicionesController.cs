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
    public class TablaPosicionesController : ControllerBase
    {
        private readonly ModelosAPIContext _context;

        public TablaPosicionesController(ModelosAPIContext context)
        {
            _context = context;
        }

        // GET: api/TablaPosiciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TablaPosicion>>> GetTablaPosicion()
        {
            return await _context.TablaPosiciones.ToListAsync();
        }

        // GET: api/TablaPosiciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TablaPosicion>> GetTablaPosicion(int id)
        {
            var tablaPosicion = await _context.TablaPosiciones.FindAsync(id);

            if (tablaPosicion == null)
            {
                return NotFound();
            }

            return tablaPosicion;
        }

        // PUT: api/TablaPosiciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTablaPosicion(int id, TablaPosicion tablaPosicion)
        {
            if (id != tablaPosicion.Id)
            {
                return BadRequest();
            }

            _context.Entry(tablaPosicion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TablaPosicionExists(id))
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

        // POST: api/TablaPosiciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TablaPosicion>> PostTablaPosicion(TablaPosicion tablaPosicion)
        {
            _context.TablaPosiciones.Add(tablaPosicion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTablaPosicion", new { id = tablaPosicion.Id }, tablaPosicion);
        }

        // DELETE: api/TablaPosiciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTablaPosicion(int id)
        {
            var tablaPosicion = await _context.TablaPosiciones.FindAsync(id);
            if (tablaPosicion == null)
            {
                return NotFound();
            }

            _context.TablaPosiciones.Remove(tablaPosicion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TablaPosicionExists(int id)
        {
            return _context.TablaPosiciones.Any(e => e.Id == id);
        }
    }
}
