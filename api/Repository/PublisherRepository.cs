using System.Threading.Tasks;
using api.Data;
using api.Dtos.Publisher;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly ApplicationDBContext _context;
        public PublisherRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Publisher>> GetAllAsync()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publisher?> GetByIdAsync(int id)
        {
            return await _context.Publishers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Publisher> CreateAsync(Publisher PublisherModel)
        {
            await _context.Publishers.AddAsync(PublisherModel);
            await _context.SaveChangesAsync();
            return PublisherModel;
        }

        public async Task<Publisher?> UpdateAsync(int id, Publisher PublisherModel)
        {
            var existingPublisher = await _context.Publishers.FindAsync(id);

            if (existingPublisher == null)
            {
                return null;
            }

            existingPublisher.Name = PublisherModel.Name;

            await _context.SaveChangesAsync();

            return existingPublisher;
        }

        public async Task<Publisher?> DeleteAsync(int id)
        {
            var PublisherModel = await _context.Publishers.FirstOrDefaultAsync(x => x.Id == id);

            if (PublisherModel == null)
            {
                return null;
            }

            _context.Publishers.Remove(PublisherModel);
            await _context.SaveChangesAsync();
            return PublisherModel;
        }
    }
}