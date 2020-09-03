using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeHortaRayanRalile.Models.Post
{
    public class PostCreateViewModel
    {
        [DisplayName("Título")]
        [MaxLength(100)]
        [Required]
        public string Titulo { get; set; }
        
        [DisplayName("Conteúdo")]
        [MaxLength(3000)]
        [Required]
        public string Descricao { get; set; }
        
        [DisplayName("Inserir foto, áudio (mp3) ou vídeo - opcional")]
        public string Multimedia { get; set; }
        //Perfilid deve ir escondido na model (campo hidden)
        public int PerfilId { get; set; }
    }
}
