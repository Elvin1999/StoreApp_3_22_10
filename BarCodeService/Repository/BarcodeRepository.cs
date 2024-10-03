using BarCodeService.DataContext;
using BarCodeService.Dtos;
using BarCodeService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BarCodeService.Repository
{
    public class BarcodeRepository : IBarcodeRepository
    {
        private readonly BarcodeDbContext _context;

        public BarcodeRepository(BarcodeDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddBarcodeAsync(ProductItemDto dto)
        {
            var item = await _context.Barcodes.FirstOrDefaultAsync(b => b.ProductId == dto.ProductId && b.Volume == dto.Volume);
            if (item == null)
            {
                var data = new Barcode
                {
                    Code = $"4-12345:{dto.ProductId}-{dto.Volume}",
                    ProductName = dto.ProductName,
                    ProductId = dto.ProductId,
                    Volume = dto.Volume,
                    TotalPrice = dto.Volume * dto.Price
                };
                await _context.Barcodes.AddAsync(data);
                await _context.SaveChangesAsync();
                return data.Code;
            }
            return item.Code ?? "";
        }
    }
}
