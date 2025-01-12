using Microsoft.AspNetCore.Mvc;
using BusPortal.Web.Data;
using BusPortal.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;
using BusPortal.Web.Models.DTO;
namespace BusPortal.Web.Controllers
{
    public class LinesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public LinesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLineViewModel viewModel)
        {
            var line = new Line {
                StartCity = viewModel.StartCity,
                DestinationCity = viewModel.DestinationCity,
                DepartureTimes = viewModel.DepartureTimes,
            };

            await dbContext.Lines.AddAsync(line);

            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Lines");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var lines = await dbContext.Lines.ToListAsync();

            return View(lines);
            
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var line = await dbContext.Lines.FindAsync(id);

            return View(line);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Line viewModel)
        {
            var line = await dbContext.Lines
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if (line is not null)
            {

                line.StartCity = viewModel.StartCity;
                line.DestinationCity = viewModel.DestinationCity;
                line.DepartureTimes = viewModel.DepartureTimes;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Lines");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Line viewModel)
        {
            var line = await dbContext.Lines.FindAsync(viewModel.Id);

            if (line is not null)
            {
                dbContext.Lines.Remove(line);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Lines");
        }
    }
}
