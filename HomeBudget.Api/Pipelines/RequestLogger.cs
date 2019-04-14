using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HomeBudget.Application.Commands.Common;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace HomeBudget.Api.Pipelines
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<TRequest> _logger;

        public RequestLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;
            _logger.LogInformation("Received request {0}.", name);

            return Task.CompletedTask;
        }
    }
}