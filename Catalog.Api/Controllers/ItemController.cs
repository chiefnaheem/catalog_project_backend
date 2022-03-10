using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Api.Dtos;
using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
	[ApiController]
	[Route("items")]
	public class ItemController : ControllerBase
	{
		private readonly IItemRepository repository;
		
		public ItemController(IItemRepository repository) 
		{
			this.repository = repository;
		}
		[HttpGet]
		public async Task<IEnumerable<ItemDto>> GetItemsAsync() 
		{
			var items = (await repository.GetItemsAsync()).Select(item => item.AsDto());
			return items;
		}
		[HttpGet("{id}")]
		public async Task <ActionResult<ItemDto>> GetItemAsync(string id) 
		{
			var item = await repository.GetItemAsync(id);
			if(item is null) 
			{
				return NotFound();
			}
			return item.AsDto();
		}
		
		[HttpPost]
		public async Task <ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
		{
			Item item = new Item() 
			{
				 Id = Guid.NewGuid().ToString(),
				 Name = itemDto.Name,
				 Price = itemDto.Price,
				 CreatedDate = DateTimeOffset.UtcNow
			};
			await repository.CreateItemAsync(item);
			return CreatedAtAction(nameof(GetItemAsync), new {id = item.Id}, item.AsDto());
		}
		
		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateItemAsync(string id, UpdateItemDto itemDto) 
		{
			var existingItem = await repository.GetItemAsync(id);
			if(existingItem is null)
			{
				return NotFound();
			}
			Item updatedItem = existingItem with 
			{
				Name = itemDto.Name,
				Price = itemDto.Price
			};
			await repository.updateItemAsync(updatedItem);
			return NoContent();
		}
		
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteItem(string id) 
		{
			var existingItem = await repository.GetItemAsync(id);
			if(existingItem is null)
			{
				return NotFound();
			}
			await repository.DeleteItemAsync(id);
			return NoContent();
		}
		
	}
}