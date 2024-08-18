﻿using ABPCourse.Demo1.Categories;
using Volo.Abp.Domain.Entities.Auditing;

namespace ABPCourse.Demo1.Products
{
    public class Product : FullAuditedEntity<int>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
