using BusPortal.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusPortal.Common.Models;
using System.Threading.Tasks;

namespace BusPortal.BLL.Services.Interfaces
{
    public interface IClientService
    {
        Task<bool> AuthenticateClient(LoginViewModel viewModel);
        Task<bool> RegisterClient(RegisterViewModel viewModel);
        Task Logout();
    }
}
