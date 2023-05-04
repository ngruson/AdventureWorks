using AW.UI.Web.Admin.Mvc.ViewModels.Shift;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface IShiftService
    {
        Task<List<ShiftViewModel>> GetShifts();
        Task<ShiftViewModel> GetDetail(string name);
        Task CreateShift(ShiftViewModel viewModel);
        Task UpdateShift(ShiftViewModel viewModel);
        Task DeleteShift(string name);
    }
}
