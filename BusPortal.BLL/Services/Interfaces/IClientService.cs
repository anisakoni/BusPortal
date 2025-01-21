using BusPortal.Common.Models;

namespace BusPortal.BLL.Services.Interfaces
{
    public interface IClientService
    {
        Task<bool> AuthenticateClient(LoginViewModel viewModel);
        Task<bool> RegisterClient(RegisterViewModel viewModel);
        Task Logout();
    }
}
