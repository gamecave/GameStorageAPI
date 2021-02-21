using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Logging;

namespace GameStorageAPI.Services
{
    public class GameFileService
    {

        private StorageClient storage;
        private readonly string BUCKET_NAME = "gamecave-files";

        public GameFileService()
        {
            storage = StorageClient.Create();
        }

        public async Task SaveFile(IFormFile file)
        {
            using (var f = file.OpenReadStream())
            {
                storage.UploadObject(BUCKET_NAME, file.FileName, null, f);
            }
        }

        public async Task<Stream> GetFile(string path)
        {
            var ret = new MemoryStream();
            // TODO: need the ret object to get the stream of the file
            storage.DownloadObject(BUCKET_NAME, path, ret);
            return ret;
        }
    }
}
