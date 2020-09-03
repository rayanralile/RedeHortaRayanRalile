using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeHortaRayanRalile.Files.Interfaces
{
    public interface IPostFileUploader
    {
        string UploadFile(IFormFile file, string fileName);
        /// <summary>
        /// 0 -> imagem
        /// 1 -> vídeo
        /// 2 -> áudio
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        int TipoMidia(string filename);
    }
}
