using System;
using System.Collections.Generic;
using System.Text;

namespace AzureServiceBusCapilliary.Entities
{
    public class Order
    {
        public long OrderId { get; set; }
        public int ConversionFactor { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal AmountPayable { get; set; }
        public string ShippingMode { get; set; }
        public decimal PromotionDiscount { get; set; }
        public long OriginalOrderId { get; set; }
        public string MerchantId { get; set; }
        public long ReturnOrderId { get; set; }
        public bool IsGift { get; set; }
        public string VoucherCode { get; set; }
        public decimal RefundAmount { get; set; }
        public string ReferenceNo { get; set; }
        public string PickupMobile { get; set; }
        public decimal VoucherDiscount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal ShippingDiscount { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryOption { get; set; }
        public string Rewards { get; set; }
        public string Status { get; set; }
        public string TransferStatus { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        [FIK.DAL.FIK_NoCUD]
        public List<OrderLine> orderLine { get; set; }
        [FIK.DAL.FIK_NoCUD]
        public List<PaymentDetails> paymentDetails { get; set; }
    }
    public class OrderLine
    {
        public long OrderLineId { get; set; }
        public long ParentOrderlineId { get; set; }
        public decimal TotalVoucherDiscount { get; set; }
        public long OrderId { get; set; }
        public string Description { get; set; }
        public string DerivedStatusCode { get; set; }
        public string DerivedStatus { get; set; }
        public decimal TotalPromotionDiscount { get; set; }
        public string DeliveryMode { get; set; }
        public string ItemStatus { get; set; }
        public long VariantProductId { get; set; }
        public long ProductId { get; set; }
        public bool IsPrimaryProduct { get; set; }
        public string SKU { get; set; }
        public string VariantSku { get; set; }
        public decimal Quantity { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal ShippingVoucherDiscount { get; set; }
        public string ProductTitle { get; set; }
        public decimal CancelQuantity { get; set; }
        public bool IsParentProduct { get; set; }
        public bool IsBackOrder { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public string locationCode { get; set; }
        public long BundleProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public string StockAction { get; set; }
        public string ReturnStatus { get; set; }
        public string TransferStatus { get; set; }
    }
    public class PaymentDetails
    {
        public long PaymentDetailsId { get; set; }
        public DateTime paymentDate { get; set; }
        public long OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentType { get; set; }
        public string ResponseCode { get; set; }
        public int PointsBurned { get; set; }
        public string ClientIP { get; set; }
        public string PaymentOption { get; set; }
        public string currencyCode { get; set; }
        public string PaymentStatus { get; set; }
    }
}
