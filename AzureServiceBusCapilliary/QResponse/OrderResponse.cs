using System;
using System.Collections.Generic;
using System.Text;

namespace AzureServiceBusCapilliary.QResponse
{
    public class Deliveryslot
    {
        public string startTime { get; set; }
        public string slotId { get; set; }
        public string endTime { get; set; }
    }

    public class OrderAttribute
    {
        public string attributeID { get; set; }
        public string orderId { get; set; }
        public string merchantId { get; set; }
        public string attributeName { get; set; }
        public string value { get; set; }
    }

    public class PaymentDetail
    {
        public double amount { get; set; }
        public string clientUserAgent { get; set; }
        public string channel { get; set; }
        public string gV { get; set; }
        public string paymentType { get; set; }
        public string responseCode { get; set; }
        public string paymentResponse { get; set; }
        public string pointsBurned { get; set; }
        public string clientIP { get; set; }
        public string paymentOption { get; set; }
        public string paymentDetailsId { get; set; }
        public string paymentDate { get; set; }
        public string currencyCode { get; set; }
        public string paymentStatus { get; set; }
    }

    public class ShippingAddress
    {
        public string zip { get; set; }
        public string country { get; set; }
        public string firstname { get; set; }
        public string city { get; set; }
        public string address2 { get; set; }
        public string address1 { get; set; }
        public string mobile { get; set; }
        public string cityID { get; set; }
        public string lastname { get; set; }
        public string phone { get; set; }
        public string countryCode { get; set; }
        public string stateCode { get; set; }
        public string state { get; set; }
        public object ShipOtherCity { get; set; }
        public string email { get; set; }
    }

    public class BillingAddress
    {
        public string zip { get; set; }
        public string country { get; set; }
        public string firstname { get; set; }
        public string city { get; set; }
        public string address2 { get; set; }
        public string address1 { get; set; }
        public string mobile { get; set; }
        public string cityID { get; set; }
        public string lastname { get; set; }
        public string phone { get; set; }
        public string countryCode { get; set; }
        public string stateCode { get; set; }
        public string state { get; set; }
        public string email { get; set; }
    }

    public class OrderLineId
    {
        public decimal totalVoucherDiscount { get; set; }
        public string orderId { get; set; }
        public string customFields { get; set; }
        public string portion { get; set; }
        public string description { get; set; }
        public string vendorId { get; set; }
        public string derivedStatusCode { get; set; }
        public object parentOrderlineId { get; set; }
        public string derivedStatus { get; set; }
        public decimal totalPromotionDiscount { get; set; }
        public string deliveryMode { get; set; }
        public string itemStatus { get; set; }
        public string variantProductId { get; set; }
        public string isPrimaryProduct { get; set; }
        public string VariantSku { get; set; }
        public string image { get; set; }
        public decimal quantity { get; set; }
        public decimal shippingCost { get; set; }
        public string productId { get; set; }
        public decimal shippingVoucherDiscount { get; set; }
        public List<object> promotionIds { get; set; }
        public string ProductTitle { get; set; }
        public decimal cancelQuantity { get; set; }
        public bool isParentProduct { get; set; }
        public bool isBackOrder { get; set; }
        public string orderLineId { get; set; }
        public decimal totalTaxAmount { get; set; }
        public string locationCode { get; set; }
        public string SKU { get; set; }
        public string BundleProductId { get; set; }
        public double productPrice { get; set; }
        public string stockAction { get; set; }
    }

    public class Data
    {
        public string channelOrderID { get; set; }
        public Deliveryslot deliveryslot { get; set; }
        public List<object> taxDetails { get; set; }
        public string orderId { get; set; }
        public string conversionFactor { get; set; }
        public string latitude { get; set; }
        public decimal taxTotal { get; set; }
        public List<OrderAttribute> orderAttributes { get; set; }
        public double amountPayable { get; set; }
        public string leadTime { get; set; }
        public string shippingMode { get; set; }
        public string promotionDiscount { get; set; }
        public string originalOrderId { get; set; }
        public string merchantId { get; set; }
        public string storeoperatorid { get; set; }
        public string returnOrderId { get; set; }
        public bool isGift { get; set; }
        public string pickupFirstName { get; set; }
        public string pickupEmail { get; set; }
        public List<PaymentDetail> paymentDetails { get; set; }
        public string voucherCode { get; set; }
        public string longitude { get; set; }
        public string refundAmount { get; set; }
        public string referenceNo { get; set; }
        public string giftMessage { get; set; }
        public string pickupMobile { get; set; }
        public string demandedDeliveryDate { get; set; }
        public string userId { get; set; }
        public string subStatus { get; set; }
        public string voucherDiscount { get; set; }
        public double totalAmount { get; set; }
        public List<object> promotions { get; set; }
        public string pickupLastName { get; set; }
        public string isSelfShip { get; set; }
        public ShippingAddress shippingAddress { get; set; }
        public BillingAddress billingAddress { get; set; }
        public string shippingDiscount { get; set; }
        public List<OrderLineId> orderLineId { get; set; }
        public DateTime orderDate { get; set; }
        public string deliveryOption { get; set; }
        public object rewards { get; set; }
        public string channelID { get; set; }
        public string status { get; set; }
    }

    public class ChangedAttributes
    {
        public string orderStatus { get; set; }
        public string updatedDate { get; set; }
    }

    public class OrderResponse
    {
        public Data data { get; set; }
        public ChangedAttributes changedAttributes { get; set; }
    }
}
