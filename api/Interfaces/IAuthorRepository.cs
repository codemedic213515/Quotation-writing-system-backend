using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync(AuthorQueryObject query);   
        Task<Author?> GetByIdAsync(int id);
        Task<Author> CreateAsync(Author AuthorModel);
        Task<Author?> UpdateAsync(int id, Author AuthorModel);
        Task<Author?> DeleteAsync(int id);        
    }
}