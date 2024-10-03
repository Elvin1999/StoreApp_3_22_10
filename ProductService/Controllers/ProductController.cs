using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductService.Dtos;
using ProductService.Entities;
using ProductService.Repository;
using ProductService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;
        private IProductExtentionServices _extentionServices;
        private IMapper _mapper;

        public ProductController(IProductRepository productRepository, IProductExtentionServices extentionServices, IMapper mapper)
        {
            _productRepository = productRepository;
            _extentionServices = extentionServices;
            _mapper = mapper;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get()
        {
            var result = await _productRepository.GetAll();
            var dtos = _mapper.Map<IEnumerable<ProductDto>>(result);
            return dtos;
        }

        [HttpPost]
        public async Task<ProductDto> Post([FromBody]ProductDto productDto)
        {
            var item = _mapper.Map<Product>(productDto);
            await _productRepository.AddProduct(item);
            var dto=_mapper.Map<ProductDto>(item);

            return dto;
        }

        [HttpGet("GetImage/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var result=await _extentionServices.GetProductImageAsync(id);
            return Ok(result);
        }

    }
}
