using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MyCloudStoreService
{
    public class Data
    {
        public List<KeyValuePair<string, Chunk>> list = new List<KeyValuePair<string, Chunk>>();
        private static object locker = true;
        private static Data instance = null;

        public static Data Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                        instance = new Data();
                }
                return instance;
            }
        }

        protected Data()
        {

        }
    }
}