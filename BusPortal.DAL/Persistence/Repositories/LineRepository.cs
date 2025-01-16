using BusPortal.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Identity.Client;
namespace BusPortal.DAL.Persistence.Repositories
{
    public interface ILineRepository : _IBaseRepository<Line, Guid>
    {
    }

    internal class LineRepository : _BaseRepository<Line, Guid>, ILineRepository
    {
        public LineRepository(DALDbContext dbContext) : base(dbContext)
        {
        }
    }
}
