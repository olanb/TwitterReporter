using System;
using System.Collections.Concurrent;
using Tweetinvi;
using TwitterReporter.Services;
using Xunit;

namespace TwitterReporter.Tests.Acceptance
{
    public class TweetStreamServiceTests
    {
        [Fact]
        public void CalculateTweetsPerMinute()
        {
            var expectedTweetsPerMinute = 3;
            var expectedTweetCount = 3;
            // Given
            var testDictionary = new ConcurrentDictionary<string, DateTime>();
            var dateTime = DateTime.Today;
            testDictionary.TryAdd("one", new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0));
            testDictionary.TryAdd("two", new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 30));
            testDictionary.TryAdd("three", new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 1, 0));

            //When
            var service = new TweetStreamService(null, testDictionary);
            var result = service.CalculateTweetsPerMinute();

            //Then
            Assert.Equal(expectedTweetsPerMinute, result.Item1);
            Assert.Equal(expectedTweetCount, result.Item2);
        }
    }
}
