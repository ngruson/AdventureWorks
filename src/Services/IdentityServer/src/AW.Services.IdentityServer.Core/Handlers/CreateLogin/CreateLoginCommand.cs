using AutoMapper;
using AW.SharedKernel.AutoMapper;
using MediatR;

namespace AW.Services.IdentityServer.Core.Handlers.CreateLogin
{
    public class CreateLoginCommand : IRequest
    {
        public string CustomerNumber { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}