using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Streaming.V2;

namespace TwitterReporter.Services
{
    public class TweetStreamService : ITweetStreamService
    {
        private readonly ConcurrentDictionary<string, DateTime> _tweetDictionary;
        private ITwitterClient _client;

        public TweetStreamService(ITwitterClient client, IDictionary<string, DateTime> concurrentDictionary)
        {
             _client = client;
            _tweetDictionary = concurrentDictionary as ConcurrentDictionary<string, DateTime>;
        }

        public async Task StartAsync()
        {
            var sampleStreamV2 = _client.StreamsV2.CreateSampleStream();
            sampleStreamV2.TweetReceived += (sender, args) =>
            {
                _tweetDictionary.TryAdd(args.Tweet.Text, DateTime.Now);
            };
            await sampleStreamV2.StartAsync();

        }

        public Tuple<double, int> CalculateTweetsPerMinute()
        {
            double tweetsPerMinute = 0;
            int count = 0;
            if (_tweetDictionary.Count != 0)
            {
                count = _tweetDictionary.Count;
                var timeSpan2 = (_tweetDictionary.Values.Max() - _tweetDictionary.Values.Min());
                tweetsPerMinute = Math.Round(count / timeSpan2.TotalMinutes);
            }
            return Tuple.Create(tweetsPerMinute, count);

        }
    }
}
