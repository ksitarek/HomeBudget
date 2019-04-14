using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HomeBudget.Application.Services.Exceptions
{
    [Serializable]
    public class RenameCategoryException : Exception
    {
        public RenameCategoryException()
        {
        }

        public RenameCategoryException(string message) : base(message)
        {
        }

        public RenameCategoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RenameCategoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}