using System;
namespace Catalog.Api.Entities
{

    public record Item : IEntity

    {
        public string Id { get; init; }

        public string Name { get; init; }

        public decimal Price { get; init; }

        public DateTimeOffset CreatedDate { get; set; }


    }
}