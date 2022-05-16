namespace Auction.Test.Shared.Builders
{
    public interface IBuilder<T>
    {
        void Reset();

        T GetResult();
    }
}
