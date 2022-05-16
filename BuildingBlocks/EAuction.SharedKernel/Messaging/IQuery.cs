using MediatR;

namespace Auction.SharedKernel.Messaging
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
