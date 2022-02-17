using System;
using System.Collections.Generic;
using System.Text;

namespace AzureServiceBusCapilliary.Entities
{
	public class Product
	{
		public string ArtNo { get; set; }
		public string MainGroupId { get; set; }
		public string SubGroupId { get; set; }
		public string CategoryId { get; set; }
		public string SubCategoryId { get; set; }
		public string BasiceBottomMaterialId { get; set; }
		public string HeelId { get; set; }
		public string BrandId { get; set; }
		public string LifeStyleId { get; set; }
		public string SourceId { get; set; }
		public string ProductClassificationId { get; set; }
		public string ProjectId { get; set; }
		public DateTime? EntryDate { get; set; }
		public string Unit { get; set; }
		public string VatCode { get; set; }
		public string Season { get; set; }
		public string Active { get; set; }
		public decimal Price { get; set; }
		public bool IsEc { get; set; }
	}
}
