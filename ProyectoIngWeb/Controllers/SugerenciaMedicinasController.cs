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
using Microsoft.AspNetCore.Authorization;

namespace ProyectoIngWeb.Controllers
{
   // [Authorize(Roles ="Usuarios")]
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
            var userContext = HttpContext.User;

            var userId = int.Parse(userContext.Claims.First(c => c.Type == "idUsuarios").Value);

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




            }).Where(u => u.idUsuario_FK == userId);
        }


        // POST: api/SugerenciaMedicinas/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody]  CrearSugerenciaViewModel model)
        {
            var userContext = HttpContext.User;

            var userId = int.Parse(userContext.Claims.First(c => c.Type == "idUsuarios").Value);

            var userEntity = await _context.Usuarios.FirstAsync(u => u.idUsuarios == userId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SugerenciaMedicina sugerencia = new SugerenciaMedicina
            {
                idMedicamento_FK = model.idMedicamento_FK,
                idUsuario_FK = userId
                

            };
            _context.Sugerencia.Add(sugerencia);
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

        // DELETE: api/SugerenciaMedicinas/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var sugerencia = await _context.Sugerencia.FindAsync(id);
            if (sugerencia == null)
            {
                return NotFound();
            }

            _context.Sugerencia.Remove(sugerencia);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }



            return Ok(sugerencia);
        }

        private bool SugerenciaMedicinaExists(int id)
        {
            return _context.Sugerencia.Any(e => e.idSugerencia == id);
        }
    }
}
