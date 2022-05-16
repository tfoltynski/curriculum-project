namespace Auction.SharedKernel.Events
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : Event;
    }
}
