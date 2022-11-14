using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Medicamento;

namespace ProyectoIngWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelacionMedicamentoSintomasController : ControllerBase
    {
        private readonly DbContextProy _context;

        public RelacionMedicamentoSintomasController(DbContextProy context)
        {
            _context = context;
        }

        // GET: api/RelacionMedicamentoSintomas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelacionMedicamentoSintoma>>> GetMedicamentoSintoma()
        {
            return await _context.MedicamentoSintoma.ToListAsync();
        }

        // GET: api/RelacionMedicamentoSintomas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RelacionMedicamentoSintoma>> GetRelacionMedicamentoSintoma(int id)
        {
            var relacionMedicamentoSintoma = await _context.MedicamentoSintoma.FindAsync(id);

            if (relacionMedicamentoSintoma == null)
            {
                return NotFound();
            }

            return relacionMedicamentoSintoma;
        }

        // PUT: api/RelacionMedicamentoSintomas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelacionMedicamentoSintoma(int id, RelacionMedicamentoSintoma relacionMedicamentoSintoma)
        {
            if (id != relacionMedicamentoSintoma.idMedicamento_Sintoma)
            {
                return BadRequest();
            }

            _context.Entry(relacionMedicamentoSintoma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelacionMedicamentoSintomaExists(id))
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

        // POST: api/RelacionMedicamentoSintomas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RelacionMedicamentoSintoma>> PostRelacionMedicamentoSintoma(RelacionMedicamentoSintoma relacionMedicamentoSintoma)
        {
            _context.MedicamentoSintoma.Add(relacionMedicamentoSintoma);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRelacionMedicamentoSintoma", new { id = relacionMedicamentoSintoma.idMedicamento_Sintoma }, relacionMedicamentoSintoma);
        }

        // DELETE: api/RelacionMedicamentoSintomas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RelacionMedicamentoSintoma>> DeleteRelacionMedicamentoSintoma(int id)
        {
            var relacionMedicamentoSintoma = await _context.MedicamentoSintoma.FindAsync(id);
            if (relacionMedicamentoSintoma == null)
            {
                return NotFound();
            }

            _context.MedicamentoSintoma.Remove(relacionMedicamentoSintoma);
            await _context.SaveChangesAsync();

            return relacionMedicamentoSintoma;
        }

        private bool RelacionMedicamentoSintomaExists(int id)
        {
            return _context.MedicamentoSintoma.Any(e => e.idMedicamento_Sintoma == id);
        }
    }
}
