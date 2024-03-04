﻿using System;
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
    public class RecetasController : ControllerBase
    {
        private readonly DbPlatoVoladorContext _context;

        public RecetasController(DbPlatoVoladorContext context)
        {
            _context = context;
        }

        // GET: api/Recetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receta>>> GetRecetas()
        {
            if (_context.Recetas == null)
            {
                return NotFound();
            }
            return await _context.Recetas.ToListAsync();
        }

        // GET: api/Recetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Receta>> GetReceta(int id)
        {
            if (_context.Recetas == null)
            {
                return NotFound();
            }
            var receta = await _context.Recetas.FindAsync(id);

            if (receta == null)
            {
                return NotFound();
            }

            return receta;
        }

        // PUT: api/Recetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceta(int id, Receta receta)
        {
            if (id != receta.RecetaId)
            {
                return BadRequest();
            }

            _context.Entry(receta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecetaExists(id))
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

        // POST: api/Recetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Receta>> PostReceta(RecetaDTO receta)
        {
            if (_context.Recetas == null)
            {
                return Problem("Entity set 'DbPlatoVoladorContext.Recetas'  is null.");
            }
            _context.Recetas.Add(receta.Receta);
            await _context.SaveChangesAsync();

            foreach (var Ingrediente in receta.IngredientesPorReceta)
            {
                if (_context.IngredientesPorReceta == null)
                {
                    return Problem("Entity set 'DbPlatoVoladorContext.IngredientesPorReceta'  is null.");
                }
                var ingredienteAux = Ingrediente;
                ingredienteAux.Receta.RecetaId = receta.Receta.RecetaId;
                _context.IngredientesPorReceta.Add(ingredienteAux);
                await _context.SaveChangesAsync();
            }

            //return CreatedAtAction("GetReceta", new { id = receta.RecetaId }, receta);
            return Ok();
        }

        // DELETE: api/Recetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceta(int id)
        {
            if (_context.Recetas == null)
            {
                return NotFound();
            }
            var receta = await _context.Recetas.FindAsync(id);
            if (receta == null)
            {
                return NotFound();
            }

            _context.Recetas.Remove(receta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecetaExists(int id)
        {
            return (_context.Recetas?.Any(e => e.RecetaId == id)).GetValueOrDefault();
        }
    }
}
