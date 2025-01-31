﻿using Microsoft.EntityFrameworkCore;
using BusPortal.DAL.Persistence.Entities;
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
        Task<IEnumerable<string>> GetAllStartCitiesAsync();
        Task<IEnumerable<string>> GetDestinationCitiesForStartCityAsync(string startCity);
        Task<Line> GetLineByRouteAsync(string startCity, string destinationCity);
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

        public async Task<Line?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<Line>().FindAsync(id);
        }

        public void Remove(Line line)
        {
            if (line == null)
            {
                throw new ArgumentNullException(nameof(line), "Line cannot be null.");
            }

            _dbContext.Set<Line>().Remove(line);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Line entity)
        {
            _dbContext.Update(entity);
        }

        public async Task<IEnumerable<string>> GetAllStartCitiesAsync()
        {
            return await _dbContext.Lines
                .Select(l => l.StartCity)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetDestinationCitiesForStartCityAsync(string startCity)
        {
            return await _dbContext.Lines
                .Where(l => l.StartCity == startCity)
                .Select(l => l.DestinationCity)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
        }

        public async Task<Line> GetLineByRouteAsync(string startCity, string destinationCity)
        {
            return await _dbContext.Lines
                .FirstOrDefaultAsync(l => l.StartCity == startCity &&
                                          l.DestinationCity == destinationCity);
        }
    }
}
