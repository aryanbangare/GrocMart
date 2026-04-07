using GrocMart.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrocMart.Persistence.Configurations
    {
        public class OrderItemsEntityConfiguration : IEntityTypeConfiguration<OrderItems>
        {
            public void Configure(EntityTypeBuilder<OrderItems> builder)
            {
                
                builder.HasKey(oi => oi.ID);

                
                builder.Property(oi => oi.Quantity)
                       .IsRequired();

                
                builder.HasOne(oi => oi.orders)
                       .WithMany(o => o.OrderItems)
                       .HasForeignKey(oi => oi.OrderID)
                       .OnDelete(DeleteBehavior.Cascade);

                
                builder.HasOne(oi => oi.products)
                       .WithMany(p => p.OrderItems)
                       .HasForeignKey(oi => oi.ProductID)
                       .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }

