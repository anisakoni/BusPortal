using BusPortal.Domain.Models;

namespace BusPortal.BLL.Services.Scoped;

public interface ILineService
{
    void Add(Domain.Models.Line lineAddDTO);
}

internal class LineService : ILineService
{
    private readonly ILineService _LineService;
    public void Add(Line lineAddDTO)
    {
        
    }

    void ILineService.Add(Domain.Models.Line lineAddDTO)
    {
        throw new NotImplementedException();
    }
}