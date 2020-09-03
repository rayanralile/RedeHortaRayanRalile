using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Services
{
    public class PerfilService : IPerfilService
    {

        private readonly IPerfilRepository _perfilRepository;

        //Lembrar de registrar dependência no Startup.cs
        public PerfilService(
            IPerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }

        public async Task Create(Perfil perfil)
        {
            await _perfilRepository.Create(perfil);
        }

        public async Task Delete(int id)
        {
            await _perfilRepository.Delete(id);
        }

        public async Task<IEnumerable<Perfil>> GetAll()
        {
            return await _perfilRepository.GetAll();
        }

        public async Task<Perfil> GetById(int id)
        {
            return await _perfilRepository.GetById(id);
        }

        public async Task Update(Perfil perfil)
        {
            await _perfilRepository.Update(perfil);
        }
    }
}
