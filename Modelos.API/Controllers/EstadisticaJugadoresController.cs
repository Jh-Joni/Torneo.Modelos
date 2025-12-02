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
    public class EstadisticaJugadoresController : ControllerBase
    {
        private readonly ModelosAPIContext _context;

        public EstadisticaJugadoresController(ModelosAPIContext context)
        {
            _context = context;
        }

        // GET: api/EstadisticaJugadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadisticaJugador>>> GetEstadisticaJugador()
        {
            return await _context.EstadisticasJugadores.ToListAsync();
        }

        // GET: api/EstadisticaJugadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadisticaJugador>> GetEstadisticaJugador(int id)
        {
            var estadisticaJugador = await _context.EstadisticasJugadores.FindAsync(id);

            if (estadisticaJugador == null)
            {
                return NotFound();
            }

            return estadisticaJugador;
        }

        // PUT: api/EstadisticaJugadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadisticaJugador(int id, EstadisticaJugador estadisticaJugador)
        {
            if (id != estadisticaJugador.Id)
            {
                return BadRequest();
            }

            _context.Entry(estadisticaJugador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadisticaJugadorExists(id))
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

        // POST: api/EstadisticaJugadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstadisticaJugador>> PostEstadisticaJugador(EstadisticaJugador estadisticaJugador)
        {
            _context.EstadisticasJugadores.Add(estadisticaJugador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadisticaJugador", new { id = estadisticaJugador.Id }, estadisticaJugador);
        }

        // DELETE: api/EstadisticaJugadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadisticaJugador(int id)
        {
            var estadisticaJugador = await _context.EstadisticasJugadores.FindAsync(id);
            if (estadisticaJugador == null)
            {
                return NotFound();
            }

            _context.EstadisticasJugadores.Remove(estadisticaJugador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadisticaJugadorExists(int id)
        {
            return _context.EstadisticasJugadores.Any(e => e.Id == id);
        }
    }
}
