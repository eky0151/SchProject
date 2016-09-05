using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessor;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace TechSharedLibraries
{
    
    public static class AzureBlobUploader
    {
        /// <summary>
        /// Upload image to Azure storage in 4 different size
        /// <param name="filePath"> image path eg.: C:\Users\Potter\prabhas-profile512.jpg </param>
        /// <seealso cref="System.String"/>
        /// <returns>Return uploaded image name and "extension"</returns>
        /// </summary>
        public static string UploadImage(string filePath)
        {
            string fileName = Guid.NewGuid().ToString("N").Substring(0, 12);

            ImageFactory factory = new ImageFactory();
            factory.Load(filePath);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("images");
            CloudBlockBlob originalBlob = container.GetBlockBlobReference($"original/{fileName}.png");

            using (var fileStream = System.IO.File.OpenRead(filePath))
            {
                originalBlob.UploadFromStream(fileStream);
            }
            CloudBlockBlob mediumBlob = container.GetBlockBlobReference($"512/{fileName}.png");
            using (var fileStream = new MemoryStream())
            {
                factory.Resize(new Size(512, 512)).Save(fileStream);
                mediumBlob.UploadFromStream(fileStream);
            }
            CloudBlockBlob smallBlob = container.GetBlockBlobReference($"64/{fileName}.png");
            using (var fileStream = new MemoryStream())
            {
                factory.Resize(new Size(64, 64)).Save(fileStream);
                smallBlob.UploadFromStream(fileStream);
            }
            CloudBlockBlob extraSmallBlob = container.GetBlockBlobReference($"32/{fileName}.png");
            using (var fileStream = new MemoryStream())
            {
                factory.Resize(new Size(32, 32)).Save(fileStream);
                extraSmallBlob.UploadFromStream(fileStream);
            }

            return fileName + ".png";
        }
    }


}
