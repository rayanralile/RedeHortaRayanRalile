using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Model.Models;
using Domain.Model.Interfaces.Services;
using Application.Api.Models.Perfil;

namespace Application.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfisController : ControllerBase
    {
        private readonly IPerfilService _perfilService;

        public PerfisController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        // GET: api/Perfis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerfilResponse>>> GetPerfis([FromQuery] string usuariologin)
        {
            if (!String.IsNullOrEmpty(usuariologin))
            {
                var todosPerfis = await _perfilService.GetAll();

                if (!todosPerfis.Any())
                    return Ok();

                var perfil = todosPerfis.FirstOrDefault(x => String.Equals(x.UsuarioLogin, usuariologin));
                if (perfil == null) // Aqui que entra o redirecionamento para view de criação
                    return Ok(new List<PerfilResponse>().AsEnumerable());

                var listaUno = new List<PerfilResponse>();
                listaUno.Add(new PerfilResponse
                {
                    Id = perfil.Id,
                    Nome = perfil.Nome,
                    Interesses = perfil.Interesses,
                    TipoHorta = perfil.TipoHorta,
                    UsuarioLogin = perfil.UsuarioLogin,
                    Biografia = perfil.Biografia,
                    Foto = perfil.Foto
                });

                return Ok(listaUno.AsEnumerable());
            }
            else
            {
                var todos = await _perfilService.GetAll();
                List<PerfilResponse> todosPerfis = new List<PerfilResponse>();
                foreach (var item in todos)
                {
                    todosPerfis.Add(new PerfilResponse
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        Biografia = item.Biografia,
                        Foto = item.Foto,
                        Interesses = item.Interesses,
                        TipoHorta = item.TipoHorta,
                        UsuarioLogin = item.UsuarioLogin
                    });
                }
                return Ok(todosPerfis.AsEnumerable());
            }
        }

        // GET: api/Perfis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerfilResponse>> GetPerfil(int id)
        {
            var perfil = await _perfilService.GetById(id);

            if (perfil == null)
            {
                return NotFound();
            }
            var perfilResponse = new PerfilResponse
            {
                Id = perfil.Id,
                UsuarioLogin = perfil.UsuarioLogin,
                TipoHorta = perfil.TipoHorta,
                Biografia = perfil.Biografia,
                Foto = perfil.Foto,
                Interesses = perfil.Interesses,
                Nome = perfil.Nome
            };

            return perfilResponse;
        }



        // PUT: api/Perfis/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerfil(int id, [FromBody] PerfilRequest perfilRequest)
        {
            if (ModelState.IsValid)
            {
                var perfil = new Perfil
                {
                    Id = id,
                    Nome = perfilRequest.Nome,
                    Interesses = perfilRequest.Interesses,
                    Biografia = perfilRequest.Biografia,
                    Foto = perfilRequest.Foto,
                    TipoHorta = perfilRequest.TipoHorta,
                    UsuarioLogin = perfilRequest.UsuarioLogin
                };
                try
                {
                    await _perfilService.Update(perfil);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfilExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            else
            {
                return BadRequest("Existe erro na requisição");
            }
            return NoContent();
        }

        // POST: api/Perfis
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Perfil>> PostPerfil(PerfilRequest perfilRequest)
        {
            var perfil = new Perfil
            {
                Nome = perfilRequest.Nome,
                Biografia = perfilRequest.Biografia,
                UsuarioLogin = perfilRequest.UsuarioLogin,
                TipoHorta = perfilRequest.TipoHorta,
                Foto = perfilRequest.Foto,
                Interesses = perfilRequest.Interesses
            };
            await _perfilService.Create(perfil);
            //perfilResponse
            var perfilResponse = new PerfilResponse
            {
                Id = perfil.Id,
                Biografia = perfil.Biografia,
                Interesses = perfil.Interesses,
                Foto = perfil.Foto,
                TipoHorta = perfil.TipoHorta,
                Nome = perfil.Nome,
                UsuarioLogin = perfil.UsuarioLogin
            };
            return CreatedAtAction("GetPerfil", new { id = perfilResponse.Id }, perfilResponse);
        }

        // DELETE: api/Perfis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Perfil>> DeletePerfil(int id)
        {
            var perfil = await _perfilService.GetById(id);
            if (perfil == null)
            {
                return NotFound();
            }

            await _perfilService.Delete(id);

            return perfil;
        }

        private bool PerfilExists(int id)
        {
            return _perfilService.GetById(id) != null;
        }
    }
}
