namespace Auction.SharedKernel.Events
{
    public static class EventBusEndpoints
    {
        public const string ProductCreatedEndpoint = "productcreated-queue";
        public const string ProductDeletedEndpoint = "productdeleted-queue";
        public const string BidPlacedEndpoint = "bidplaced-queue";
        public const string BidUpdatedEndpoint = "bidupdated-queue";
    }
}
