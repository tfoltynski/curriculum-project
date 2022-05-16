using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace Auction.SharedKernel.Exceptions
{
    public sealed class BusinessViolationException : ApiException
    {
        public BusinessViolationException(string message) : base(StatusCodes.Status400BadRequest, message)
        {
        }

        public BusinessViolationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
    }
}
