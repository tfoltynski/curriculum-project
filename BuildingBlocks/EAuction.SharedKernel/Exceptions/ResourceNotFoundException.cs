using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace Auction.SharedKernel.Exceptions
{
    public sealed class ResourceNotFoundException : ApiException<ResourceNotFoundException.Details>
    {
        public ResourceNotFoundException(string resourceName, string resourceId) : base(StatusCodes.Status404NotFound, $"Resource '{resourceName}' with id: '{resourceId}' could not be found.")
        {
            base.Details = new Details { ResourceName = resourceName, Id = resourceId };
        }

        public ResourceNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }

        public sealed new class Details
        {
            public string ResourceName { get; set; }

            public string Id { get; set; }
        }
    }
}
