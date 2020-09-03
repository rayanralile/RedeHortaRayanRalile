using System;
using System.Text.Json.Serialization;

namespace Application.Api.Models.Post
{
    public class PostResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Multimedia { get; set; }
        public DateTime DataCriacao { get; set; }
        public int PerfilId { get; set; }
        [JsonIgnore]
        public Domain.Model.Models.Perfil Perfil { get; set; }
    }
}
