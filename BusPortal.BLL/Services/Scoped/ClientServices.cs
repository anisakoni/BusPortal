using AutoMapper;
using BusPortal.BLL.Services.Interfaces;
using BusPortal.Common.Models;
using BusPortal.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

public class ClientService : IClientService
{
    private readonly DALDbContext _dbContext;
    private readonly IMapper _mapper;

    public ClientService(DALDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<bool> AuthenticateClient(LoginViewModel viewModel)
    {
       
        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Email == viewModel.Username);
        if (client == null)
        {
            return false; 
        }

        
        if (VerifyPassword(viewModel.Password, client.Password))
        {
            return true; 
        }

        return false; 
    }

    public Task Logout()
    {
        // Implement logout logic 
        return Task.CompletedTask;
    }

    public async Task<bool> RegisterClient(RegisterViewModel viewModel)
    {
       
        var existingClient = await _dbContext.Clients.FirstOrDefaultAsync(u => u.Email == viewModel.Email);
        if (existingClient != null)
        {
            return false; 
        }

      
        var newClientBLL = new BusPortal.BLL.Domain.Models.Client
        {
            Id = Guid.NewGuid(),
            Name = viewModel.Name,
            Email = viewModel.Email,
            Password = HashPassword(viewModel.Password), 
            Admin = false 
        };

       
        var newClientDAL = _mapper.Map<BusPortal.DAL.Persistence.Entities.Client>(newClientBLL);

        
        await _dbContext.Clients.AddAsync(newClientDAL);
        await _dbContext.SaveChangesAsync();

        return true; 
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    private bool VerifyPassword(string inputPassword, string storedHashedPassword)
    {
        return HashPassword(inputPassword) == storedHashedPassword;
    }
}
