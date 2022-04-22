using AW.SharedKernel.ValueTypes;
using MediatR;

namespace AW.Services.IdentityServer.Core.Handlers.CreateLogin
{
    public class CreateLoginCommand : IRequest
    {
        public string CustomerNumber { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public NameFactory? Name { get; set; }
    }
}