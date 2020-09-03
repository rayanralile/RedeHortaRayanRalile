using System.Collections.Generic;

namespace Domain.Model.Models
{
    public class Perfil
    {
        public int Id { get; set; }
        public string UsuarioLogin { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public string Biografia { get; set; }
        public string TipoHorta { get; set; }
        public string Interesses { get; set; }
        public List<Post> Posts { get; set; }
    }
}
