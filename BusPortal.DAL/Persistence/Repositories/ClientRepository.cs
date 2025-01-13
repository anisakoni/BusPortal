using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusPortal.DAL.Persistence.Entities;

namespace BusPortal.DAL.Persistence.Repositories
{
    public interface IClientRepository : _IBaseRepository<Client, Guid>
    {
    }
    internal class ClientRepository : _BaseRepository<Client, Guid>, IClientRepository
    {
        public ClientRepository(DALDbContext dbContext) : base(dbContext)
        {
        }
    }
}
