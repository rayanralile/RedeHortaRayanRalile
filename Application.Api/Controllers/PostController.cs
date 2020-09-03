using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Api.Models.Post;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: api/Post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostResponse>>> GetPosts([FromQuery] int? perfilid)
        {
            if (perfilid == null)
            {
                var posts = await _postService.GetAll();
                var todosPosts = new List<PostResponse>();
                foreach (var item in posts)
                {
                    todosPosts.Add(ConvertPostToPostResponse(item));
                }
                return Ok(todosPosts.AsEnumerable());
            }
            else
            {
                int perfilmyid = perfilid.Value;
                var response = await GetPostByPerfil(perfilmyid);
                return Ok(response);
            }
        }
       // Método abaixo atrelado ao action acima.
        private async Task<IEnumerable<PostResponse>> GetPostByPerfil(int perfilid)
        {
            var posts = await _postService.GetAll();
            posts = posts.Where(x => x.PerfilId == perfilid);
            var todosPosts = new List<PostResponse>();
            foreach (var item in posts)
            {
                todosPosts.Add(ConvertPostToPostResponse(item));
            }
            return todosPosts.AsEnumerable();
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostResponse>> GetPost(int? id)
        {
            if (id == null)
                return NotFound();

            var post = await _postService.GetById(id.Value);

            if (post == null)
                return NotFound();

            var response = ConvertPostToPostResponse(post);
            return Ok(response);
        }

        // PUT: api/Post/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, [FromBody] PostRequest postRequest)
        {
            if(ModelState.IsValid)
            {
                var post = new Post
                {
                    Id = id,
                    Descricao = postRequest.Descricao,
                    Multimedia = postRequest.Multimedia,
                    Perfil = postRequest.Perfil,
                    PerfilId = postRequest.PerfilId,
                    Titulo = postRequest.Titulo,
                    DataCriacao = postRequest.DataCriacao
                };
                try
                {
                    await _postService.Update(post);
                    PostResponse response = ConvertPostToPostResponse(post);
                    return Ok(response);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(id))
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
        }


        // POST: api/Post
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PostResponse>> PostPost(PostRequest postRequest)
        {
            var post = new Post
            {
                Descricao = postRequest.Descricao,
                Multimedia = postRequest.Multimedia,
                Perfil = postRequest.Perfil,
                Titulo = postRequest.Titulo,
                PerfilId = postRequest.PerfilId
            };

            await _postService.Create(post);

            var postResponse = ConvertPostToPostResponse(post);

            return CreatedAtAction("GetPost", new { id = post.Id }, postResponse);
        }

        // DELETE: api/Post/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var post = await _postService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }

            await _postService.Delete(id, post);

            return Ok();
        }

        private bool PostExists(int id)
        {
            return _postService.GetById(id) != null;
        }
        private PostResponse ConvertPostToPostResponse(Post post)
        {
            return new PostResponse
            {
                Id = post.Id,
                Perfil = post.Perfil,
                PerfilId = post.PerfilId,
                Descricao = post.Descricao,
                Multimedia = post.Multimedia,
                Titulo = post.Titulo,
                DataCriacao = post.DataCriacao
            };
        }
    }
}
