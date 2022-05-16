using MediatR;

namespace Auction.SharedKernel.Messaging
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }

    public interface ICommand : IRequest
    {
    }
}
