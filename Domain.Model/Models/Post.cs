using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Multimedia { get; set; }
        public DateTime DataCriacao { get; set; }
        public int PerfilId { get; set; }
        public Perfil Perfil { get; set; }
    }
}
