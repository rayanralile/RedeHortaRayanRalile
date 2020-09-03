using RedeHortaRayanRalile.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeHortaRayanRalile.ApiServices.Interfaces
{
    public interface IPostApiServices
    {
        Task CreatePost(PostCreateViewModel post);
        Task<IEnumerable<PostIndexViewModel>> GetPosts(int perfilid);
        Task<IEnumerable<PostTimelineViewModel>> GetTimelinePosts();
        Task<PostEditViewModel> GetPostEdit(int id);
        Task UpdatePost(PostEditViewModel perfil, int id);
        Task DeletePost(int id);
    }
}
