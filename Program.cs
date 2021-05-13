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

            //Get connection string
            string getConnString = config["connectionstring"];
            Console.WriteLine(getConnString);

            // CloudStorageAccount StorageAccount = CloudStorageAccount.Parse();
        }
    }
}
