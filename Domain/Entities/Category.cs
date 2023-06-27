﻿namespace Domain.Entities
{
    public class Category : BaseAuditEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
