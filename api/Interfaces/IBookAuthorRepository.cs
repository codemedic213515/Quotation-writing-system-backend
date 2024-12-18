using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IBookAuthorRepository
    {
        Task<List<BookAuthor>> GetAllAsync(BookAuthorQueryObject query);
        Task<BookAuthor?> GetByIdAsync(int bookId, int authorId);
        Task<BookAuthor> CreateAsync(BookAuthor bookAuthorModel);
        Task<BookAuthor?> DeleteAsync(int bookId, int authorId);
    }
}