using FoodManager.Catalog.Domain.ValueObjects;
using FoodManager.Internal.Shared.Enums;

namespace FoodManager.Catalog.Domain.Entities
{
    public class FoodEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string Tenant { get; set; } = string.Empty;
        public int Assessment { get; set; }
        public Category Category { get; set; }
        public FoodImage FoodImage { get; set; } = default!;

        public FoodEntity SetId(Guid id)
        {
            Id = id;
            return this;
        }

        public FoodEntity SetName(string name)
        {
            Name = name;
            return this;
        }

        public FoodEntity SetPrice(decimal price)
        {
            Price = price;
            return this;
        }

        public FoodEntity SetDescription(string description)
        {
            Description = description;
            return this;
        }
        public FoodEntity SetAssessment(int assessment)
        {
            Assessment = assessment;
            return this;
        }

        public FoodEntity SetCategory(Category category)
        {
            Category = category;
            return this;
        }

        public FoodEntity SetTenant(string tenant)
        {
            Tenant = tenant;
            return this;
        }

        public FoodEntity SetImageFile(FoodImage foodImage)
        {
            FoodImage = foodImage;
            return this;
        }

        public Builder ToBuilder()
        {
            return new Builder
            {
                Name = Name,
                Price = Price,
                Description = Description,
                Assessment = Assessment,
                Category = Category,
                Tenant = Tenant,
                FoodImage = FoodImage
            };
        }

        public class Builder
        {
            public Guid Id { get; set; }
            public string? Name { get; set; }
            public decimal Price { get; set; }
            public string? Description { get; set; }
            public string? Tenant { get; set; }
            public int Assessment { get; set; }
            public Category Category { get; set; }
            public FoodImage FoodImage { get; set; } = default!;

            public static Builder Create() => new();

            public Builder SetId(Guid id) { Id = id; return this; }
            public Builder SetName(string name) { Name = name; return this; }
            public Builder SetPrice(decimal price) { Price = price; return this; }
            public Builder SetDescription(string description) { Description = description; return this; }
            public Builder SetTenant(string tenant) { Tenant = tenant; return this; }
            public Builder SetAssessment(int assesment) { Assessment = assesment; return this; }
            public Builder SetCategory(Category category) { Category = category; return this; }
            public Builder SetImageFile(FoodImage foodImage) { FoodImage = foodImage; return this; }

            public FoodEntity Build()
            {
                return new FoodEntity
                {
                    Id = Id,
                    Name = Name,
                    Price = Price,
                    Description = Description,
                    Assessment = Assessment,
                    Category = Category,
                    Tenant = Tenant ?? string.Empty,
                    FoodImage = FoodImage
                };
            }
        }
    }
}