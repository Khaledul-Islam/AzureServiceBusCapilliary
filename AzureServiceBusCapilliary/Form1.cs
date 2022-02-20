using AzureServiceBusCapilliary.QResponse;
using AzureServiceBusCapilliary.Utilities;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureServiceBusCapilliary
{
    public partial class Form1 : Form
    {
        static ISubscriptionClient orderClient;
        static ISubscriptionClient productClient;
        static ISubscriptionClient returnClient;

        public static string Endpoint = StaticDetails.Endpoint;
        public static string Topic = StaticDetails.Topic;
        public static string OrderSubscription = StaticDetails.OrderSubscription;
        public static string ProductSubscription = StaticDetails.ProductSubscription;
        public static string ReturnSubscription = StaticDetails.ReturnSubscription;
        AzureRepository repo = new AzureRepository();
        public Form1()
        {
            InitializeComponent();
        }

        #region OrderManagement
        private void OrderManagement()
        {
            try
            {
                orderClient = new SubscriptionClient(Endpoint, Topic, OrderSubscription);
                var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
                {
                    MaxConcurrentCalls = 1,
                    AutoComplete = false
                };
                orderClient.RegisterMessageHandler(ReceiveOrderAsync, messageHandlerOptions);
            }
            catch (Exception ex)
            {
                repo.LogManager(ex.StackTrace, ex.Message, false, "Exception from Order");
            }
        }

        async Task ReceiveOrderAsync(Microsoft.Azure.ServiceBus.Message message, CancellationToken token)
        {
            try
            {
                var jsonString = Encoding.UTF8.GetString(message.Body);
                var json = JsonConvert.DeserializeObject<OrderResponse>(jsonString);
                var response = repo.OrderManager(json, out string errMsg);
                repo.LogManager(jsonString, errMsg, response, "Order ID " + json.data.orderId);
                await orderClient.CompleteAsync(message.SystemProperties.LockToken);
            }
            catch (Exception ex)
            {
                var jsonString = Encoding.UTF8.GetString(message.Body);
                repo.LogManager(jsonString, ex.Message + ex.StackTrace, false, "Exception from Order");
            }
        }
        #endregion

        #region ProductManagement
        private void ProductManagement()
        {
            try
            {
                productClient = new SubscriptionClient(Endpoint, Topic, ProductSubscription);
                var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
                {
                    MaxConcurrentCalls = 1,
                    AutoComplete = false
                };
                productClient.RegisterMessageHandler(ReceiveProductAsync, messageHandlerOptions);
            }
            catch (Exception ex)
            {
                repo.LogManager(ex.StackTrace, ex.Message, false, "Exception from Product");
            }
        }
        async Task ReceiveProductAsync(Microsoft.Azure.ServiceBus.Message message, CancellationToken token)
        {
            try
            {
                var jsonString = Encoding.UTF8.GetString(message.Body);
                var json = JsonConvert.DeserializeObject<ProductResponse>(jsonString);
                var response = repo.ProductManager(json, out string errMsg);
                repo.LogManager(jsonString, errMsg, response, "Product ID " + json.newData.productId);
                await orderClient.CompleteAsync(message.SystemProperties.LockToken);
            }
            catch (Exception ex)
            {
                var jsonString = Encoding.UTF8.GetString(message.Body);
                repo.LogManager(jsonString, ex.Message + ex.StackTrace, false, "Exception from Product");
            }
        }
        #endregion


        #region ReturnManagement
        private void ReturnManagement()
        {
            try
            {
                returnClient = new SubscriptionClient(Endpoint, Topic, ReturnSubscription);
                var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
                {
                    MaxConcurrentCalls = 1,
                    AutoComplete = false
                };
                returnClient.RegisterMessageHandler(ReceiveReturnAsync, messageHandlerOptions);
            }
            catch (Exception ex)
            {
                repo.LogManager(ex.StackTrace, ex.Message, false, "Exception from Return");
            }
        }

        async Task ReceiveReturnAsync(Microsoft.Azure.ServiceBus.Message message, CancellationToken token)
        {
            try
            {

                var jsonString = Encoding.UTF8.GetString(message.Body);
                dynamic obj = JsonConvert.DeserializeObject(jsonString);
                var ss = JsonConvert.SerializeObject(obj.data);
                var json = JsonConvert.DeserializeObject<ReturnResponse>(ss);
                var response = repo.ReturnManager(json, out string errMsg);
                repo.LogManager(jsonString, errMsg, response, "Return Order ID " + json.returnRequest.orderId);
                await orderClient.CompleteAsync(message.SystemProperties.LockToken);
            }
            catch (Exception ex)
            {
                var jsonString = Encoding.UTF8.GetString(message.Body);
                repo.LogManager(jsonString, ex.Message + ex.StackTrace, false, "Exception from Return");
            }
        }
        #endregion

        Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            string s = exceptionReceivedEventArgs.Exception.ToString();
            return Task.CompletedTask;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (repo.ConnCheck())
            {
                _ = Task.Run(() => OrderManagement());
                _ = Task.Run(() => ProductManagement());
                _ = Task.Run(() => ReturnManagement());
            }
            else
            {
                MessageBox.Show("Database Connection Error");
                lblDb.Text = "Database Connection Error";
;            }
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrEmpty(lblDb.Text))
            {
                orderClient.CloseAsync();
                productClient.CloseAsync();
                returnClient.CloseAsync();
            }
           
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(1000);
        }
    }
}
