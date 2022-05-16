using Auction.SharedKernel.Events;

namespace Auction.Test.Shared.Builders.Events
{
    public abstract class BaseEventBuilder<T, K> : IBuilder<T> where T : Event where K : BaseEventBuilder<T, K>
    {
        protected abstract T Result { get; set; }

        public T GetResult()
        {
            return Result;
        }

        public void Reset()
        {
            Result = default(T);
        }

        public K SetVersion(int version)
        {
            Result.Version = version;
            return (K)this;
        }
    }
}
