using AutoMapper;
using BusPortal.BLL.Services.Interfaces;
using BusPortal.Common.Models;
using BusPortal.DAL.Persistence.Entities;
using BusPortal.DAL.Persistence.Repositories;
using Microsoft.AspNetCore.Identity; // For PasswordHasher
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusPortal.BLL.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Client> _passwordHasher;

        public ClientService(IClientRepository clientRepository, IMapper mapper, IPasswordHasher<Client> passwordHasher)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _mapper = mapper;
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public async Task<bool> RegisterClient(RegisterViewModel model)
        {
            var clientEntity = _mapper.Map<Client>(model);

            // Check if the client already exists
            var existingClient = _clientRepository.GetAll().FirstOrDefault(c => c.Email == clientEntity.Email);
            if (existingClient != null)
            {
                return false; // Client already exists
            }

            clientEntity.Password = _passwordHasher.HashPassword(clientEntity, model.Password);

            // Add the client to the repository
            _clientRepository.Add(clientEntity);
            _clientRepository.SaveChanges();
            return true;
        }

        public async Task<bool> AuthenticateClient(LoginViewModel model)
        {
            // Find the client by email
            var clientEntity = _clientRepository.GetAll().FirstOrDefault(c => c.Email == model.Email);
            if (clientEntity == null)
            {
                return false; // Client does not exist
            }

            // Verify the password hash
            var result = _passwordHasher.VerifyHashedPassword(clientEntity, clientEntity.Password, model.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return false; // Invalid password
            }

            return true;
        }

        public Task Logout()
        {
            // Logout logic to be implemented
            throw new NotImplementedException();
        }
    }
}
