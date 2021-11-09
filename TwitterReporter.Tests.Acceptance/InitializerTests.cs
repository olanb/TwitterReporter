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
            var serviceProvider = Initializer.GetServiceProvider(Initializer.BuildConfig());
            // When
            var client = serviceProvider.GetService<ITwitterClient>();
            // Then
            Assert.Equal("hLmvUqhyIi1BpOlfGxBhzGqbJ", client.Credentials.ConsumerKey);
            Assert.Equal("YNqkDNcP8nbjeyZAR6vJF9M3fBi8Biu50ViG63DMVNeF4dS2AR", client.Credentials.ConsumerSecret);
            Assert.Equal("AAAAAAAAAAAAAAAAAAAAAE2sUQEAAAAAk271BUJBMgXW9OzRGuZaEZFEYNA%3D1BvKjdIt3UyaTyyQVEOEIqTATbgJDDJyT0Zz21imQ7kV3AMKvY", client.Credentials.BearerToken);
        }
    }


}
