using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorkerServiceHealth
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var client =new  HttpClient();
                var response =await client.GetStringAsync("https://localhost:44318/health");
                if(response!="healthy")
                {
                    //TODO
                    //Send Email
                    //Send Notification
                    //Send SMS
                    //Send ....
                    //Run A Service
                }
                _logger.LogInformation("Worker running at: {time} with result: {result}", DateTimeOffset.Now,response);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
