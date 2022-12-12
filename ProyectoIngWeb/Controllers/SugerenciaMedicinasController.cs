using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Sugerencia;
using ProyectoIngWeb.Models.Sugerencia;
using ProyectoIngWeb.Models.Medicamentos;
using ProyectoIngWeb.Models.Usuarios;

namespace ProyectoIngWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SugerenciaMedicinasController : ControllerBase
    {
        private readonly DbContextProy _context;

        public SugerenciaMedicinasController(DbContextProy context)
        {
            _context = context;
        }

        // GET: api/SugerenciaMedicinas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<SugerenciaViewModel>> Listar()

        {
            var sugerencia = await _context.Sugerencia.Include(m => m.medicamento).Include(u => u.usuario).ToListAsync();


            //Console.Write(Medsintomas);
            return sugerencia.Select(s => new SugerenciaViewModel
            {
                idSugerencia = s.idSugerencia,
                idMedicamento_FK = s.idMedicamento_FK,
                idUsuario_FK = s.idUsuario_FK,
                medicamento = new MedicamentosViewModel
                {
                    idMedicamento = s.medicamento.idMedicamento,
                    nombre = s.medicamento.nombre,
                    compuestoQuimico = s.medicamento.compuestoQuimico,
                    dosis = s.medicamento.dosis
                },
                usuario = new UsuariosViewModel
                {
                    idUsuarios = s.usuario.idUsuarios,
                    NombreUsuario = s.usuario.NombreUsuario
                },




            });
        }

        // GET: api/SugerenciaMedicinas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SugerenciaMedicina>> GetSugerenciaMedicina(int id)
        {
            var sugerenciaMedicina = await _context.Sugerencia.FindAsync(id);

            if (sugerenciaMedicina == null)
            {
                return NotFound();
            }

            return sugerenciaMedicina;
        }

        // PUT: api/SugerenciaMedicinas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSugerenciaMedicina(int id, SugerenciaMedicina sugerenciaMedicina)
        {
            if (id != sugerenciaMedicina.idSugerencia)
            {
                return BadRequest();
            }

            _context.Entry(sugerenciaMedicina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SugerenciaMedicinaExists(id))
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

        // POST: api/SugerenciaMedicinas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SugerenciaMedicina>> PostSugerenciaMedicina(SugerenciaMedicina sugerenciaMedicina)
        {
            _context.Sugerencia.Add(sugerenciaMedicina);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSugerenciaMedicina", new { id = sugerenciaMedicina.idSugerencia }, sugerenciaMedicina);
        }

        // DELETE: api/SugerenciaMedicinas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SugerenciaMedicina>> DeleteSugerenciaMedicina(int id)
        {
            var sugerenciaMedicina = await _context.Sugerencia.FindAsync(id);
            if (sugerenciaMedicina == null)
            {
                return NotFound();
            }

            _context.Sugerencia.Remove(sugerenciaMedicina);
            await _context.SaveChangesAsync();

            return sugerenciaMedicina;
        }

        private bool SugerenciaMedicinaExists(int id)
        {
            return _context.Sugerencia.Any(e => e.idSugerencia == id);
        }
    }
}
