using Azure.Storage.Blobs;
using System.Drawing;
using System.Reflection.Metadata;
using System;
using System.Text;
using Azure.Storage.Blobs.Models;
using Azure.Storage;
using Microsoft.AspNetCore.Components.Forms;

namespace FileUpload_Samples.Services
{
    public class AzureBlobService
    {
        public async Task UploadAsync(string sasUrl, string fileName, Stream content, Action<long> progressAction)
        {
            BlobContainerClient _container = new BlobContainerClient(new Uri(sasUrl));
            BlobClient blob = _container.GetBlobClient(fileName);

            var blobName = $"{Guid.NewGuid()}/{fileName}";
            await blob.UploadAsync(content, new BlobUploadOptions
            {
                // HttpHeaders = new BlobHttpHeaders { ContentType = inputFile.ContentType },
                TransferOptions = new StorageTransferOptions
                {
                    InitialTransferSize = 1024 * 1024,
                    MaximumTransferSize = 1024 * 1024,
                    MaximumConcurrency = 1
                },
                ProgressHandler = new Progress<long>((progress) =>
                {
                    progressAction(progress);
                    //progressBar = (100.0 * progress / content.Length).ToString("0");

                })

            }); ;
        }

       // Interestingly it doesn't seem to be a problem if the source isn't from a file.
       // I can use UploadFromStreamAsync to copy from a stream from another large blob opened with OpenReadAsync and memory usage stays well below the overall blob size.
    }
}
