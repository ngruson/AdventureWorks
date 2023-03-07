using AW.SharedKernel.AutoMapper;

namespace AW.ConsoleTools.Handlers.AzureAD.GetUser
{
    public class Group : IMapFrom<Microsoft.Graph.Models.Group>
    {
        public Group() { }
        public Group(string id, string displayName)
        {
            Id = id;
            DisplayName = displayName;
        }

        public string? Id { get; init; }
        public string? DisplayName { get; init; }
    }
}
