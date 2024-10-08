﻿using Microsoft.AspNetCore.Mvc;
using SearchService.Dtos;
using SearchService.Repository;
using SearchService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SearchService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IProductService _productService;

        public SearchController(ISearchRepository searchRepository, IProductService productService)
        {
            _searchRepository = searchRepository;
            _productService = productService;
        }

        // GET: api/<SearchController>
        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            var item = await _searchRepository.GetAsync(code);
            if (item == null)
            {
                return NotFound();
            }

            var dto = new BarcodeDto
            {
                Code = item.Code,
                ProductName = item.ProductName,
                TotalPrice = item.TotalPrice,
                Volume = item.Volume,
                Id = item.Id,
            };

            var productId=item.ProductId;
            dto.ImageUrl=await _productService.GetProductImagePathAsync(productId);

            return Ok(dto);
        }


    }
}
