using BarCodeService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarCodeService.DataContext
{
    public class BarcodeDbContext:DbContext
    {
        public BarcodeDbContext(DbContextOptions<BarcodeDbContext> options)
            :base(options)
        {
            
        }
        public DbSet<Barcode> Barcodes { get; set; }
    }
}
