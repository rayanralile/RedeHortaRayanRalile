using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedeHortaRayanRalile.ApiServices.Implementations;
using RedeHortaRayanRalile.ApiServices.Interfaces;
using RedeHortaRayanRalile.Files.Interfaces;
using RedeHortaRayanRalile.Models.Post;

namespace RedeHortaRayanRalile.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPerfilApiService _perfilApiService;
        private readonly IPostApiServices _postApiServices;
        private readonly IPostFileUploader _postFileUploader;
        public PostController(IPerfilApiService perfilApiService,
            IPostApiServices postApiServices,
            IPostFileUploader postFileUploader)
        {
            _perfilApiService = perfilApiService;
            _postApiServices = postApiServices;
            _postFileUploader = postFileUploader;
        }
        // GET: PostController
        public async Task<ActionResult> Index()
        {
            var usuarioLogin = this.User.Identity.Name;
            // Agora eu vou verificar se existe o Perfil
            // desse usuário. Se sim, retorno o Id do perfil
            // caso contrário, chamarei o Create Perfil 
            // Usuário precisa criar seu perfil para ver os posts
            // da minha rede social.

            var perfil = await _perfilApiService.GetPerfilFromLogin(usuarioLogin);
            if (!perfil.Any())
                return RedirectToAction("Create","Perfis");
            int perfilid = perfil.SingleOrDefault().Id;

            var posts = await _postApiServices.GetPosts(perfilid);
     //       if (posts is null)
     //           return RedirectToAction(nameof(Create));
            return View(posts);
        }

        // GET: PostController/Details/5
        /*    public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                return View();
            } */

        // GET: PostController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
     //   [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostCreateViewModel post, IFormFile multimedia)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (multimedia is null)
                        post.Multimedia = null;
                    else
                        post.Multimedia = _postFileUploader.UploadFile(multimedia, Guid.NewGuid().ToString());

                    post.PerfilId = (await _perfilApiService.GetPerfilFromLogin(this.User.Identity.Name))
                        .SingleOrDefault()
                        .Id;

                    await _postApiServices.CreatePost(post);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View(post);
        }

        // GET: PostController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            //Abaixo verifica se o usuário logado é dono
            //deste post. Se não for, ele vai pra index dele.
            int idUsuario = (await _perfilApiService.GetPerfilFromLogin(this.User.Identity.Name))
                        .SingleOrDefault()
                        .Id;
            var postEdit = await _postApiServices.GetPostEdit(id.Value);
            if (postEdit.PerfilId != idUsuario)
                return RedirectToAction(nameof(Index));
            // Pronto. Agora se tudo estiver certo,
            // envia o post para a view de edição.

            return View(postEdit);
        }

        // POST: PostController/Edit/5
        [HttpPost]
    //    [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, PostEditViewModel post, IFormFile myfile)
        {
            if (id != post.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (myfile is null)
                        post.Multimedia = (await _postApiServices.GetPostEdit(id)).Multimedia;
                    else
                        post.Multimedia = _postFileUploader.UploadFile(myfile, Guid.NewGuid().ToString());

                    await _postApiServices.UpdatePost(post, id);
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: PostController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            await _postApiServices.DeletePost(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: PostController/Delete/5
        /*
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
        } */
        [AllowAnonymous]
        public async Task<ActionResult> Timeline()
        {
            var postsTimeline = await _postApiServices.GetTimelinePosts();
            return View(postsTimeline);
        }
    }
}
