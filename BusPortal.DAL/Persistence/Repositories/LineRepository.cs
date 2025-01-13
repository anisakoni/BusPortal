using BusPortal.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Identity.Client;
namespace BusPortal.DAL.Persistence.Repositories
{
    public interface ILineRepository : _IBaseRepository<Line, Guid>
    {
        //Line? GetByName(string name);
    }

    internal class LineRepository : _BaseRepository<Line, Guid>, ILineRepository
    {
        public LineRepository(DALDbContext dbContext) : base(dbContext)
        {
           
        }
        public new Line GetById(Guid id)
        {
            return base.GetById(id);
        }
        //public IEnumerable<Line> FilterByStartCity(string startCity)
        //{
        //    return _dbSet.Where(x => x.StartCity.ToLower().Contains(startCity.ToLower())).ToList();
        //}

        //public Line? GetByName(string name)
        //{
        //    return _dbSet.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        //}

    }
}
