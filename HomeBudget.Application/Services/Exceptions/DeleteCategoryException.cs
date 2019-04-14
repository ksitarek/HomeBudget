using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HomeBudget.Application.Services.Exceptions
{
    [Serializable]
    public class DeleteCategoryException : Exception
    {
        public DeleteCategoryException()
        {
        }

        public DeleteCategoryException(string message) : base(message)
        {
        }

        public DeleteCategoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeleteCategoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}