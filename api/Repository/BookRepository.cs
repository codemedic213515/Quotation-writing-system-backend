using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Book;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDBContext _context;
        public BookRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync(BookQueryObject query)
        {
            var books = _context.Books.Include(c => c.Publisher).Include(a => a.Item).ThenInclude(b => b.Type).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                books = books.Where(s => s.Item.Name.Contains(query.Name));
            };

            if (!string.IsNullOrWhiteSpace(query.Subject))
            {
                books = books.Where(s => s.Subject.Contains(query.Subject));
            };

            if (!string.IsNullOrWhiteSpace(query.Type))
            {
                books = books.Where(s => s.Item.Type.Name.Contains(query.Type));
            };

            if (!string.IsNullOrWhiteSpace(query.ISBN))
            {
                books = books.Where(s => s.ISBN.Contains(query.ISBN));
            };

             if (!string.IsNullOrWhiteSpace(query.PublisherName))
            {
                books = books.Where(s => s.Publisher.Name.Contains(query.PublisherName));
            };           

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    books = query.IsDecsending ? books.OrderByDescending(s => s.Item.Name) : books.OrderBy(s => s.Item.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await books.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Item).Include(a => a.Publisher).FirstOrDefaultAsync(c => c.ItemId == id);
        }

        public async Task<Book> CreateAsync(Book bookModel)
        {
            await _context.Books.AddAsync(bookModel);
            await _context.SaveChangesAsync();
            return bookModel;
        }

        public async Task<Book?> UpdateAsync(int id, Book bookModel)
        {
            var existingBook = await _context.Books.FindAsync(id);

            if (existingBook == null)
            {
                return null;
            }

            existingBook.Subject = bookModel.Subject;
            existingBook.ISBN = bookModel.ISBN;
            existingBook.PublisherId = bookModel.PublisherId;           

            await _context.SaveChangesAsync();

            return existingBook;
        }

        public async Task<Book?> DeleteAsync(int id)
        {
            var bookModel = await _context.Books.FirstOrDefaultAsync(x => x.ItemId == id);

            if (bookModel == null)
            {
                return null;
            }

            _context.Books.Remove(bookModel);
            await _context.SaveChangesAsync();
            return bookModel;
        }
    }
}