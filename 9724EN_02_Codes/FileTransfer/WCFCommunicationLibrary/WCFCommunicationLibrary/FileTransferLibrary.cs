using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;

namespace WCFCommunicationLibrary
{
    public class FileTransferLibrary : IFileTransferLibrary
    {
        public ResponseFile DownloadFile(RequestFile request)
        {
            ResponseFile result = new ResponseFile();

            FileStream stream = this.GetFileStream(Path.GetFullPath(request.FileName));
            stream.Seek(request.byteStart, SeekOrigin.Begin);
            result.FileName = request.FileName;
            result.Length = stream.Length;
            result.FileByteStream = stream;
            return result;

        }
        private FileStream GetFileStream(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
                throw new FileNotFoundException("File not found");

            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }
        public void UploadFile(ResponseFile request)
        {
           
            string filePath = Path.GetFullPath(request.FileName);
            
            int chunkSize = 2048;
            byte[] buffer = new byte[chunkSize];

            using (FileStream stream = new FileStream(filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write))
            {
                do
                {
                    int readbyte = request.FileByteStream.Read(buffer, 0, chunkSize);
                    if (readbyte == 0) break;

                    stream.Write(buffer, 0, readbyte);
                } while (true);
                stream.Close();
            }
        }
   
    }
}
