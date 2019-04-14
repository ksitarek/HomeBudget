using System;
using System.Runtime.Serialization;

namespace HomeBudget.Application.Services.Exceptions
{
    [Serializable]
    public class CreateCategoryException : Exception
    {
        public CreateCategoryException()
        {
        }

        public CreateCategoryException(string message) : base(message)
        {
        }

        public CreateCategoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CreateCategoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}