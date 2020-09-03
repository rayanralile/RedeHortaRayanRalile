using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Services
{
    public interface IPerfilService
    {
        Task<IEnumerable<Perfil>> GetAll();
        Task<Perfil> GetById(int id);
        Task Create(Perfil perfil);
        Task Update(Perfil perfil);
        Task Delete(int id);
    }
}
