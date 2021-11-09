using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tweetinvi;
using Tweetinvi.Models;
using TwitterReporter.Services;

namespace TwitterReporter
{
    public static class Initializer
    {
        public static ServiceProvider GetServiceProvider(IConfiguration config)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(loggingBuilder =>
                {
                    var loggingSection = config.GetSection("Logging");
                    loggingBuilder.AddFile(loggingSection);
                })
                .AddSingleton<ITwitterClient>(new TwitterClient(GetCredentials(config)))
                .AddSingleton<IDictionary<string, DateTime>, ConcurrentDictionary<string, DateTime>>()
                .AddSingleton<ITweetStreamService, TweetStreamService>()
                .BuildServiceProvider();
            return serviceProvider;
        }


        private static ConsumerOnlyCredentials GetCredentials(IConfiguration config)
        {
 
            return new ConsumerOnlyCredentials
            {
                ConsumerKey = config.GetSection("TwitterCredentials:ConsumerKey").Value,
                ConsumerSecret = config.GetSection("TwitterCredentials:ConsumerSecret").Value,
                BearerToken = config.GetSection("TwitterCredentials:BearerToken").Value
            };

        }
        public static IConfiguration BuildConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: false);

            IConfiguration config = builder.Build();
            return config;
        }

    }
}