using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Usuarios;
using ProyectoIngWeb.Models.Usuarios;
using ProyectoIngWeb.Models.Usuarios.Usuarios;

namespace ProyectoIngWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosRolsController : ControllerBase
    {
        private readonly DbContextProy _context;

        public UsuariosRolsController(DbContextProy context)
        {
            _context = context;
        }

        //GET: api/UsuariosRols/Listar
       [HttpGet("[action]")]
        public async Task<IEnumerable<UsuariosViewModel>> Listar()
        {


            //var usuario = await _context.Usuarios.Include(c => c.RolUsuarios).ToListAsync();
            var usuario = await _context.Usuarios.ToListAsync();

            return usuario.Select(c => new UsuariosViewModel
            {
                idUsuarios= c.idUsuarios,
                idRolUsuarios_FK = c.idRolUsuarios_FK,
                //rol = c.RolUsuarios.Nombre,
                NombreUsuario = c.NombreUsuario,
                ApellidoUsuario = c.ApellidoUsuario,
                EmailUsuario = c.EmailUsuario,
                PasswordUsuario_hash = c.PasswordUsuario_hash,
                condicion = c.condicion
            });
        }

        // GET: api/UsuariosRols/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Usuario>> Mostrar([FromRoute] int id)
        {
            var usuariosRol = await _context.Usuarios.FindAsync(id);

            if (usuariosRol == null)
            {
                return NotFound();
            }

            return Ok(new UsuariosViewModel
            {
                idUsuarios = usuariosRol.idUsuarios,
                idRolUsuarios_FK = usuariosRol.idRolUsuarios_FK,
                NombreUsuario = usuariosRol.NombreUsuario,
                ApellidoUsuario = usuariosRol.ApellidoUsuario,
                EmailUsuario = usuariosRol.EmailUsuario
            });
        }

        // PUT: api/UsuariosRols/5/Actualizar

        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.idUsuarios <= 0)
            {
                return BadRequest();
            }

            var usuarios = await _context.Usuarios.FirstOrDefaultAsync(c => c.idUsuarios == model.idUsuarios);

            if (usuarios == null)
            {
                return NotFound();
            }
            
            usuarios.idRolUsuarios_FK = model.idRolUsuarios_FK;
            usuarios.NombreUsuario = model.NombreUsuario;
            usuarios.ApellidoUsuario = model.ApellidoUsuario;
            usuarios.EmailUsuario = model.EmailUsuario.ToLower();
            //usuarios.PasswordUsuario_hash = model.PasswordUsuario_hash;
            //usuarios.PasswordUsuario_salt = model.PasswordUsuario_hash;
            if (model.act_password == true)
            {
                CrearPasswordHash(model.PasswordUsuario, out byte[] passwordHash, out byte[] passwordSalt);
                usuarios.PasswordUsuario_hash = passwordHash;
                usuarios.PasswordUsuario_salt = passwordSalt;
            }
            // usuarios.PasswordUsuario_hash = model.PasswordUsuario_hash;


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

        // POST: api/UsuariosRols/Crear

        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {



            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var email = model.EmailUsuario.ToLower();

            if (await _context.Usuarios.AnyAsync(c => c.EmailUsuario == email))
            {
                return BadRequest("El email ya existe");
            }
            CrearPasswordHash(model.PasswordUsuario, out byte[] passwordHash, out byte[] passwordSalt);
            Usuario usua = new Usuario
            {
                idRolUsuarios_FK = model.idRolUsuarios_FK,
                NombreUsuario = model.NombreUsuario,
                ApellidoUsuario = model.ApellidoUsuario,
                EmailUsuario = model.EmailUsuario.ToLower(),
                PasswordUsuario_hash = passwordHash ,
                PasswordUsuario_salt = passwordSalt,
                condicion = true

            };
            _context.Usuarios.Add(usua);
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

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; //envio la llave
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));//envio la contrasenia encriptada
            }

        }


        // DELETE: api/UsuariosRols/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Usuario>> Eliminar([FromRoute] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var usuariosRol = await _context.Usuarios.FindAsync(id);
            if (usuariosRol == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuariosRol);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }



            return Ok(usuariosRol);
        }


        //PUT: api/UsuariosRols/Desactivar/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Desactivar([FromRoute] int id )
        {
           if(id<=0)
            { 
                return BadRequest();
            }
            var usuarios = await _context.Usuarios.FirstOrDefaultAsync(c => c.idUsuarios == id);

            if (usuarios == null)
            {
                return NotFound();
            }
            usuarios.condicion = false;
           

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

        //PUT: api/UsuariosRols/Activar/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var usuarios = await _context.Usuarios.FirstOrDefaultAsync(c => c.idUsuarios == id);

            if (usuarios == null)
            {
                return NotFound();
            }
            usuarios.condicion = true;


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

        //[HttpPost("[action]")]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    var email = model.email.ToLower();
        //    var usuario = await _context.UsuariosRol.FirstOrDefaultAsync(u => u.EmailUsuario == email);
        //    if(usuario==null)
        //    {
        //        return NotFound();
        //    }
        //}



        private bool UsuariosRolExists(int id)
        {
            return _context.Usuarios.Any(e => e.idUsuarios == id);
        }
    }
}
