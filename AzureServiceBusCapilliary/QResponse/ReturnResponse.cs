using System;
using System.Collections.Generic;
using System.Text;

namespace AzureServiceBusCapilliary.QResponse
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class RequestedPaymentMode
    {
        public string cityName { get; set; }
        public string paymentMode { get; set; }
        public string accountName { get; set; }
        public string branchName { get; set; }
        public string bankName { get; set; }
        public string accountNumber { get; set; }
        public string ifscCode { get; set; }
    }

    public class ReturnRequestDetail
    {
        public string returnRequestId { get; set; }
        public string reason { get; set; }
        public string suggestedReturnAction { get; set; }
        public string productID { get; set; }
        public string returnQty { get; set; }
        public string orderItemId { get; set; }
        public string isReceived { get; set; }
        public string isTaxable { get; set; }
        public string MRP { get; set; }
        public string confirmedRefundAmount { get; set; }
        public string productTitle { get; set; }
        public string promotionDiscount { get; set; }
        public string UOM { get; set; }
        public string receivedQty { get; set; }
        public string returnRequestDetailId { get; set; }
        public string variantProductID { get; set; }
        public string averageWeight { get; set; }
        public string SKU { get; set; }
        public string productPrice { get; set; }
        public string variantSKU { get; set; }
        public string refundAmount { get; set; }
    }

    public class TaxDetails
    {
    }

    public class ReturnRequest
    {
        public string returnRequestId { get; set; }
        public RequestedPaymentMode requestedPaymentMode { get; set; }
        public List<ReturnRequestDetail> returnRequestDetails { get; set; }
        public string requestType { get; set; }
        public TaxDetails taxDetails { get; set; }
        public string orderId { get; set; }
        public string subStatusComment { get; set; }
        public string includeShippingCost { get; set; }
        public string refundStatus { get; set; }
        public string source { get; set; }
        public string dateInitiated { get; set; }
        public string subStatus { get; set; }
        public string confirmedRefundAmount { get; set; }
        public string taxableCreditNoteNumber { get; set; }
        public string merchantId { get; set; }
        public string shipmentId { get; set; }
        public string dateReceived { get; set; }
        public string isSelfShip { get; set; }
        public string invoiceNumber { get; set; }
        public string billOfSupplyCreditNoteNumber { get; set; }
        public string returnRequestPayments { get; set; }
        public string requestStatus { get; set; }
        public string refundedAmount { get; set; }
    }

    public class ReturnResponse
    {
        public ReturnRequest returnRequest { get; set; }
    }


}
