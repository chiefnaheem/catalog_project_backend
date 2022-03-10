using System;

namespace Catalog.Api.Dtos
{
	public record ItemDto
	
	{
		public string Id { get; init; }
		
		public string Name { get; init; }
		
		public decimal Price { get; init; }
		
		public DateTimeOffset CreatedDate { get; set; }
		
	}
}