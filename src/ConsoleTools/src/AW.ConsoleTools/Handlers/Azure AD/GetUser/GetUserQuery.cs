using MediatR;

namespace AW.ConsoleTools.Handlers.AzureAD.GetUser
{
    public class GetUserQuery : IRequest<User?>
    {
        public GetUserQuery(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; init; }
    }
}