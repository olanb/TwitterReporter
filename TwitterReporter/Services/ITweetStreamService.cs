using System;
using System.Threading.Tasks;

namespace TwitterReporter.Services
{
    public interface ITweetStreamService
    {
        Task StartAsync();
        Tuple<double, int> CalculateTweetsPerMinute();
    }
}