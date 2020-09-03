using Infrastructure.Crosscutting.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using RedeHortaRayanRalile.Files.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeHortaRayanRalile.Files.Implementations
{
    public class PostFileUploader : IPostFileUploader
    {
        public string UploadFile(IFormFile file, string fileName)
        {
            fileName += System.IO.Path.GetExtension(file.FileName);
            var reader = file.OpenReadStream();
            string connectionString = GlobalConstants.blobStorage;
            var cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("post-multimedia");
            container.CreateIfNotExists();
            var blob = container.GetBlockBlobReference(fileName);
            blob.UploadFromStream(reader);
            var destinoDaImagemNaNuvem = blob.Uri.ToString();
            return destinoDaImagemNaNuvem;
        }
        public int TipoMidia(string filename)
        {
            string mediaType = System.IO.Path.GetExtension(filename);
            switch (mediaType)
            {
                case ".jpeg":
                case ".jpg":
                case ".png":
                case ".gif":
                case ".bmp":
                case ".webp":
                    return 0; break;
                case ".mp4":
                    return 1; break;
                case ".mp3":
                    return 2; break;
                default:
                    return 100; break;
            }
        }

    }
}
