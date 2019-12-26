using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace MyCloudStoreService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StoreService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select StoreService.svc or StoreService.svc.cs at the Solution Explorer and start debugging.
    public class StoreService : IStoreService
    {
        private int blockSize = 512000;

        public bool authenthicateUser(string userName, string password)
        {
            return DBManipulation.authenticateUser(userName, password);
        }

        public void downloadFile(string fileName, string hashValue)
        {
            byte[] b = DBManipulation.downloadFile(fileName, hashValue);
            int nr = 0;
            byte[][] ret = DBManipulation.BufferSplit(b, blockSize);

            for (int i = 0; i < ret.Length; i++)
            {
                Data.Instance.list.Add(new KeyValuePair<string, Chunk>(hashValue, new MyCloudStoreService.Chunk(ret[i], nr)));
                nr++;
            }

        }

        public void DoWork()
        {
        }

        public bool storeFile(string fileName, string hashValue)
        {
            IList<KeyValuePair<string, Chunk>> nl = Data.Instance.list.Where(i => i.Key.Equals(hashValue)).ToList();
            nl.OrderBy(x => x.Value.number).ToList();
            byte[] st = new byte[blockSize];
            bool f = true;
            foreach(var element in nl)
            {

                if (f)
                {
                    Buffer.BlockCopy(element.Value.chunkContent, 0, st, 0, element.Value.chunkContent.Length);
                    f = false;
                }
                else
                    st = DBManipulation.Combine(st, element.Value.chunkContent);
            }
            if (DBManipulation.storeFile(fileName, st, hashValue))
            {
                Data.Instance.list.RemoveAll(i => i.Key.Equals(hashValue));
                return true;
            }
            else
            {
                Data.Instance.list.RemoveAll(i => i.Key.Equals(hashValue));
                return false;
            }

            
        }

        void IStoreService.getChunks(string hashValue, byte[] chunk, int nr)
        {
            Data.Instance.list.Add(new KeyValuePair<string, Chunk>(hashValue, new MyCloudStoreService.Chunk(chunk,nr)));

        }


        IList<string> IStoreService.filesNames(string hashValue)
        {
            return DBManipulation.getNameOfFiles(hashValue);
        }

        public byte[] downloadFileChunk(string hashValue)
        {
            IList<KeyValuePair<string, Chunk>> nl = Data.Instance.list.Where(i => i.Key.Equals(hashValue)).ToList();
            nl.OrderBy(x => x.Value.number).ToList();
            byte[] b = nl.ElementAt(0).Value.chunkContent;
            Data.Instance.list.Remove(nl.ElementAt(0));
            return b;
        }

        public double[] checkAvailableSpace(string userName, double size)
        {
            size = size / 1000000;
            double[] space = DBManipulation.availableSpace(userName);
            if ((size + space[0] > space[1]))
                return null;
            else
                return space;

        }

        public void incUsedSpace(string userName, double value)
        {
            DBManipulation.incData(userName, value);
        }

        public void decUsedSpace(string userName, double value)
        {
            DBManipulation.decData(userName, value);
        }

        public void deleteFile(string fileName, string hashValue)
        {
            DBManipulation.deleteFile(hashValue, fileName);
        }
    }
}
