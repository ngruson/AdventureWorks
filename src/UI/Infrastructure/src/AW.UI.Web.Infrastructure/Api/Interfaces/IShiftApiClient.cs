namespace AW.UI.Web.Infrastructure.Api.Interfaces
{
    public interface IShiftApiClient
    {
        Task<Shift.Handlers.GetShift.Shift?> GetShift(Guid objectId);
        Task<List<Shift.Handlers.GetShifts.Shift>?> GetShifts();
        Task<Shift.Handlers.CreateShift.CreatedShift?> CreateShift(Shift.Handlers.CreateShift.Shift shift);
        Task<Shift.Handlers.UpdateShift.Shift?> UpdateShift(Shift.Handlers.UpdateShift.Shift shift);
        Task DeleteShift(Guid objectId);
    }
}
