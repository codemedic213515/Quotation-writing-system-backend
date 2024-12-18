using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Item;
using api.Models;

namespace api.Mappers
{
    public static class ItemMappers
    {
        public static ItemDto ToItemDto(this Item itemModel)
        {
            return new ItemDto
            {
                Id = itemModel.Id,
                Name = itemModel.Name,
                //TypeId = itemModel.TypeId,
                Type = itemModel.Type,
                ImageFileSpec = itemModel.ImageFileSpec,
                Description = itemModel.Description,
                UnitCost = itemModel.UnitCost,
                UnitPrice = itemModel.UnitPrice
            };
        }
    
        public static Item ToItemFromCreate(this CreateItemDto itemDto)
        {
            return new Item
            {
                Name = itemDto.Name,
                TypeId = itemDto.TypeId,
                ImageFileSpec = itemDto.ImageFileSpec,
                Description = itemDto.Description,
                UnitCost = itemDto.UnitCost,
                UnitPrice = itemDto.UnitPrice
            };
        }

        public static Item ToItemFromUpdate(this UpdateItemRequestDto itemDto)
        {
            return new Item
            {
                Name = itemDto.Name,
                TypeId = itemDto.TypeId,
                ImageFileSpec = itemDto.ImageFileSpec,
                Description = itemDto.Description,
                UnitCost = itemDto.UnitCost,
                UnitPrice = itemDto.UnitPrice
            };
        }
    }
}
