using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBudget.Application.Commands.Common
{
    public class CommandResult
    {
        public bool IsSuccess { get; private set; }
        public dynamic ObjectId { get; private set; }
        public Exception Exception { get; private set; }

        public static CommandResult Success() => new CommandResult() { IsSuccess = true };

        public static CommandResult Success(dynamic objectId) => new CommandResult() { IsSuccess = true, ObjectId = objectId };

        public static CommandResult Error(Exception exception) => new CommandResult() { IsSuccess = false, Exception = exception };
    }
}