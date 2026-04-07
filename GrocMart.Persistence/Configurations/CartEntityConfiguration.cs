using GrocMart.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrocMart.Persistence.Configurations
{
    public sealed class CartEntityConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder
                .HasOne(c => c.Products)
                .WithMany(p => p.Carts)
                .HasForeignKey(c => c.ProductID);
        }
    }
}