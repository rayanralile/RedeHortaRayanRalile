using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RedeHortaRayanRalile.ApiServices.Models.Post
{
    public class PostRequestViewModel
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Multimedia { get; set; }
        public DateTime DataCriacao { get; set; }
        public int PerfilId { get; set; }
        [JsonIgnore]
        public Domain.Model.Models.Perfil Perfil { get; set; }

    }
}
