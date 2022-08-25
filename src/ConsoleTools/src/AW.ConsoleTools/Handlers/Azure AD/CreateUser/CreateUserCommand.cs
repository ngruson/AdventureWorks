using MediatR;

namespace AW.ConsoleTools.Handlers.AzureAD.CreateUser
{
    public class CreateUserCommand : IRequest<User>
    {
        public CreateUserCommand(string displayName, string mailNickname)
        {
            DisplayName = displayName;
            MailNickname = mailNickname;
        }

        public string DisplayName { get; set; }
        public string MailNickname { get; set; }
    }
}