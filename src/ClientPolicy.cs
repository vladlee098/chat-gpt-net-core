using System;
using System.Net.Http;
using Polly;
using Polly.Retry;

namespace src.ChatGpt
{
    ///
    /// This is a copy from:
    /// https://github.com/binarythistle/Polly-Beginners-Guide/blob/main/RequestService/Policies/ClientPolicy.cs
    ///
    public class ClientPolicy
    {
        public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get;}
        public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry {get;}
        public AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry {get;}

        public ClientPolicy()
        {
            ImmediateHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode)
                .RetryAsync(10);

            LinearHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(3));

            ExponentialHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));            
        }
    }
}