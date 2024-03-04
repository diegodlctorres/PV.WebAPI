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

            var query = from receta in _context.Recetas
                        join usuario in _context.Usuarios on receta.UsuarioId equals usuario.UsuarioId
                        join ingredientesPorReceta in _context.IngredientesPorReceta on receta.RecetaId equals ingredientesPorReceta.RecetaId into ingredientesGroup
                        select new
                        {
                            RecetaId = receta.RecetaId,
                            NombreReceta = receta.NombreReceta,
                            InstruccionesPreparacion = receta.InstruccionesPreparacion,
                            test = receta.test,
                            test2 = receta.test2,
                            Usuario = usuario,
                            Ingredientes = ingredientesGroup.Select(ingredientePorReceta => new
                            {
                                IngredienteId = ingredientePorReceta.IngredienteId,
                                NombreIngrediente = ingredientePorReceta.Ingrediente.NombreIngrediente,
                                Cantidad = ingredientePorReceta.Cantidad,
                                UnidadMedida = ingredientePorReceta.UnidadMedida
                            }).ToList()
                        };

            return Ok(query.ToList());
        }

        // GET: api/Recetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetReceta(int id)
        {
            if (_context.Recetas == null)
            {
                return NotFound();
            }
            var recetaAux = (from receta in _context.Recetas
                         where receta.RecetaId == id
                         join usuario in _context.Usuarios on receta.UsuarioId equals usuario.UsuarioId
                         join ingredientesPorReceta in _context.IngredientesPorReceta on receta.RecetaId equals ingredientesPorReceta.RecetaId into ingredientesGroup
                         select new
                         {
                             RecetaId = receta.RecetaId,
                             NombreReceta = receta.NombreReceta,
                             InstruccionesPreparacion = receta.InstruccionesPreparacion,
                             test = receta.test,
                             test2 = receta.test2,
                             Usuario = usuario,
                             Ingredientes = ingredientesGroup.Select(ingredientePorReceta => new
                             {
                                 IngredienteId = ingredientePorReceta.IngredienteId,
                                 NombreIngrediente = ingredientePorReceta.Ingrediente.NombreIngrediente,
                                 Cantidad = ingredientePorReceta.Cantidad,
                                 UnidadMedida = ingredientePorReceta.UnidadMedida
                             }).ToList()
                         }).FirstOrDefault();
            if (recetaAux == null)
            {
                return NotFound();
            }

            return recetaAux;
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
            _context.Recetas.Add(receta);
            await _context.SaveChangesAsync();
            foreach (IngredientesPorReceta ingrediente in receta.Ingredientes)
            {
                var ingredienteAux = new IngredientesPorReceta();
                ingredienteAux.RecetaId = receta.RecetaId;
                ingredienteAux.IngredienteId = ingrediente.IngredienteId;
                ingredienteAux.UnidadMedida = ingrediente.UnidadMedida;
                ingredienteAux.Cantidad = ingrediente.Cantidad;

                _context.IngredientesPorReceta.Add(ingredienteAux);
                await _context.SaveChangesAsync();
            }
            return Ok("okey");
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