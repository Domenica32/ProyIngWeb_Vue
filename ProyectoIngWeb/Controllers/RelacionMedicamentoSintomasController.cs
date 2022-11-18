using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Medicamento;
using ProyectoIngWeb.Models.MedicametoSintomas;
using ProyectoIngWeb.Models.Medicamentos;
using ProyectoIngWeb.Models.Sintomas;

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

        // GET: api/RelacionMedicamentoSintomas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<MedicamentoSintomaViewModel>> Listar()
        
        {
            var Medsintomas = await _context.MedicamentoSintoma.Include(ms => ms.medicamento).Include(ms => ms.sintoma).ToListAsync();


            //Console.Write(Medsintomas);
            return Medsintomas.Select(ms => new MedicamentoSintomaViewModel
            {
                idMedicamento_Sintoma = ms.idMedicamento_Sintoma,
                idMedicamento_FK = ms.idMedicamento_FK,
                idSintoma_FK = ms.idSintoma_FK,
                medicamento = new MedicamentosViewModel { idMedicamento = ms.medicamento.idMedicamento,
                nombre= ms.medicamento.nombre},
                sintoma = new SintomasViewModel{ idSintoma = ms.sintoma.idSintoma,
                    LugarSintoma = ms.sintoma.LugarSintoma, TipoMalestar = ms.sintoma.TipoMalestar
                },




            }); ;
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

        // PUT: api/RelacionMedicamentoSintomas/Actualizar
        [HttpPut("[action]")]

        public async Task<IActionResult> Actualizar([FromBody]  ProyectoIngWeb.Models.MedicametoSintomas.ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.idMedicamento_Sintoma <= 0)
            {
                return BadRequest();
            }

            var Medsintoma = await _context.MedicamentoSintoma.FirstOrDefaultAsync(s => s.idMedicamento_Sintoma == model.idMedicamento_Sintoma);

            if (Medsintoma == null)
            {
                return NotFound();
            }

            Medsintoma.idMedicamento_FK = model.idMedicamento_FK;
            Medsintoma.idSintoma_FK = model.idSintoma_FK;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            return Ok();
        }



        // POST: api/RelacionMedicamentoSintomas/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody]  ProyectoIngWeb.Models.MedicametoSintomas.CrearViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             RelacionMedicamentoSintoma sinMed = new RelacionMedicamentoSintoma
             {
                idSintoma_FK = model.idSintoma_FK,
                idMedicamento_FK = model.idMedicamento_FK

            };
            _context.MedicamentoSintoma.Add(sinMed);
            try
            {

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/Medicamentos/5
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var medSin = await _context.MedicamentoSintoma.FindAsync(id);
            if (medSin == null)
            {
                return NotFound();
            }

            _context.MedicamentoSintoma.Remove(medSin);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }



            return Ok(medSin);
        }
        private bool RelacionMedicamentoSintomaExists(int id)
        {
            return _context.MedicamentoSintoma.Any(e => e.idMedicamento_Sintoma == id);
        }
    }
}
