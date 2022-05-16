using Auction.SharedKernel.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Auction.SharedKernel.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
