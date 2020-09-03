using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAll();
        Task<Post> GetById(int id);
        Task Create(Post post);
        Task Update(Post post);
        Task Delete(int id, Post post = null);
    }
}
