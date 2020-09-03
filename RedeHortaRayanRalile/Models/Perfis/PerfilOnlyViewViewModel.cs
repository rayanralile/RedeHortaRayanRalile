using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeHortaRayanRalile.Models.Perfis
{
    public class PerfilOnlyViewViewModel
    {
        public string UsuarioLogin { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public string Biografia { get; set; }

        [DisplayName("Tipo de Horta")]
        public string TipoHorta { get; set; }
        public string Interesses { get; set; }
    }
}
