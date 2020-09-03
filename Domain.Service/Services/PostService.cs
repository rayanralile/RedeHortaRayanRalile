using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPerfilRepository _perfilRepository;
        public PostService(IPostRepository postRepository,
            IPerfilRepository perfilRepository)
        {
            _postRepository = postRepository;
            _perfilRepository = perfilRepository;
        }

        public async Task Create(Post post)
        {
            if (post.Perfil is null)
                post.Perfil = await _perfilRepository.GetById(post.PerfilId);

            post.DataCriacao = DateTime.Now;

            await _postRepository.Create(post);
        }

        public async Task Delete(int id, Post post = null)
        {
            await _postRepository.Delete(id, post);
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            var todosPosts = await _postRepository.GetAll();
            todosPosts = todosPosts.OrderByDescending(x => x.DataCriacao);
            return todosPosts;
        }

        public async Task<Post> GetById(int id)
        {
            return await _postRepository.GetById(id);
        }

        public async Task Update(Post post)
        {
            if(post.Perfil is null)
                post.Perfil = await _perfilRepository.GetById(post.PerfilId);
       //     post.DataCriacao = (await _postRepository.GetById(post.Id)).DataCriacao;
            await _postRepository.Update(post);
        }
    }
}
