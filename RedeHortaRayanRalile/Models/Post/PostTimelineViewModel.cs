using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeHortaRayanRalile.Models.Post
{
    public class PostTimelineViewModel
    {
        //Abaixo o nome do criador do Post
        [DisplayName("Autor")]
        public string NomeAutor { get; set; }
        [DisplayName("Criado em")]
        public DateTime DataCriacao { get; set; }

        [DisplayName("Título")]
        public string Titulo { get; set; }

        [DisplayName("Conteúdo")]
        public string Descricao { get; set; }

        public string Multimedia { get; set; }

        //Item abaixo deve ser preenchido no service, a fim de ser usado
        //no razor para determinar o tipo de tratamento por tag
        //0 - foto / 1 - vídeo / 2 - áudio
        public int TipoMidia { get; set; }
        //Abaixo perfil id, ir como hidden
        //Haverá link para visitar perfil. Usar id abaixo para fazer a url
        public int PerfilId { get; set; }
    }
}
