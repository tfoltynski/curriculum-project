using System;
using System.Runtime.Serialization;

namespace Auction.SharedKernel.Exceptions
{
    [Serializable]
    public abstract class ApiException<T> : ApiException
    {
        private T details;

        protected ApiException(int status, string message) : base(status, message) { }

        protected ApiException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

        public new T Details
        {
            get => details;
            protected set
            {
                details = value;
                base.Details = value;
            }
        }
    }

    [Serializable]
    public abstract class ApiException : Exception
    {
        protected ApiException(int status, string message) : base(message)
        {
            Status = status;
        }

        protected ApiException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

        public object Details { get; protected set; }

        public int Status { get; protected set; }
    }
}
