namespace AW.UI.Web.SharedKernel.Interfaces.Api
{
    public interface IShiftApiClient
    {
        Task<List<Shift.Handlers.GetShifts.Shift>?> GetShifts();
    }
}
