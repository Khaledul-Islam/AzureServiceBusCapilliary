using AzureServiceBusCapilliary.Entities;
using AzureServiceBusCapilliary.Providers;
using AzureServiceBusCapilliary.QResponse;
using AzureServiceBusCapilliary.Utilities;
using FIK.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AzureServiceBusCapilliary
{
    public class AzureRepository : BaseRepository
    {
        public long orderId;
        public AzureRepository()
        {

        }
        #region OrderManager
        public bool OrderManager(OrderResponse response, out string errMsg)
        {
            errMsg = string.Empty;
            CompositeModel composite = new CompositeModel();
            CompositeModel compositeLine = new CompositeModel();
            if (response.data.status == "A" && response.actionDetails != null && response.actionDetails.type == "ORDER_ALLOCATION")//|| response.data.status == "D" || response.data.status == "C"
            {
                string updateCol = @"OrderLineId,ParentOrderlineId,TotalVoucherDiscount,OrderId,Description,DerivedStatusCode,DerivedStatus,TotalPromotionDiscount,DeliveryMode,ItemStatus,VariantProductId,ProductId,IsPrimaryProduct,SKU,VariantSku,Quantity,ShippingCost,ShippingVoucherDiscount,ProductTitle,CancelQuantity,IsParentProduct,IsBackOrder,TotalTaxAmount,locationCode,BundleProductId,ProductPrice,StockAction,ReturnStatus,TransferStatus";

                List<OrderLine> dbOrderLst = _dal.Select<OrderLine>("select * from EC_OrderLine where OrderId='" + response.data.orderId + "'", ref msg);
                if (dbOrderLst != null && dbOrderLst.Count > 0)
                {
                    foreach (var dbOrder in dbOrderLst)
                    {
                        var data = response.data.orderLineId.Find(a => a.VariantSku == dbOrder.VariantSku && a.orderId == Convert.ToString(dbOrder.OrderId));

                        if (Convert.ToString(dbOrder.OrderId) == data.orderId && dbOrder.VariantSku == data.VariantSku && dbOrder.locationCode == data.locationCode)
                        {
                            if (dbOrder.TransferStatus == "Y")
                            {
                                //order line transfer status Y
                                Order model = PrepareData(response);
                                model.TransferStatus = "Y";
                                compositeLine.AddRecordSet<OrderLine>(model, OperationMode.Update, "", updateCol, "OrderId,VariantSku", "EC_OrderLine");

                            }
                            else if (dbOrder.TransferStatus == "N")
                            {
                                Order model = PrepareData(response);
                                model.TransferStatus = "N";
                                compositeLine.AddRecordSet<OrderLine>(model, OperationMode.Update, "", updateCol, "OrderId,VariantSku", "EC_OrderLine");
                            }
                        }
                        else if (Convert.ToString(dbOrder.OrderId) == data.orderId && dbOrder.VariantSku == data.VariantSku && dbOrder.locationCode != data.locationCode)
                        {
                            //order line transfer status N
                            Order model = PrepareData(response);
                            model.TransferStatus = "N";
                            compositeLine.AddRecordSet<OrderLine>(model, OperationMode.Update, "", updateCol, "OrderId,VariantSku", "EC_OrderLine");
                            if (dbOrder.TransferStatus == "N")
                            {
                                dbOrder.TransferStatus = "Skip";
                                compositeLine.AddRecordSet<OrderLine>(dbOrder, OperationMode.Insert, "", "", "OrderId,VariantSku", "EC_OrderLineHistory");
                            }
                            if (dbOrder.TransferStatus == "Y")
                            {
                                dbOrder.TransferStatus = "N";
                                compositeLine.AddRecordSet<OrderLine>(dbOrder, OperationMode.Insert, "", "", "OrderId,VariantSku", "EC_OrderLineHistory");
                            }
                        }
                    }
                    var lineResponse = _dal.InsertUpdateComposite(compositeLine, ref msg);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        errMsg = msg;
                    }
                    return lineResponse;
                }
                else
                {
                    try
                    {
                        Order model = PrepareData(response);

                        //if (response.data.originalOrderId != null)
                        //{
                        //    List<OrderLine> orderPrv = _dal.Select<OrderLine>("select * from EC_OrderLine where OrderId='" + model.OriginalOrderId + "'", ref msg);
                        //    foreach (var item in orderPrv)
                        //    {
                        //        item.CancelQuantity = item.Quantity;
                        //        item.DerivedStatus = "Cancelled";
                        //        item.DerivedStatusCode = "IC";
                        //    }
                        //    composite.AddRecordSet<OrderLine>(orderPrv, OperationMode.Update, "", "CancelQuantity,DerivedStatus,DerivedStatusCode", "OrderId", "EC_OrderLine");

                        //}


                        composite.AddRecordSet<Order>(model, OperationMode.InsertOrUpdaet, "", "", "OrderId", "EC_Order");
                        composite.AddRecordSet<OrderLine>(model.orderLine, OperationMode.InsertOrUpdaet, "", "", "OrderId,VariantSku", "EC_OrderLine");
                        composite.AddRecordSet<PaymentDetails>(model.paymentDetails, OperationMode.InsertOrUpdaet, "", "", "OrderId", "EC_PaymentDetails");
                        var res = _dal.InsertUpdateComposite(composite, ref msg);
                        if (!string.IsNullOrEmpty(msg))
                        {
                            errMsg = msg;
                        }
                        errMsg = "Order is not allocated";
                        return res;
                    }
                    catch (Exception e)
                    {

                        errMsg = e.Message;
                        return false;
                    }
                }

                //if (response.data.status == "C")
                //{
                //    var ISExist = _dal.SelectFirstOrDefault<Order>("select * from EC_Order where OrderId='" + response.data.orderId + "'", ref msg);
                //    if (ISExist == null)
                //    {
                //        errMsg = "This order data not exist in our system";
                //        return false;
                //    }
                //}
            }
            else if (response.data.status == "C" || response.data.status == "D")
            {
                try
                {
                    Order model = PrepareData(response);

                    //if (response.data.originalOrderId != null)
                    //{
                    //    List<OrderLine> orderPrv = _dal.Select<OrderLine>("select * from EC_OrderLine where OrderId='" + model.OriginalOrderId + "'", ref msg);
                    //    foreach (var item in orderPrv)
                    //    {
                    //        item.CancelQuantity = item.Quantity;
                    //        item.DerivedStatus = "Cancelled";
                    //        item.DerivedStatusCode = "IC";
                    //    }
                    //    composite.AddRecordSet<OrderLine>(orderPrv, OperationMode.Update, "", "CancelQuantity,DerivedStatus,DerivedStatusCode", "OrderId", "EC_OrderLine");

                    //}


                    if (response.data.status == "C")
                    {
                        var ISExist = _dal.SelectFirstOrDefault<Order>("select * from EC_Order where OrderId='" + response.data.orderId + "'", ref msg);
                        if (ISExist == null)
                        {
                            errMsg = "This order data not exist in our system";
                            return false;
                        }
                    }

                    string updateCol = @"OrderLineId,ParentOrderlineId,TotalVoucherDiscount,OrderId,Description,DerivedStatusCode,DerivedStatus,TotalPromotionDiscount,DeliveryMode,ItemStatus,VariantProductId,ProductId,IsPrimaryProduct,SKU,VariantSku,Quantity,ShippingCost,ShippingVoucherDiscount,ProductTitle,CancelQuantity,IsParentProduct,IsBackOrder,TotalTaxAmount,locationCode,BundleProductId,ProductPrice,StockAction,ReturnStatus";


                    composite.AddRecordSet<Order>(model, OperationMode.InsertOrUpdaet, "", "", "OrderId", "EC_Order");
                    composite.AddRecordSet<OrderLine>(model.orderLine, OperationMode.InsertOrUpdaet, "", updateCol, "OrderId,VariantSku", "EC_OrderLine");
                    composite.AddRecordSet<PaymentDetails>(model.paymentDetails, OperationMode.InsertOrUpdaet, "", "", "OrderId", "EC_PaymentDetails");

                    var res = _dal.InsertUpdateComposite(composite, ref msg);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        errMsg = msg;
                    }
                    errMsg = "Order is not allocated";
                    return res;
                }
                catch (Exception e)
                {

                    errMsg = e.Message;
                    return false;
                }
            }
            else
            {
                errMsg = "Skipped.Order Status Not In Requirement";
                return false;
            }

        }

        private Order PrepareData(OrderResponse response)
        {
            Order model = new Order();
            model.AmountPayable = Convert.ToDecimal(response.data.amountPayable);
            model.ConversionFactor = Convert.ToInt32(response.data.conversionFactor);
            model.DeliveryOption = response.data.deliveryOption;
            model.IsGift = response.data.isGift;
            model.MerchantId = response.data.merchantId;
            model.OrderDate = response.data.orderDate;
            model.OrderId = Convert.ToInt64(response.data.orderId);
            orderId = model.OrderId;
            model.OriginalOrderId = string.IsNullOrEmpty(response.data.originalOrderId) ? 0 : Convert.ToInt64(response.data.originalOrderId);
            model.PickupMobile = response.data.pickupMobile;
            model.PromotionDiscount = Convert.ToDecimal(response.data.promotionDiscount);
            model.ReferenceNo = response.data.referenceNo;
            model.RefundAmount = Convert.ToDecimal(response.data.refundAmount);
            model.ReturnOrderId = string.IsNullOrEmpty(response.data.returnOrderId) ? 0 : Convert.ToInt64(response.data.returnOrderId);
            model.Rewards = Convert.ToString(response.data.rewards);
            model.ShippingDiscount = Convert.ToDecimal(response.data.shippingDiscount);
            model.ShippingMode = response.data.shippingMode;
            model.Status = response.data.status;
            model.TaxTotal = response.data.taxTotal;
            model.TotalAmount = Convert.ToDecimal(response.data.totalAmount);
            model.VoucherCode = response.data.voucherCode;
            model.VoucherDiscount = Convert.ToDecimal(response.data.voucherDiscount);
            model.Mobile = response.data.billingAddress.mobile;
            model.Address = response.data.billingAddress.address1 + response.data.billingAddress.address2;
            model.Name = response.data.billingAddress.firstname + response.data.billingAddress.lastname;
            model.Email = response.data.billingAddress.email;
            model.TransferStatus = "N";// orderline transfer status - New
            model.orderLine = orderLine(response.data.orderLineId);
            model.paymentDetails = paymentDetails(response.data.paymentDetails);

            return model;
        }

        public List<OrderLine> orderLine(List<OrderLineId> lstItem)
        {
            List<OrderLine> orderLine = new List<OrderLine>();
            foreach (var item in lstItem)
            {
                OrderLine order = new OrderLine();
                order.BundleProductId = Convert.ToInt64(item.BundleProductId);
                order.CancelQuantity = Convert.ToDecimal(item.cancelQuantity);
                order.DeliveryMode = item.deliveryMode;
                order.DerivedStatus = item.derivedStatus;
                order.DerivedStatusCode = item.derivedStatusCode;
                order.Description = item.description;
                order.IsBackOrder = item.isBackOrder;
                order.IsParentProduct = item.isParentProduct;
                order.IsPrimaryProduct = Convert.ToBoolean(item.isPrimaryProduct);
                order.ItemStatus = item.itemStatus;
                order.locationCode = item.locationCode;
                order.OrderId = Convert.ToInt64(item.orderId);
                order.OrderLineId = Convert.ToInt64(item.orderLineId);
                order.ParentOrderlineId = Convert.ToInt64(item.parentOrderlineId);
                order.ProductId = Convert.ToInt64(item.productId);
                order.ProductPrice = Convert.ToDecimal(item.productPrice);
                order.ProductTitle = item.ProductTitle;
                order.Quantity = Convert.ToDecimal(item.quantity);
                order.ShippingCost = Convert.ToDecimal(item.shippingCost);
                order.ShippingVoucherDiscount = Convert.ToDecimal(item.shippingVoucherDiscount);
                order.SKU = item.SKU;
                order.StockAction = item.stockAction;
                order.TotalPromotionDiscount = item.totalPromotionDiscount;
                order.TotalTaxAmount = item.totalTaxAmount;
                order.TotalVoucherDiscount = Convert.ToDecimal(item.totalVoucherDiscount);
                order.VariantProductId = Convert.ToInt64(item.variantProductId);
                order.VariantSku = item.VariantSku;
                order.TransferStatus = "N";
                orderLine.Add(order);
            }
            return orderLine;
        }
        public List<PaymentDetails> paymentDetails(List<PaymentDetail> lstPayment)
        {
            List<PaymentDetails> paymentDetails = new List<PaymentDetails>();
            foreach (var item in lstPayment)
            {
                PaymentDetails model = new PaymentDetails();
                model.Amount = Convert.ToDecimal(item.amount);
                model.ClientIP = item.clientIP;
                model.currencyCode = item.currencyCode;
                model.OrderId = orderId;
                model.paymentDate = Convert.ToDateTime(item.paymentDate);
                model.PaymentDetailsId = Convert.ToInt64(item.paymentDetailsId);
                model.PaymentOption = item.paymentOption;
                model.PaymentStatus = item.paymentStatus;
                model.PaymentType = item.paymentType;
                model.PointsBurned = Convert.ToInt32(item.pointsBurned);
                model.ResponseCode = item.responseCode;

                paymentDetails.Add(model);
            }

            return paymentDetails;
        }
        #endregion

        #region ProductManager
        public bool ProductManager(ProductResponse response, out string errMsg)
        {
            try
            {
                errMsg = string.Empty;
                List<string> sqlList = new List<string>();
                if (!string.IsNullOrEmpty(response.newData.variantSKU))
                {
                    sqlList.Add("update  Article set  IsEc='1' where ArtNo='" + response.newData.variantSKU + "' ");
                }
                else
                {
                    sqlList.Add("update  Article set  IsEc='1' where left(ArtNo,8)='" + response.newData.productSKU + "' ");

                }
                bool resp = _dal.ExecuteQuery(sqlList, ref msg);
                if (resp == false || !string.IsNullOrEmpty(msg))
                {
                    errMsg = msg;
                    return false;
                }
                return resp;
            }
            catch (Exception ex)
            {

                errMsg = ex.Message;
                return false;
            }
        }
        #endregion

        #region ReturnManager
        public bool ReturnManager(ReturnResponse response, out string errMsg)
        {
            var orderId = response.returnRequest.orderId;
            errMsg = string.Empty;
            List<string> sqlList = new List<string>();
            try
            {
                foreach (var item in response.returnRequest.returnRequestDetails)
                {
                    sqlList.Add("Update EC_OrderLine set CancelQuantity='" + item.returnQty + "', ReturnStatus='" + response.returnRequest.requestStatus + "' where OrderId='" + orderId + "' and VariantSku='" + item.variantSKU + "'");
                }
                bool resp = _dal.ExecuteQuery(sqlList, ref msg);
                if (resp == false || !string.IsNullOrEmpty(msg))
                {
                    errMsg = msg;
                    return false;
                }
                return resp;
            }
            catch (Exception ex)
            {

                errMsg = ex.Message;
                return false;
            }
        }
        #endregion

        #region LogManager
        public void LogManager(string JsonString, string errMsg, bool response, string FromWhere, string ID)
        {
            try
            {
                //EcDataTransferLog
                TransferLog tl = new TransferLog();
                tl.Date = DateTime.Now;
                tl.Type = "AzureServiceBus";
                tl.Taskid = FromWhere;
                if (!response)
                {
                    tl.Message = JsonString;
                    tl.Status = "FAILED";
                    tl.MessageCode = ID;
                    tl.ErrorCode = ID;
                    tl.Reason = errMsg;
                }
                if (response)
                {
                    tl.Message = "ID :- " + FromWhere + " has been saved successfully." + JsonString;
                    tl.Status = "SUCCESSED";
                    tl.MessageCode = ID;
                    tl.ErrorCode = ID;
                    tl.Reason = "";
                }
                _dal.Insert<TransferLog>(tl, "", "", "EcDataTransferLog", ref msg);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region DBCheck
        public bool ConnCheck()
        {

            try
            {
                var response = _dal.Select<Order>("select top(1) OrderId from EC_Order", ref msg);
                if (!string.IsNullOrEmpty(msg))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception e)
            {
                throw e;
                return false;
            }
            // var response = _dal.Select<Order>("select top(1) OrderId from EC_Order", ref msg).Count;
            // return response > 0 ? true : false;
        }
        #endregion
    }
}
