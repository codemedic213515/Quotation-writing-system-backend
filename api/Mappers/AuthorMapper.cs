using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Author;
using api.Models;

namespace api.Mappers
{
    public static class AuthorMappers
    {
        public static AuthorDto ToAuthorDto(this Author authorModel)
        {
            return new AuthorDto
            {
                Id = authorModel.Id,
                Name = authorModel.Name
            };
        }

        public static Author ToAuthorFromCreate(this CreateAuthorDto authorDto)
        {
            return new Author
            {
                Name = authorDto.Name,
            };
        }

        public static Author ToAuthorFromUpdate(this UpdateAuthorRequestDto authorDto)
        {
            return new Author
            {
                Name = authorDto.Name,
            };
        }
    }
}