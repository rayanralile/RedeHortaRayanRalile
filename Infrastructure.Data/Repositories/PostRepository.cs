using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly HortaContext _hortaContext;
        public PostRepository(HortaContext hortaContext)
        {
            _hortaContext = hortaContext;
        }
        public async Task<IEnumerable<Post>> GetAll()
        {
            var posts = _hortaContext
                .Posts
                .Include(l => l.Perfil).ToListAsync();
            return await posts;
        }

        public async Task<Post> GetById(int id)
        {
            return await _hortaContext.Posts
                .Include(l => l.Perfil)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Create(Post post)
        {
            _hortaContext.Add(post);
            await _hortaContext.SaveChangesAsync();
        }

        public async Task Update(Post post)
        {
            _hortaContext.Entry(post).State = EntityState.Modified;
            _hortaContext.Update(post);
            await _hortaContext.SaveChangesAsync();
        }

        public async Task Delete(int id, Post post = null)
        {
            if(post is null)
                post = await GetById(id);

            _hortaContext.Remove(post);
            await _hortaContext.SaveChangesAsync();
        }
    }
}
