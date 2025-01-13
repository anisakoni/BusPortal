using BusPortal.BLL.Services.Interfaces;
using BusPortal.Common.Models;
using BusPortal.DAL.Persistence;
using BusPortal.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusPortal.BLL.Services.Scoped
{
    public class LinesService : ILinesService
    {
        private readonly DALDbContext _dbContext;

        public LinesService(DALDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddLineAsync(AddLineViewModel viewModel)
        {
            var line = new Line
            {
                Id = Guid.NewGuid(),
                StartCity = viewModel.StartCity,
                DestinationCity = viewModel.DestinationCity,
                DepartureTimes = viewModel.DepartureTimes,
            };

            await _dbContext.Lines.AddAsync(line);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Line>> GetAllLinesAsync()
        {
            return await _dbContext.Lines.ToListAsync();
        }

        public async Task<Line> GetLineByIdAsync(Guid id)
        {
            return await _dbContext.Lines.FindAsync(id);
        }

        public async Task UpdateLineAsync(Line viewModel)
        {
            var line = await _dbContext.Lines.FindAsync(viewModel.Id);

            if (line != null)
            {
                line.StartCity = viewModel.StartCity;
                line.DestinationCity = viewModel.DestinationCity;
                line.DepartureTimes = viewModel.DepartureTimes;

                _dbContext.Lines.Update(line);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteLineAsync(Guid id)
        {
            var line = await _dbContext.Lines.FindAsync(id);

            if (line != null)
            {
                _dbContext.Lines.Remove(line);
                await _dbContext.SaveChangesAsync();
            }
        }

        Task<List<Domain.Models.Line>> ILinesService.GetAllLinesAsync()
        {
            throw new NotImplementedException();
        }

        Task<Domain.Models.Line> ILinesService.GetLineByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLineAsync(Domain.Models.Line viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
