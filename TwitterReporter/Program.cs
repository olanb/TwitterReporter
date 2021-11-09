using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using TwitterReporter.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tweetinvi.Core.Streaming;

namespace TwitterReporter
{
    class Program
    {
        private static void Main(string[] args)
        {
            ILogger logger = null;
            try
            {
                var config = Initializer.BuildConfig();
                var serviceProvider = Initializer.GetServiceProvider(config);

                logger = serviceProvider.GetService<ILogger<Program>>();

                var service = serviceProvider.GetService<ITweetStreamService>();
                logger.LogInformation("Starting Tweet Stream Service...");
                var task1 = Task.Run(async () =>
                {
                    if (service == null)
                        throw new NullReferenceException("TweetStreamService not available.");
                    await service.StartAsync();
                });
                while (true)
                {
                    Thread.Sleep(30000);
                    logger.LogInformation("Calculating Tweets Per Minute...");
                    var result = Task.Run(() => service.CalculateTweetsPerMinute()).Result;
                    Console.WriteLine(
                        $"Average Tweets/Minute: {result.Item1}...Total number of tweets received: {result.Item2}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "TwitterReporter encountered an exception.");
            }
        }

    }
}
