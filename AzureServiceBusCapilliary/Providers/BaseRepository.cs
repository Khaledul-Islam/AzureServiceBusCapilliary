using AzureServiceBusCapilliary.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureServiceBusCapilliary.Providers
{
    public class BaseRepository
    {
        public FIK.DAL.SQL _dal;
        public string query;
        public string msg;
        public string baseQuery;
        public string whereCluase;
        public string offsetCluase;

        public BaseRepository()
        {
            msg = "";
            _dal = new FIK.DAL.SQL(StaticDetails.ConnectionString);
        }
    }
}
