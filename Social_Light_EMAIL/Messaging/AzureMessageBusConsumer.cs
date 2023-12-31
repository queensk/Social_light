using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using Social_Light_EMAIL.Messaging.IMessaging;
using Social_Light_EMAIL.Models;
using Social_Light_EMAIL.Services;

namespace Social_Light_EMAIL.Messaging
{
    public class AzureMessageBusConsumer : IAzureMessageBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly string Connectionstring;
        private readonly string QueueName;
        private readonly string topic;
        private readonly string subscription;
        private readonly ServiceBusProcessor _registrationProcessor;
        private readonly ServiceBusProcessor _orderEmails;
        private readonly EmailSendService _emailService;
        private readonly EmailService _saveToDb;

        public AzureMessageBusConsumer(IConfiguration configuration, EmailService saveToDb)
        {
            _configuration = configuration;
            Connectionstring = _configuration.GetSection("ServiceBus:ConnectionString").Get<string>();
            QueueName = _configuration.GetSection("QueuesandTopics:RegisterUser").Get<string>();
            topic = _configuration.GetSection("AzureService:Topic").Get<string>();
            subscription = _configuration.GetSection("AzureService:Subscription").Get<string>();
            
            var servicesBusClient = new ServiceBusClient(Connectionstring);
            _registrationProcessor = servicesBusClient.CreateProcessor(QueueName);
            _orderEmails = servicesBusClient.CreateProcessor(topic, subscription);
            _emailService = new EmailSendService(_configuration);
            _saveToDb = saveToDb;
        }
        public async Task Start()
        {
            _registrationProcessor.ProcessMessageAsync += OnRegistartion;
            _registrationProcessor.ProcessErrorAsync += ErrorHandler;
            await _registrationProcessor.StartProcessingAsync();

            // _orderEmails.ProcessMessageAsync += OnLike;
            _orderEmails.ProcessErrorAsync += ErrorHandler;
            await _orderEmails.StartProcessingAsync();

        }

        public async Task Stop()
        {
            await  _registrationProcessor.StopProcessingAsync();
            await _registrationProcessor.DisposeAsync();
        }

        // private async Task OnLike(ProcessMessageEventArgs arg)
        // {
        //     var message = arg.Message;

        //     var body = Encoding.UTF8.GetString(message.Body);

        //     // var rewards = JsonConvert.DeserializeObject<RewardsDto>(body);

        //     StringBuilder sb = new StringBuilder();
        //     sb.Append("<h1>This user Liked your post</h1>");
        //     sb.Append("<p>You have this likes</p>");

        //     var Umessage = new UserMessage()
        //     {
        //         // Email = rewards.Email,
        //         Name = "John Doe"
        //     };

        //     await _emailService.SendEmailAsync(Umessage, sb.ToString());
        //     await arg.CompleteMessageAsync(message);
        // }

        private Task ErrorHandler(ProcessErrorEventArgs arg)
        {
            Console.WriteLine(arg.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnRegistartion(ProcessMessageEventArgs arg)
        {
            var message= arg.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            var userMessage= JsonConvert.DeserializeObject<UserMessage>(body);

            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<img src=\"https://cdn.pixabay.com/photo/2023/04/20/10/19/coding-7939372_1280.jpg\" width=\"600\" height=\"600\">");
                stringBuilder.Append("<h1> Hello " + userMessage.Name + "</h1>");
                stringBuilder.AppendLine("<br/>Welcome to The Social Light");

                stringBuilder.Append("<br/>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p>Get Your clout</p>");
                var emailLogger = new EmailLoggers()
                {
                    Email = userMessage.Email,
                    Message = stringBuilder.ToString()

                };
                await _saveToDb.SaveData(emailLogger);
                await _emailService.SendEmailAsync(userMessage, stringBuilder.ToString());
                //you can delete the message from the queue
                 await arg.CompleteMessageAsync(message);
            }catch (Exception ex) {

             }
        }

    }
}