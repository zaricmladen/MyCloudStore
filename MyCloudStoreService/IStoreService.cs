using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyCloudStoreService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStoreService" in both code and config file together.
    [ServiceContract]
    public interface IStoreService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        bool authenthicateUser(string userName, string password);

        [OperationContract]
        bool storeFile(string fileName,string hashValue);

        [OperationContract]
        void getChunks(string hashValue,byte[] chunk, int nr);

        [OperationContract]
        void downloadFile(string fileName, string hashValue);

        [OperationContract]
        byte[] downloadFileChunk(string hashValue);

        [OperationContract]
        IList<string> filesNames(string hashValue);

        [OperationContract]
        double[] checkAvailableSpace(string userName, double size);

        [OperationContract]
        void incUsedSpace(string userName, double value);

        [OperationContract]
        void decUsedSpace(string userName, double value);

        [OperationContract]
        void deleteFile(string fileName, string hashValue);



    }

    [DataContract]
    [Serializable]
    public class Chunk
    {
        [DataMember]
        public byte[] chunkContent { get; set; }

        [DataMember]
        public int number { get; set; }
    
        public Chunk(byte[] con, int nr)
        {
            chunkContent = con;
            number = nr;
        }
    }

}
