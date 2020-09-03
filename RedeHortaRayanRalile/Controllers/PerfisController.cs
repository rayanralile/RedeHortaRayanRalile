using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RedeHortaRayanRalile.ApiServices.Implementations;
using RedeHortaRayanRalile.ApiServices.Interfaces;
using RedeHortaRayanRalile.Files.Interfaces;
using RedeHortaRayanRalile.Models.Perfis;

namespace RedeHortaRayanRalile.Controllers
{

    
    public class PerfisController : Controller
    {
        private readonly IPerfilApiService _perfilApiService;
        private readonly IPerfilFileUploader _perfilFileUploader;
        public PerfisController(IPerfilApiService perfilApiService,
            IPerfilFileUploader perfilFileUploader)
        {
            _perfilApiService = perfilApiService;
            _perfilFileUploader = perfilFileUploader;
        }
        // GET: PerfisController
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var usuarioLogin = this.User.Identity.Name;
            // Agora eu vou verificar se existe o Perfil
            // desse usuário. Se sim, retorno o Id e chamo o details
            // caso contrário, chamarei o Create

            var perfil = await _perfilApiService.GetPerfilFromLogin(usuarioLogin);
            if(perfil == null)
                return RedirectToAction(nameof(Create));
            if (perfil.Any())
                return View(perfil.FirstOrDefault());
            else
                return RedirectToAction(nameof(Create));
        }
        // O método abaixo serve para listar todos os perfis cadastrados, sem poder de edição
        public async Task<ActionResult> ListAll()
        {
            var perfilLista = await _perfilApiService.GetAllPerfis();
            return View(perfilLista);
        }
        // O próximo método permite apenas visualizar o perfil.
        public async Task<ActionResult> OnlyView(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var perfil = await _perfilApiService.GetOnlyViewPerfil(id.Value);
           
            if (perfil == null)
            {
                return NotFound();
            }
            return View(perfil);
        }

        // GET: PerfisController/Details/5
        //   public ActionResult Details(int id)
        //   {
        //       return View();
        //   }

        // GET: PerfisController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PerfisController/Create
        [Authorize]
        [HttpPost]
     //   [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Nome,Foto,Biografia,TipoHorta,Interesses")] PerfilCreateViewModel perfil, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                perfil.Foto = _perfilFileUploader.UploadFile(foto, Guid.NewGuid().ToString());
                await _perfilApiService.CreatePerfil(perfil, this.User.Identity.Name);

                return RedirectToAction(nameof(Index));
            }
            return View(perfil);
        }

        // GET: PerfisController/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var perfil = await _perfilApiService.GetPerfilEdit(id.Value);
            
            if (perfil == null)
                return NotFound();

            return View(perfil);
        }

        // POST: PerfisController/Edit/5
        [Authorize]
        [HttpPost]
     //   [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,UsuarioLogin,Nome,Foto,Biografia,TipoHorta,Interesses")] PerfilEditViewModel perfil, IFormFile photo)
        {
            if (id != perfil.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (photo is null)
                        perfil.Foto = (await _perfilApiService.GetPerfilEdit(id)).Foto;
                    else
                        perfil.Foto = _perfilFileUploader.UploadFile(photo, Guid.NewGuid().ToString());
                    
                    await _perfilApiService.UpdatePerfil(perfil, id);
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(perfil);
        }

        // GET: PerfisController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PerfisController/Delete/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
