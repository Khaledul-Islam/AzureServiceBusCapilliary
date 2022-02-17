using System;
using System.Collections.Generic;
using System.Text;

namespace AzureServiceBusCapilliary.Entities
{
	public class TransferLog
	{
		public string Taskid { get; set; }
		public string MessageCode { get; set; }
		public string Message { get; set; }
		public string ErrorCode { get; set; }
		public string Type { get; set; }
		public DateTime? Date { get; set; }
		public string Status { get; set; }
		public string Reason { get; set; }
	}
}
