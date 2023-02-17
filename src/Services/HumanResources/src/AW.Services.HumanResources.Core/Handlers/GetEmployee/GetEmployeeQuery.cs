using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.GetEmployee
{
    public class GetEmployeeQuery : IRequest<Employee>
    {
        public GetEmployeeQuery()
        {
        }
        public GetEmployeeQuery(string loginID)
        {
            LoginID = loginID;
        }

        public string? LoginID { get; set; }
    }
}
