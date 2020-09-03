using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Api.Models.Perfil
{
    public class PerfilRequest
    {
        public string UsuarioLogin { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public string Biografia { get; set; }
        public string TipoHorta { get; set; }
        public string Interesses { get; set; }
    }
}
