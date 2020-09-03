using RedeHortaRayanRalile.Models.Perfis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeHortaRayanRalile.ApiServices.Interfaces
{
    public interface IPerfilApiService
    {
        Task CreatePerfil(PerfilCreateViewModel perfil, string login);
        Task<IEnumerable<PerfilListAllViewModel>> GetAllPerfis();
        Task<PerfilOnlyViewViewModel> GetOnlyViewPerfil(int id);
        Task<PerfilEditViewModel> GetPerfilEdit(int id);
        Task<IEnumerable<PerfilIndexViewModel>> GetPerfilFromLogin(string login);
        Task UpdatePerfil(PerfilEditViewModel perfil, int id);
        Task DeletarPerfil(string username);
    }
}
