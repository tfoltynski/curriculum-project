using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Auction.View.API.Hubs
{
    public class ProductHub : Hub<IProductHub>
    {
        public ProductHub()
        {
        }

        public async Task JoinUserGroup(string user)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, user);
        }
    }
}
