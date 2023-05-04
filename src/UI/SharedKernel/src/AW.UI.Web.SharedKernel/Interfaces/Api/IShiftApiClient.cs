using AW.UI.Web.SharedKernel.Shift.Handlers.DeleteShift;
using AW.UI.Web.SharedKernel.Shift.Handlers.UpdateShift;

namespace AW.UI.Web.SharedKernel.Interfaces.Api
{
    public interface IShiftApiClient
    {
        Task<Shift.Handlers.GetShift.Shift?> GetShift(string name);
        Task<List<Shift.Handlers.GetShifts.Shift>?> GetShifts();
        Task<Shift.Handlers.CreateShift.Shift?> CreateShift(Shift.Handlers.CreateShift.Shift shift);
        Task<Shift.Handlers.UpdateShift.Shift?> UpdateShift(UpdateShiftCommand command);
        Task DeleteShift(DeleteShiftCommand request);
    }
}
