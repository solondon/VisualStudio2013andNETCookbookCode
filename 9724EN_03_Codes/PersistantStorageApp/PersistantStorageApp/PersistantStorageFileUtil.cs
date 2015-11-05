
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PersistantStorageApp
{
    public class PersistantStorageFileUtil
    {
        public void SaveToStore(string fileName, string data)
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            using (StreamWriter sw = new StreamWriter(new IsolatedStorageFileStream(fileName, FileMode.Append, store)))
            {
                sw.WriteLine(data); 
                sw.Close();
            }
        }
        public void DeleteFromStore(string fileName)
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            if (!store.FileExists(fileName))
            {
                store.DeleteFile(fileName);
            }
        }
        public string ReadFromStore(string fileName)
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            if (!store.FileExists(fileName))
            {
                IsolatedStorageFileStream dataFile = store.CreateFile(fileName);
                dataFile.Close();
            }

            using (StreamReader reader = new StreamReader(new IsolatedStorageFileStream(fileName, FileMode.Open, store)))
            {
                string rawData = reader.ReadToEnd();
                reader.Close();

                return rawData;
            }
        }
        public string EncryptData(string data)
        {
            byte[] databytes = Encoding.UTF8.GetBytes(data);
            byte[] protecteddatabytes = ProtectedData.Protect(databytes, null);

            string protectedData = Encoding.UTF8.GetString(protecteddatabytes, 0, protecteddatabytes.Length);

            return protectedData;
        }

        public string DecryptData(string protectedData)
        {
            byte[] protecteddatabytes = Encoding.UTF8.GetBytes(protectedData);
            byte[] databytes = ProtectedData.Unprotect(protecteddatabytes, null);

            string data = Encoding.UTF8.GetString(databytes, 0, databytes.Length);

            return data;
        }

        public string EncryptSerializeData(object target)
        {
            var serializer = new DataContractJsonSerializer(target.GetType());
            MemoryStream memStream = new MemoryStream();
            serializer.WriteObject(memStream, target);
            string jsondata = Encoding.UTF8.GetString(memStream.GetBuffer(), 0, (int)memStream.Length);

            return this.EncryptData(jsondata);
        }
        
    }
}
