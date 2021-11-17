using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.DependencyInjection;
using Tweetinvi;
using TwitterReporter.Services;
using Xunit;

namespace TwitterReporter.Tests.Acceptance
{
    public class InitializerTests
    {
        [Fact]
        public void GetTwitterCredentials()
        {
            // Given
            var config = Initializer.BuildConfig();
            var serviceProvider = Initializer.GetServiceProvider(config);
            var expectedConsumerKey = config.GetSection("TwitterCredentials:ConsumerKey").Value;
            var expectedConsumerSecret = config.GetSection("TwitterCredentials:ConsumerSecret").Value;
            var expectedBearerToken = config.GetSection("TwitterCredentials:BearerToken").Value;
            // When
            var client = serviceProvider.GetService<ITwitterClient>();
            // Then
            Assert.Equal(expectedConsumerKey, client.Credentials.ConsumerKey);
            Assert.Equal(expectedConsumerSecret, client.Credentials.ConsumerSecret);
            Assert.Equal(expectedBearerToken, client.Credentials.BearerToken);
        }
    }


}
