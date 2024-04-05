using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Inochis.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Data.Concrete.Configs
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p=>p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p=>p.Properties).IsRequired().HasMaxLength(500);
            builder.Property(p=>p.Url).IsRequired().HasMaxLength(100);
            builder.Property(p=>p.ImageUrl).IsRequired().HasMaxLength(500);
            builder.Property(p => p.Price).IsRequired().HasColumnType("real"); 
       
            builder.Property(p => p.CreatedDate).HasDefaultValueSql("date('now')"); 
            builder.Property(p => p.ModifiedDate).HasDefaultValueSql("date('now')"); 
         
           
        }
    }
}
