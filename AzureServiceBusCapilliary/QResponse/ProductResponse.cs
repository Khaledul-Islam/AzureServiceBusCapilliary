using System;
using System.Collections.Generic;
using System.Text;

namespace AzureServiceBusCapilliary.QResponse
{
    public class NewData
    {
        public string Availability { get; set; }
        public string productSKU { get; set; }
        public string productId { get; set; }
        public string variantProductId { get; set; }
        public string productURL { get; set; }
        public string variantSKU { get; set; }
    }

    public class ProductResponse
    {
        public string action { get; set; }
        public NewData newData { get; set; }
    }
}
