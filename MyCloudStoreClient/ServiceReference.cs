using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyCloudStoreClient
{
    class ServiceReference
    {
        private static StoreServiceReference.StoreServiceClient servis = null;
        private static object objLock = new object();
        
        public static StoreServiceReference.StoreServiceClient GetServis()
        {
            if(servis == null)
            {
                lock(objLock)
                {
                    if (servis == null)
                        servis = new StoreServiceReference.StoreServiceClient();
                }
            }

            return servis;
        }
        
    }
}
