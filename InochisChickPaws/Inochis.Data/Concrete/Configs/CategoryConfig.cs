using Inochis.Entity.Concrete;
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
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c=>c.Description).IsRequired().HasMaxLength(500);
            builder.Property(c => c.Url).IsRequired().HasMaxLength(50);
            builder.ToTable("Categories");
            builder.HasData(
                    new Category
                    {
                        Id = 1,
                        Name = "Tüm Ürünlerimiz",
                        Description = "",
                        Url = "tum-urun"
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "Kolyelerimiz",
                        Description = "",
                        Url = "kolye"
                    },
                    new Category
                    {
                        Id = 3,
                        Name = "Bileklik-Charmlarımız",
                        Description = "",
                        Url = "bileklik-charm"
                    },
                    new Category
                    {
                        Id = 4,
                        Name = "Küpelerimiz",
                        Description = "",
                        Url = "kupe"
                    },
                    new Category
                    {
                        Id = 5,
                        Name = "İndirimli Ürünler",
                        Description = "",
                        Url = "indirim"
                    }
                );
        }
    }
}
