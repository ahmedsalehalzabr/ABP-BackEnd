using ABPCourse.Demo1.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ABPCourse.Demo1.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // يعمل ماب للدسفولت بريورتي
            builder.ConfigureByConvention();

            // ادخل الاي دي بيدي 
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.NameAr).HasMaxLength(Demo1Consts.GeneralTextMaxlength);
            builder.Property(x => x.NameEn).HasMaxLength(Demo1Consts.GeneralTextMaxlength);
            builder.Property(x => x.DescriptionAr).HasMaxLength(Demo1Consts.DescriptionTextMaxLength);
            builder.Property(x => x.DescriptionAr).HasMaxLength(Demo1Consts.DescriptionTextMaxLength);

            // الجدول الذي تريد تحفض فيه تسميه كذا 
            builder.ToTable("Categories");
        }
    }
}
