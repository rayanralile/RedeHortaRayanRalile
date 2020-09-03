using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedeHortaRayanRalile.ApiServices.Models.Perfil
{
    public class PerfilRequestViewModel
    {
        public string UsuarioLogin { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public string Biografia { get; set; }
        public string TipoHorta { get; set; }
        public string Interesses { get; set; }
    }
}
