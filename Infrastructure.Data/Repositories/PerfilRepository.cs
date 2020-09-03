using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly HortaContext _hortaContext;

        public PerfilRepository(HortaContext hortaContext)
        {
            _hortaContext = hortaContext;
        }

        public async Task Create(Perfil perfil)
        {
            _hortaContext.Add(perfil);
            await _hortaContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var perfil = await GetById(id);
            _hortaContext.Remove(perfil);
            await _hortaContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Perfil>> GetAll()
        {
            return await _hortaContext.Perfis.ToListAsync();
        }

        public async Task<Perfil> GetById(int id)
        {
            return await _hortaContext.Perfis.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Perfil perfil)
        {
            _hortaContext.Entry(perfil).State = EntityState.Modified;
            _hortaContext.Update(perfil);
            await _hortaContext.SaveChangesAsync();
        }
    }
}
