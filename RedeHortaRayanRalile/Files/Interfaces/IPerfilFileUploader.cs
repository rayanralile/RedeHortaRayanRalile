using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeHortaRayanRalile.Files.Interfaces
{
    public interface IPerfilFileUploader
    {
        string UploadFile(IFormFile file, string fileName);
    }
}
