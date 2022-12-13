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


        // POST: api/SugerenciaMedicinas/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody]  CrearSugerenciaViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SugerenciaMedicina sugerencia = new SugerenciaMedicina
            {
                idMedicamento_FK = model.idMedicamento_FK,
                idUsuario_FK =model.idUsuario_FK
                

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

        private bool SugerenciaMedicinaExists(int id)
        {
            return _context.Sugerencia.Any(e => e.idSugerencia == id);
        }
    }
}
