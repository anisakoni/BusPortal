using BusPortal.BLL.Services.Interfaces;
using BusPortal.Common.Models;
using BusPortal.DAL.Persistence.Entities;
using BusPortal.DAL.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusPortal.BLL.Services.Scoped
{
    public class LinesService : ILinesService
    {
        private readonly ILineRepository _lineRepository;

        public LinesService(ILineRepository lineRepository)
        {
            _lineRepository = lineRepository ?? throw new ArgumentNullException(nameof(lineRepository));
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

            await _lineRepository.AddAsync(line);
            await _lineRepository.SaveChangesAsync(); 
        }

        public async Task<List<Line>> GetAllLinesAsync()
        {
            return await _lineRepository.GetAllAsync(); 
        }

        public async Task<Line> GetLineByIdAsync(Guid id)
        {
            return await _lineRepository.GetByIdAsync(id); 
        }

        public async Task UpdateLineAsync(Line viewModel)
        {
            var line = await _lineRepository.GetByIdAsync(viewModel.Id);

            if (line != null)
            {
                line.StartCity = viewModel.StartCity;
                line.DestinationCity = viewModel.DestinationCity;
                line.DepartureTimes = viewModel.DepartureTimes;

                _lineRepository.Update(line); 
                await _lineRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteLineAsync(Guid id)
        {
            var line = await _lineRepository.GetByIdAsync(id);

            if (line != null)
            {
                _lineRepository.Remove(line); 
                await _lineRepository.SaveChangesAsync();
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
