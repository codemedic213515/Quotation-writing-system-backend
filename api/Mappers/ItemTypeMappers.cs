using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ItemType;
using api.Models;

namespace api.Mappers
{
    public static class ItemTypeMappers
    {
        public static ItemTypeDto ToItemTypeDto(this ItemType itemTypeModel)
        {
            return new ItemTypeDto
            {
                Id = itemTypeModel.Id,
                Name = itemTypeModel.Name,
                Description = itemTypeModel.Description,
            };
        }
        
        public static ItemType ToItemTypeFromCreate(this CreateItemTypeDto ItemTypeDto)
        {
            return new ItemType
            {
                Name = ItemTypeDto.Name,
                Description = ItemTypeDto.Description,
            };
        }

        public static ItemType ToItemTypeFromUpdate(this UpdateItemTypeRequestDto ItemTypeDto)
        {
            return new ItemType
            {
                Name = ItemTypeDto.Name,
                Description = ItemTypeDto.Description,
            };
        }
    }
}
