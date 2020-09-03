using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Crosscutting.IoC
{
    public static class GlobalConstants
    {
        public static readonly bool isOnline = false;
        public static readonly string blobStorage = "COLOQUE A CONNECTION STRING DA SUA STORAGE DO AZURE (BLOB) AQUI";
    }
}
