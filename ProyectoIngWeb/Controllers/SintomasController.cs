using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Medicamento;
using ProyectoIngWeb.Models.Sintomas;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoIngWeb.Controllers
{

    //[Authorize(Roles = "1")]
    [Route("api/[controller]")]
    [ApiController]
    public class SintomasController : ControllerBase
    {
        private readonly DbContextProy _context;

        public SintomasController(DbContextProy context)
        {
            _context = context;
        }

        // GET: api/Sintomas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<SintomasViewModel>> Listar()
        {
            var sintomas = await _context.Sintoma.ToListAsync();

            return sintomas.Select(s => new SintomasViewModel
            {
                idSintoma = s.idSintoma,
                LugarSintoma = s.LugarSintoma,
                TipoMalestar = s.TipoMalestar,
                estado = s.estado
            });
        }


        // GET: api/Sintomas/ListarSelect
        [HttpGet("[action]")]
        public async Task<IEnumerable<SintomasViewModel>> ListarSelect()
        {
            var sintomas = await _context.Sintoma.ToListAsync();

            return sintomas.Select(s => new SintomasViewModel
            {
                idSintoma = s.idSintoma,
                LugarSintoma = s.LugarSintoma,
                TipoMalestar = s.TipoMalestar,
                estado = s.estado
            }).Where(s => s.estado);
        }

        // GET: api/Sintomas/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Mostrar([FromRoute] int id)
        {
            var sintoma = await _context.Sintoma.FindAsync(id);

            if (sintoma == null)
            {
                return NotFound();
            }

            return Ok(new SintomasViewModel
            {
                idSintoma = sintoma.idSintoma,
                LugarSintoma = sintoma.LugarSintoma,
                TipoMalestar = sintoma.TipoMalestar,
                estado = sintoma.estado
            });
        }

        // PUT: api/Sintomas/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.idSintoma <= 0)
            {
                return BadRequest();
            }

            var sintoma = await _context.Sintoma.FirstOrDefaultAsync(s => s.idSintoma == model.idSintoma);

            if (sintoma == null)
            {
                return NotFound();
            }

            sintoma.LugarSintoma = model.LugarSintoma;
            sintoma.TipoMalestar = model.TipoMalestar;
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


        // POST: api/Sintomas/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Sintoma sin = new Sintoma
            {
                LugarSintoma = model.LugarSintoma,
                TipoMalestar = model.TipoMalestar,
                estado = true

            };
            _context.Sintoma.Add(sin);
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

        // DELETE: api/Sintomas/5
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var sintoma = await _context.Sintoma.FindAsync(id);
            if (sintoma == null)
            {
                return NotFound();
            }

            _context.Sintoma.Remove(sintoma);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }



            return Ok(sintoma);
        }

        //PUT: api/Sintomas/Desactivar/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var sintoma = await _context.Sintoma.FirstOrDefaultAsync(s => s.idSintoma == id);

            if (sintoma == null)
            {
                return NotFound();
            }
            sintoma.estado = false;


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
            var sintoma = await _context.Sintoma.FirstOrDefaultAsync(s => s.idSintoma == id);

            if (sintoma == null)
            {
                return NotFound();
            }
            sintoma.estado = true;


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


        private bool SintomaExists(int id)
        {
            return _context.Sintoma.Any(e => e.idSintoma == id);
        }
    }
}
