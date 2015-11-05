using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWorkerRole
{
    public class BlobExample
    {
        private CloudBlobContainer cloudBlobContainer;
        int pageSize = 512;
        int uploadSize = 1048576;
        public BlobExample(string containerName)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
        }

        private void CreateBlobs()
        {
            cloudBlobContainer.CreateIfNotExists();
            CreateBlob("Abhishek/FirstBlob");
            CreateBlob("Abhishek/SecondBlob");
        }
        private void TraverseDirectoryTree(String directoryName)
        {
            CloudBlobDirectory cloudBlobDirectory = cloudBlobContainer.GetDirectoryReference(directoryName);
            IEnumerable<IListBlobItem> blobItems = cloudBlobDirectory.ListBlobs();
            foreach (CloudBlobDirectory cloudBlobDirectoryItem in blobItems.OfType<CloudBlobDirectory>())
            {
                Uri uri = cloudBlobDirectoryItem.Uri;
                Trace.WriteLine(uri.ToString());
                IEnumerable<CloudBlockBlob> leafBlobs = cloudBlobDirectoryItem.ListBlobs().OfType<CloudBlockBlob>();
                foreach (CloudBlockBlob leafBlockBlob in leafBlobs)
                {
                    Uri leafUri = leafBlockBlob.Uri;
                    Trace.WriteLine(leafUri.ToString());
                }
            }
        }
        private void CreateBlob(String blobName)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(blobName);
                writer.Flush();

                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);

                //Write data to the stream and upload it to the storage blob
                cloudBlockBlob.UploadFromStream(stream);

                writer.Dispose();
            }
            this.TakeSnapshot(blobName);
        }
        private void DownloadBlob(string blobName, string fileName)
        {
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);
            FileStream fileStream = new FileStream(fileName,FileMode.Append);
            IAsyncResult iAsyncResult = cloudBlockBlob.BeginDownloadToStream(fileStream,
            (result) =>
            {
                cloudBlockBlob.EndDownloadToStream(result);
                fileStream.Close();
            }, null);
        }
        private DateTimeOffset TakeSnapshot(string blobName)
        {
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);
            CloudBlockBlob snapshot = cloudBlockBlob.CreateSnapshot(); //Backs up the blob
            return (DateTimeOffset)snapshot.SnapshotTime;
        }


        private void UploadBigData(string blobName, string filePath)
        {


            CloudPageBlob cloudPageBlob = cloudBlobContainer.GetPageBlobReference(blobName);
            cloudPageBlob.Properties.ContentType = "binary/octet-stream";

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                Int32 blobSize = (Int32)fileStream.Length;
                if ((blobSize % pageSize) != 0)
                {
                    throw new ApplicationException(
                    "Page blob size must be a multiple of page size");
                }
                cloudPageBlob.Create(blobSize);
                Int32 pageBlobOffset = 0;
                Int32 numberIterations = blobSize / uploadSize;
                for (Int32 i = 0; i < numberIterations; i++)
                {
                    pageBlobOffset = UploadPages(fileStream, cloudPageBlob, pageBlobOffset);
                }
                pageBlobOffset = UploadFooter(fileStream, cloudPageBlob, pageBlobOffset);
            }
        }
        private Int32 UploadPages(FileStream fileStream, CloudPageBlob cloudPageBlob, Int32 pageBlobOffset)
        {
            Byte[] buffer = new Byte[uploadSize];
            Int32 countBytesRead = fileStream.Read(
            buffer, 0, uploadSize);
            Int32 countBytesUploaded = 0;
            Int32 bufferOffset = 0;
            Int32 rangeStart = 0;
            Int32 rangeSize = 0;
            while (bufferOffset < uploadSize)
            {
                Boolean nextPageIsLast =
                bufferOffset + pageSize >= uploadSize;
                Boolean nextPageHasData = NextPageHasData(buffer, bufferOffset);
                if (nextPageHasData)
                {
                    if (rangeSize == 0)
                    {
                        rangeStart = bufferOffset;
                    }
                    rangeSize += pageSize;
                }
                if ((rangeSize > 0) && (!nextPageHasData ||
                nextPageIsLast))
                {
                    using (MemoryStream memoryStream =
                    new MemoryStream(buffer, rangeStart, rangeSize))
                    {
                        cloudPageBlob.WritePages(
                        memoryStream, pageBlobOffset + rangeStart);
                        countBytesUploaded += rangeSize;
                        rangeSize = 0;
                    }
                }
                bufferOffset += pageSize;
            }
            pageBlobOffset += uploadSize;
            return pageBlobOffset;
        }
        private Int32 UploadFooter(FileStream fileStream, CloudPageBlob cloudPageBlob, Int32 pageBlobOffset)
        {
            const Int32 numberFooterBytes = 512;
            Byte[] footerBytes = new Byte[numberFooterBytes];
            Int32 countBytesRead = fileStream.Read(
            footerBytes, 0, numberFooterBytes);
            using (MemoryStream memoryStream =
            new MemoryStream(footerBytes))
            {
                cloudPageBlob.WritePages(memoryStream, pageBlobOffset);
                pageBlobOffset += numberFooterBytes;
            }
            return pageBlobOffset;
        }
        private Boolean NextPageHasData(Byte[] buffer, Int32 bufferOffset)
        {
            for (Int32 i = bufferOffset; i < bufferOffset + pageSize; i++)
            {
                if (buffer[i] != 0x0)
                {
                    return true;
                }
            }
            return false;
        }

      
    }
}
