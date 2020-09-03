using Newtonsoft.Json;
using RedeHortaRayanRalile.ApiServices.Interfaces;
using RedeHortaRayanRalile.ApiServices.Models.Post;
using RedeHortaRayanRalile.Files.Interfaces;
using RedeHortaRayanRalile.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RedeHortaRayanRalile.ApiServices.Implementations
{
    public class PostApiService : IPostApiServices
    {
        public HttpClient client;
        private readonly IPostFileUploader _postFileUploader;
        private readonly IPerfilApiService _perfilApiService;
        public PostApiService(IPostFileUploader postFileUploader,
            IPerfilApiService perfilApiService)
        {
            _postFileUploader = postFileUploader;
            _perfilApiService = perfilApiService;
            client = new HttpClient();
            client.BaseAddress = new Uri("https://rayan-ralile-pb-horta-api.azurewebsites.net");
        }
        public async Task CreatePost(PostCreateViewModel post)
        {
            var postRequest = new PostRequestViewModel
            {
                Titulo = post.Titulo,
                Descricao = post.Descricao,
                Multimedia = post.Multimedia,
                PerfilId = post.PerfilId
            };
            var postSerialized = JsonConvert.SerializeObject(postRequest);
            var contentPost = new StringContent(postSerialized, Encoding.UTF8, "application/json");
            await client.PostAsync("api/post", contentPost);
        }

        public async Task<PostEditViewModel> GetPostEdit(int id)
        {
            var response = await client.GetAsync($"api/post/{id}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var postResponse = JsonConvert.DeserializeObject<PostResponseViewModel>(responseString);

            var post = new PostEditViewModel 
            {
                Id = postResponse.Id,
                Descricao = postResponse.Descricao,
                PerfilId = postResponse.PerfilId,
                Multimedia = postResponse.Multimedia,
                Titulo = postResponse.Titulo,
                DataCriacao = postResponse.DataCriacao
            };
            return post;
        }

        public async Task<IEnumerable<PostIndexViewModel>> GetPosts(int perfilid)
        {
            var response = await client.GetAsync($"api/post?perfilid={perfilid}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var postResponseList = JsonConvert.DeserializeObject<List<PostResponseViewModel>>(responseString);
            if (postResponseList == null)
                return null;
            var postList = new List<PostIndexViewModel>();
            foreach (var item in postResponseList)
            {
                postList.Add(new PostIndexViewModel 
                {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Multimedia = item.Multimedia,
                    TipoMidia = _postFileUploader.TipoMidia(item.Multimedia),
                    Titulo = item.Titulo,
                    DataCriacao = item.DataCriacao
                });;
            }
            return postList.AsEnumerable();
        }

        public async Task<IEnumerable<PostTimelineViewModel>> GetTimelinePosts()
        {
            var response = await client.GetAsync($"api/post");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var postResponseList = JsonConvert.DeserializeObject<List<PostResponseViewModel>>(responseString);
            if (postResponseList == null)
                return null;

            var postList = new List<PostTimelineViewModel>();
            foreach (var item in postResponseList)
            {
                var objetoAutor = await _perfilApiService.GetPerfilEdit(item.PerfilId);
                string nomeAutor = objetoAutor.Nome;
                postList.Add(new PostTimelineViewModel 
                {
                    Titulo = item.Titulo,
                    Descricao = item.Descricao,
                    Multimedia = item.Multimedia,
                    NomeAutor = nomeAutor,
                    PerfilId = item.PerfilId,
                    TipoMidia = _postFileUploader.TipoMidia(item.Multimedia),
                    DataCriacao = item.DataCriacao
                });
            }
            return postList.AsEnumerable();
        }

        public async Task UpdatePost(PostEditViewModel perfil, int id)
        {
            var postRequest = new PostRequestViewModel 
            {
                Descricao = perfil.Descricao,
                Multimedia = perfil.Multimedia,
                PerfilId = perfil.PerfilId,
                Titulo = perfil.Titulo,
                DataCriacao = perfil.DataCriacao
            };
            var postSerialized = JsonConvert.SerializeObject(postRequest);
            var content = new StringContent(postSerialized, Encoding.UTF8, "application/json");
            await client.PutAsync($"api/post/{id}", content);
        }
        public async Task DeletePost(int id)
        {
            await client.DeleteAsync("api/post/" + id);
        }
    }
}
