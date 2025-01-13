using BusPortal.BLL.Domain.Models;
using BusPortal.BLL.Services.Interfaces;
using BusPortal.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BusPortal.Web.Controllers
{
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
            return View(lines);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var line = await _linesService.GetLineByIdAsync(id);

            if (line == null)
            {
                return NotFound();
            }

            return View(line);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Line viewModel)
        {
            if (ModelState.IsValid)
            {
                await _linesService.UpdateLineAsync(viewModel);
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
