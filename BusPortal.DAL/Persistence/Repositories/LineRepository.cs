using BusPortal.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusPortal.DAL.Persistence.Repositories
{
    
    public interface ILineRepository : _IBaseRepository<Line, Guid>
    {
        Task AddAsync(Line entity); 
        Task<List<Line>> GetAllAsync(); 
        Task<Line> GetByIdAsync(Guid id); 
        void Remove(Line line);
        Task SaveChangesAsync(); 
        void Update(Line entity);
    }

    internal class LineRepository : _BaseRepository<Line, Guid>, ILineRepository
    {
        private readonly DALDbContext _dbContext;

        public LineRepository(DALDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        
        public async Task AddAsync(Line entity)
        {
            await _dbContext.Set<Line>().AddAsync(entity);
        }

        
        public async Task<List<Line>> GetAllAsync()
        {
            return await _dbContext.Set<Line>().AsNoTracking().ToListAsync();
        }

        
        public async Task<Line> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<Line>().FindAsync(id) ?? throw new InvalidOperationException("Entity not found");
        }

        public void Remove(Line line)
        {
            throw new NotImplementedException();
        }

        
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Line entity)
        {
            _dbContext.Update(entity);
        }
    }
}
