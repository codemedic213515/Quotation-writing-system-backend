using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Book;
using api.Models;

namespace api.Mappers
{
    public static class BookMappers
    {
        public static BookDto ToBookDto(this Book bookModel)
        {
            return new BookDto
            {
                Item = bookModel.Item,
                ISBN = bookModel.ISBN,
                Subject = bookModel.Subject,
                Publisher = bookModel.Publisher
            };
        }

        public static Book ToBookFromCreate(this CreateBookDto bookDto)
        {
            return new Book
            {
                ItemId = bookDto.ItemId,
                Subject = bookDto.Subject,
                ISBN = bookDto.ISBN,
                PublisherId = bookDto.PublisherId
            };
        }

        public static Book ToBookFromUpdate(this UpdateBookRequestDto bookDto)
        {
            return new Book
            {
                Subject = bookDto.Subject,
                ISBN = bookDto.ISBN,
                PublisherId = bookDto.PublisherId
            };
        }
    }
}