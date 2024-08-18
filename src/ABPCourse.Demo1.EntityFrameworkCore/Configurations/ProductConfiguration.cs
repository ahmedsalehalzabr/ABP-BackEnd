using ABPCourse.Demo1.Categories;
using ABPCourse.Demo1.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ABPCourse.Demo1.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ConfigureByConvention();

            builder.Property(x => x.NameAr).HasMaxLength(Demo1Consts.GeneralTextMaxlength);
            builder.Property(x => x.NameEn).HasMaxLength(Demo1Consts.GeneralTextMaxlength);
            builder.Property(x => x.DescriptionAr).HasMaxLength(Demo1Consts.DescriptionTextMaxLength);
            builder.Property(x => x.DescriptionAr).HasMaxLength(Demo1Consts.DescriptionTextMaxLength);

            builder.HasOne(x => x.Category)
                   .WithMany()
                   .HasForeignKey(x => x.CategoryId)
                   .IsRequired();
        }
    }
}
