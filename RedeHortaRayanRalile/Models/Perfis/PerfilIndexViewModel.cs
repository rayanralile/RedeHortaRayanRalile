using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeHortaRayanRalile.Models.Perfis
{
    public class PerfilIndexViewModel
    {
        public int Id { get; set; }

        [DisplayName("Seu login na Rede Social.:")]
        public string UsuarioLogin { get; set; }

        public string Nome { get; set; }
        public string Foto { get; set; }
        public string Biografia { get; set; }

        [DisplayName("Tipo de Horta")]
        public string TipoHorta { get; set; }

        [DisplayName("Seus interesses em plantas")]
        public string Interesses { get; set; }
    }
}
