using System;
using System.IO;
using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

using Microsoft.Azure;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace BlobConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Config Builder
            var build = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            
            //Set config
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();

            //Connect to Azure service
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(config["connectionstring"]);

            //Create new blob client
            CloudBlobClient clientBlob = storageAccount.CreateCloudBlobClient();

            //Select container, check and set permissions
            CloudBlobContainer container = clientBlob.GetContainerReference("contenedorplatzi");
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob});

            //Set filename in container
            CloudBlockBlob myBlob = container.GetBlockBlobReference("azureLogo.jpg");

            //Read file with IO method
            using (var fileStream = System.IO.File.OpenRead(@"./images/AzureLogo.jpg"))
            {
                //Upload file to container
                myBlob.UploadFromStream(fileStream);
            }

            Console.WriteLine("Upload File: OK");
        }
    }
}
