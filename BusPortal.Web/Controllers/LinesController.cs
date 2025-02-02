using BusPortal.BLL.Domain.Models;
using BusPortal.BLL.Services.Interfaces;
using BusPortal.Common.Models;
using BusPortal.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Line = BusPortal.Web.Models.Entities.Line;

namespace BusPortal.Web.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class LinesController : Controller
    {
        private readonly ILinesService _linesService;

        public LinesController(ILinesService linesService)
        {
            _linesService = linesService ?? throw new ArgumentNullException(nameof(linesService));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLineViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _linesService.AddLineAsync(viewModel);
                return RedirectToAction("List");
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var lines = await _linesService.GetAllLinesAsync();
            var mappedLines = lines.Select(line => new Line
            {
                Id = line.Id,
                StartCity = line.StartCity,
                DestinationCity = line.DestinationCity,
                DepartureTimes = line.DepartureTimes,
                Price = line.Price
                //Date=line.Date
            }).ToList();
            return View(mappedLines);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var line = await _linesService.GetLineByIdAsync(id);

            if (line == null)
            {
                return View("NotFound");
            }

           
            var mappedLine = new Line
            {
                Id = line.Id,
                StartCity = line.StartCity,
                DestinationCity = line.DestinationCity,
                DepartureTimes = line.DepartureTimes,
                Price = line.Price
                // Date = line.Date
            };

            return View(mappedLine);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Domain.Models.Line viewModel)
        {
            if (ModelState.IsValid)
            {
                var lineToUpdate = new BusPortal.BLL.Domain.Models.Line
                {
                    Id = viewModel.Id,
                    StartCity = viewModel.StartCity,
                    DestinationCity = viewModel.DestinationCity,
                    DepartureTimes = viewModel.DepartureTimes,
                    Price = viewModel.Price
                    // Date = viewModel.Date
                };

                await _linesService.UpdateLineAsync(lineToUpdate);
                return RedirectToAction("List");
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _linesService.DeleteLineAsync(id);
            return RedirectToAction("List");
        }
    }
}
