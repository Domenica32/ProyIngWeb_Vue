using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Medicamento;
using ProyectoIngWeb.Models.Medicamentos;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoIngWeb.Controllers
{
    //[Authorize(Roles = "1")]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentosController : ControllerBase
    {
        private readonly DbContextProy _context;
        public RelacionMedicamentoSintomasController MedicamentoSintoma;

        public MedicamentosController(DbContextProy context)
        {
            _context = context;
             MedicamentoSintoma = new RelacionMedicamentoSintomasController(context);

        }


    // GET: api/Medicamentos/Listar

    [HttpGet("[action]")]
        public async Task<IEnumerable<MedicamentosViewModel>> Listar()
        {
            var medicamentos = await _context.Medicamento.ToListAsync();

            return medicamentos.Select(m => new MedicamentosViewModel
            {
                idMedicamento = m.idMedicamento,
                nombre = m.nombre,
                compuestoQuimico = m.compuestoQuimico,
                dosis = m.dosis,
                estado = m.estado
            });
        }

        // GET: api/Medicamentos/ListarSelect
        [HttpGet("[action]")]
        public async Task<IEnumerable<MedicamentosViewModel>> ListarSelect()
        {
            var medicamentos = await _context.Medicamento.ToListAsync();

            return medicamentos.Select(m => new MedicamentosViewModel
            {
                idMedicamento = m.idMedicamento,
                nombre = m.nombre,
                compuestoQuimico = m.compuestoQuimico,
                dosis = m.dosis,
                estado = m.estado
            }).Where(m => m.estado == true);
        }


        //CORE 
        // GET: api/Medicamentos/ListarMedicamentoCore
        [HttpGet("[action]/{alergia}:{sintoma}")]
        public async Task<IEnumerable<MedicamentosViewModel>> ListarMedicamentoCore(int sintoma, [FromRoute] string alergia)
        {
            var medicamentos = await _context.Medicamento.ToListAsync();

            List<MedicamentosViewModel> ListaCoreMedicamentos = medicamentos.Select(m => new MedicamentosViewModel
            {
                nombre = m.nombre,
                compuestoQuimico = m.compuestoQuimico,
                estado = m.estado,
                idMedicamento = m.idMedicamento
            }).Where(m => m.estado == true).ToList();

            List<MedicamentosViewModel> ListaComparacionMedicamento = new List<MedicamentosViewModel>() ;

            foreach (MedicamentosViewModel medicamento in ListaCoreMedicamentos)
            {
                if (!medicamento.compuestoQuimico.Equals(alergia) )
                {
                    var comprobacion = await MedicamentoSintoma.ListarSintomaCore(sintoma,medicamento.idMedicamento);
                    if (comprobacion) { 
                        ListaComparacionMedicamento.Add(medicamento);
                        }
                }
            }
            return ListaComparacionMedicamento;
        }







        // GET: api/Medicamentos/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Mostrar([FromRoute]int id)
        {
            var medicamento = await _context.Medicamento.FindAsync(id);

            if (medicamento == null)
            {
                return NotFound();
            }

            return Ok(new MedicamentosViewModel {
                idMedicamento = medicamento.idMedicamento,
                nombre = medicamento.nombre,
                compuestoQuimico = medicamento.compuestoQuimico,
                dosis = medicamento.dosis,
                estado = medicamento.estado


            });
        }

        // PUT: api/Medicamentos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar( [FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.idMedicamento <= 0)
            {
                return BadRequest();
            }

            var medicamento = await _context.Medicamento.FirstOrDefaultAsync(c => c.idMedicamento == model.idMedicamento);

            if (medicamento == null)
            {
                return NotFound();
            }

            medicamento.nombre = model.nombre;
            medicamento.compuestoQuimico = model.nombre;
            medicamento.dosis = model.dosis;
            try
            {
                await _context.SaveChangesAsync();
            }catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            return Ok();
        }

        // POST: api/Medicamentos/Crear
        [HttpPost ("[action]") ]
        public async Task<ActionResult> Crear ([FromBody] CrearViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            Medicamento med = new Medicamento
            {
                nombre = model.nombre,
                compuestoQuimico = model.compuestoQuimico,
                dosis = model.dosis,
                estado = true

            };
            _context.Medicamento.Add(med);
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
        public async Task<ActionResult> Eliminar([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var medicamento = await _context.Medicamento.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }

            _context.Medicamento.Remove(medicamento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }



            return Ok(medicamento);
        }



        //PUT: api/Medicamentos/Desactivar/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var medicamento = await _context.Medicamento.FirstOrDefaultAsync(m => m.idMedicamento == id);

            if (medicamento == null)
            {
                return NotFound();
            }
            medicamento.estado = false;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepcion
                return BadRequest();

            }

            return Ok();
        }

        //PUT: api/Medicamentos/Activar/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var medicamento = await _context.Medicamento.FirstOrDefaultAsync(m => m.idMedicamento == id);

            if (medicamento == null)
            {
                return NotFound();
            }
            medicamento.estado = true;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepcion
                return BadRequest();

            }

            return Ok();
        }
        private bool MedicamentoExists(int id)
        {
            return _context.Medicamento.Any(e => e.idMedicamento == id);
        }
    }
}
