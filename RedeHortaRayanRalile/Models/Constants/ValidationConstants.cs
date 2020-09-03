using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeHortaRayanRalile.Models.Constants
{
    public static class ValidationConstants
    {
        public const string StringLengthErrorMessage = "Campo {0} deve conter entre {2} e {1} caracteres.";
        public const string StringLengthOnlyMaxErrorMSG = "O campo {0} não é obrigatório, mas não pode conter mais do que {1} caracteres.";
    }
}
