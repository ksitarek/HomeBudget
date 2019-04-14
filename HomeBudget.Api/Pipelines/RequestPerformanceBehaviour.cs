using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeBudget.Api.Pipelines
{
    public class RequestPerformanceBehaviour<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResult> next)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 300)
            {
                var name = typeof(TRequest).Name;
                _logger.LogInformation("Long running request {0}: {1} elapsed milliseconds.", name, _timer.ElapsedMilliseconds);
            }

            return response;
        }
    }
}