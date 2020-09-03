using RedeHortaRayanRalile.Models.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeHortaRayanRalile.Models.Perfis
{
    public class PerfilEditViewModel
    {
        public int Id { get; set; }
        public string UsuarioLogin { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = ValidationConstants.StringLengthErrorMessage)]
        public string Nome { get; set; }

        [Required]
        public string Foto { get; set; }

        [DisplayName("Sua biografia para o perfil:")]
        [StringLength(1000, ErrorMessage = ValidationConstants.StringLengthOnlyMaxErrorMSG)]
        public string Biografia { get; set; }

        [DisplayName("Tipo de Horta (ex: Horta de Apto)")]
        [StringLength(50, ErrorMessage = ValidationConstants.StringLengthOnlyMaxErrorMSG)]
        public string TipoHorta { get; set; }

        [DisplayName("Seus interesses em plantas (ex: Rosas Jasmine e Margaridas)")]
        [StringLength(50, ErrorMessage = ValidationConstants.StringLengthOnlyMaxErrorMSG)]
        public string Interesses { get; set; }

    }
}
