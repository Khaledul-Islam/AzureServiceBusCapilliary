using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace AzureServiceBusCapilliary.Utilities
{
    public static class StaticDetails
    {
        public static string Endpoint = ConfigurationManager.AppSettings["Endpoint"];
        public static string Topic = ConfigurationManager.AppSettings["Topic"];
        public static string OrderSubscription = ConfigurationManager.AppSettings["OrderSubscription"];
        public static string ProductSubscription = ConfigurationManager.AppSettings["ProductSubscription"];
        public static string LocationSubscription = ConfigurationManager.AppSettings["LocationSubscription"];
        public static string ReturnSubscription = ConfigurationManager.AppSettings["ReturnSubscription"];
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }
}
