//using Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Infrastructure.Configuration
//{
//    internal class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
//    {
//        public void Configure(EntityTypeBuilder<ProductBrand> builder)
//        {
//            builder.HasMany(b => b.Products)
//                .WithOne(p => p.ProductBrand);

//            builder.Property(b => b.Name)
//                .IsRequired()
//                .HasMaxLength(100);

//        }
//    }
//}
