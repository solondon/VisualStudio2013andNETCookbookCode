using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFCommunicationLibrary
{
    [ServiceContract]
    public interface IFileTransferLibrary
    {

        [OperationContract()]
        void UploadFile(ResponseFile request);

        [OperationContract]
        ResponseFile DownloadFile(RequestFile request);

    }

    [MessageContract]
    public class ResponseFile : IDisposable
    {
        [MessageHeader]
        public string FileName;

        [MessageHeader]
        public long Length;

        [MessageBodyMember]
        public System.IO.Stream FileByteStream;

        [MessageHeader]
        public long byteStart = 0;

        public void Dispose()
        {
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }
    }

    [MessageContract]
    public class RequestFile
    {
        [MessageBodyMember]
        public string FileName;

        [MessageBodyMember]
        public long byteStart = 0;
    }
}
