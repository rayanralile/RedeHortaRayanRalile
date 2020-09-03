using Newtonsoft.Json;
using RedeHortaRayanRalile.ApiServices.Interfaces;
using RedeHortaRayanRalile.ApiServices.Models.Perfil;
using RedeHortaRayanRalile.Models.Perfis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RedeHortaRayanRalile.ApiServices.Implementations
{
    public class PerfilApiServiceOFFLINE : IPerfilApiService
    {
        public HttpClient client;
        public PerfilApiServiceOFFLINE()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001");
        }
        public async Task<IEnumerable<PerfilIndexViewModel>> GetPerfilFromLogin(string login)
        {
            var response = await client.GetAsync($"api/perfis?usuariologin={login}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var perfilResponseList = JsonConvert.DeserializeObject<List<PerfilResponseViewModel>>(responseString);
            if (perfilResponseList == null)
                return null;
            List<PerfilIndexViewModel> perfilList = new List<PerfilIndexViewModel>();
            foreach (var item in perfilResponseList)
            {
                perfilList.Add(new PerfilIndexViewModel
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

            return perfilList.AsEnumerable();
        }
        public async Task<IEnumerable<PerfilListAllViewModel>> GetAllPerfis()
        {
            var response = await client.GetAsync("api/perfis");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var perfilJson = JsonConvert.DeserializeObject<List<PerfilResponseViewModel>>(responseString);
            List<PerfilListAllViewModel> perfilLista = new List<PerfilListAllViewModel>();
            foreach (var item in perfilJson)
            {
                perfilLista.Add(new PerfilListAllViewModel
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    UsuarioLogin = item.UsuarioLogin,
                    TipoHorta = item.TipoHorta,
                    Interesses = item.Interesses,
                    Foto = item.Foto
                });
            }

            return perfilLista.AsEnumerable();
        }
        public async Task<PerfilOnlyViewViewModel> GetOnlyViewPerfil(int id)
        {
            var response = await client.GetAsync($"api/perfis/{id}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var perfilJson = JsonConvert.DeserializeObject<PerfilResponseViewModel>(responseString);
            PerfilOnlyViewViewModel perfil = new PerfilOnlyViewViewModel
            {
                Nome = perfilJson.Nome,
                Interesses = perfilJson.Interesses,
                TipoHorta = perfilJson.TipoHorta,
                UsuarioLogin = perfilJson.UsuarioLogin,
                Biografia = perfilJson.Biografia,
                Foto = perfilJson.Foto
            };

            return perfil;
        }

        public async Task CreatePerfil(PerfilCreateViewModel perfil, string login)
        {
            var perfilRequest = new PerfilRequestViewModel
            {
                UsuarioLogin = login,
                Nome = perfil.Nome,
                Foto = perfil.Foto,
                Biografia = perfil.Biografia,
                TipoHorta = perfil.TipoHorta,
                Interesses = perfil.Interesses
            };

            var perfilSerialized = JsonConvert.SerializeObject(perfilRequest);
            var contentPerfil = new StringContent(perfilSerialized, Encoding.UTF8, "application/json");
            await client.PostAsync("api/perfis", contentPerfil);
        }

        public async Task<PerfilEditViewModel> GetPerfilEdit(int id)
        {
            var response = await client.GetAsync($"api/perfis/{id}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var perfilResponse = JsonConvert.DeserializeObject<PerfilResponseViewModel>(responseString);

            var perfil = new PerfilEditViewModel
            {
                Id = perfilResponse.Id,
                Nome = perfilResponse.Nome,
                Biografia = perfilResponse.Biografia,
                Foto = perfilResponse.Foto,
                Interesses = perfilResponse.Interesses,
                TipoHorta = perfilResponse.TipoHorta,
                UsuarioLogin = perfilResponse.UsuarioLogin
            };

            return perfil;
        }
        public async Task UpdatePerfil(PerfilEditViewModel perfil, int id)
        {
            PerfilRequestViewModel request = new PerfilRequestViewModel
            {
                Nome = perfil.Nome,
                Foto = perfil.Foto,
                Biografia = perfil.Biografia,
                UsuarioLogin = perfil.UsuarioLogin,
                TipoHorta = perfil.TipoHorta,
                Interesses = perfil.Interesses
            };

            var perfilSerialized = JsonConvert.SerializeObject(request);
            var content = new StringContent(perfilSerialized, Encoding.UTF8, "application/json");
            await client.PutAsync($"api/perfis/{id}", content);
        }
        public async Task DeletarPerfil(string username)
        {
            var response = await client.GetAsync($"api/perfis?usuariologin={username}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var perfilResponseList = JsonConvert.DeserializeObject<List<PerfilResponseViewModel>>(responseString);
            if (perfilResponseList == null || !perfilResponseList.Any())
                return;
            int idDel = perfilResponseList.SingleOrDefault().Id;
            await client.DeleteAsync($"api/perfis/{idDel}");
        }
    }
}
