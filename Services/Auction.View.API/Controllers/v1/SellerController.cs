using Auction.View.API.Models;
using Auction.View.API.Models.Common;
using Auction.View.API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.View.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public SellerController(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository ?? throw new System.ArgumentNullException(nameof(productRepository));
            this.mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        [HttpGet("show-product/{productId}")]
        public async Task<ActionResult> ShowProduct([FromRoute] string productId)
        {
            var product = await productRepository.GetByIdAsync(p => p.ProductId == productId, p => p.Include(p => p.SellerInformation).Include(p => p.Bids).ThenInclude(p => p.BuyerInformation));
            var response = mapper.Map<ShowProductDetailsResponse>(product);
            response.Bids = response.Bids.OrderByDescending(p => p.Amount).ToList();
            return Ok(response);
        }

        [HttpGet("show-products")]
        public async Task<ActionResult> ShowProducts([FromQuery] PaginationInput pagination)
        {
            var product = await productRepository.GetAsync(orderBy: p => p.OrderByDescending(p => p.CreatedDate), include: p => p.Include(p => p.SellerInformation).Include(p => p.Bids).ThenInclude(p => p.BuyerInformation), pageNumber: pagination.PageNumber, pageSize: pagination.PageSize);
            var response = new ShowProductsResponse
            {
                Products = new PagedList<ShowProductsResponse.ProductDto>(product.TotalCount, product.CurrentPage, product.PageSize)
                {
                    Results = product.Results.Select(mapper.Map<ShowProductsResponse.ProductDto>).ToList()
                }
            };
            return Ok(response);
        }
    }
}
