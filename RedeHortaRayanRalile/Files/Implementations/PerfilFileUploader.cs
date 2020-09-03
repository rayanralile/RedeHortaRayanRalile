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
    public class PerfilFileUploader : IPerfilFileUploader
    {
        public string UploadFile(IFormFile file, string fileName)
        {
            fileName += System.IO.Path.GetExtension(file.FileName);
            string connectionString = GlobalConstants.blobStorage;
            var cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            var reader = file.OpenReadStream();
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("profile-images");
            container.CreateIfNotExists();
            var blob = container.GetBlockBlobReference(fileName);
            blob.UploadFromStream(reader);
            var destinoDaImagemNaNuvem = blob.Uri.ToString();
            return destinoDaImagemNaNuvem;
        }
    }
}
