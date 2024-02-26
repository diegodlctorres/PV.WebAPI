using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PV.WebAPI.Models;

namespace PV.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientesPorRecetasController : ControllerBase
    {
        private readonly DbPlatoVoladorContext _context;

        public IngredientesPorRecetasController(DbPlatoVoladorContext context)
        {
            _context = context;
        }

        // GET: api/IngredientesPorRecetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientesPorReceta>>> GetIngredientesPorReceta()
        {
          if (_context.IngredientesPorReceta == null)
          {
              return NotFound();
          }
            return await _context.IngredientesPorReceta.ToListAsync();
        }

        // GET: api/IngredientesPorRecetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientesPorReceta>> GetIngredientesPorReceta(int id)
        {
          if (_context.IngredientesPorReceta == null)
          {
              return NotFound();
          }
            var ingredientesPorReceta = await _context.IngredientesPorReceta.FindAsync(id);

            if (ingredientesPorReceta == null)
            {
                return NotFound();
            }

            return ingredientesPorReceta;
        }

        // PUT: api/IngredientesPorRecetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredientesPorReceta(int id, IngredientesPorReceta ingredientesPorReceta)
        {
            if (id != ingredientesPorReceta.RelacionId)
            {
                return BadRequest();
            }

            _context.Entry(ingredientesPorReceta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientesPorRecetaExists(id))
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

        // POST: api/IngredientesPorRecetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IngredientesPorReceta>> PostIngredientesPorReceta(IngredientesPorReceta ingredientesPorReceta)
        {
          if (_context.IngredientesPorReceta == null)
          {
              return Problem("Entity set 'DbPlatoVoladorContext.IngredientesPorReceta'  is null.");
          }
            _context.IngredientesPorReceta.Add(ingredientesPorReceta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredientesPorReceta", new { id = ingredientesPorReceta.RelacionId }, ingredientesPorReceta);
        }

        // DELETE: api/IngredientesPorRecetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientesPorReceta(int id)
        {
            if (_context.IngredientesPorReceta == null)
            {
                return NotFound();
            }
            var ingredientesPorReceta = await _context.IngredientesPorReceta.FindAsync(id);
            if (ingredientesPorReceta == null)
            {
                return NotFound();
            }

            _context.IngredientesPorReceta.Remove(ingredientesPorReceta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientesPorRecetaExists(int id)
        {
            return (_context.IngredientesPorReceta?.Any(e => e.RelacionId == id)).GetValueOrDefault();
        }
    }
}
