using Ardalis.GuardClauses;
using AutoMapper;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Admin.Mvc.ViewModels.Shift;
using AW.UI.Web.SharedKernel.Shift.Handlers.CreateShift;
using AW.UI.Web.SharedKernel.Shift.Handlers.DeleteShift;
using AW.UI.Web.SharedKernel.Shift.Handlers.GetShift;
using AW.UI.Web.SharedKernel.Shift.Handlers.GetShifts;
using AW.UI.Web.SharedKernel.Shift.Handlers.UpdateShift;
using MediatR;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public class ShiftService : IShiftService
    {
        private readonly ILogger<ShiftService> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ShiftService(
            ILogger<ShiftService> logger,
            IMapper mapper,
            IMediator mediator
        )
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task CreateShift(ShiftViewModel viewModel)
        {
            var shift = _mapper.Map<SharedKernel.Shift.Handlers.CreateShift.Shift>(viewModel);

            _logger.LogInformation("Send command to add shift");
            await _mediator.Send(new CreateShiftCommand(shift));
            _logger.LogInformation("Command was succesfully executed");
        }

        public async Task DeleteShift(string name)
        {
            _logger.LogInformation("Deleting shift");
            await _mediator.Send(new DeleteShiftCommand(name));
            _logger.LogInformation("Shift successfully deleted");
        }

        private async Task<SharedKernel.Shift.Handlers.GetShift.Shift> GetShift(string name)
        {
            _logger.LogInformation("Getting shift");
            var shift = await _mediator.Send(new GetShiftQuery(name));
            _logger.LogInformation("Retrieved shift");
            Guard.Against.Null(shift, _logger);

            return shift!;
        }

        public async Task<ShiftViewModel> GetDetail(string name)
        {
            var shift = await GetShift(name);
            var shiftViewModel = _mapper.Map<ShiftViewModel>(shift);
            _logger.LogInformation("Returning shift");
            return shiftViewModel;
        }

        public async Task<List<ShiftViewModel>> GetShifts()
        {
            _logger.LogInformation("Getting shifts");
            var response = await _mediator.Send(new GetShiftsQuery());

            var vm = _mapper.Map<List<ShiftViewModel>>(response);

            _logger.LogInformation("Returning {ViewModel}", vm);
            return vm;
        }

        public async Task UpdateShift(ShiftViewModel viewModel)
        {
            var shift = await GetShift(viewModel.Name!);
            var shiftToUpdate = _mapper.Map<SharedKernel.Shift.Handlers.UpdateShift.Shift>(shift);
            _mapper.Map(viewModel, shiftToUpdate);

            _logger.LogInformation("Updating shift");
            await _mediator.Send(new UpdateShiftCommand(viewModel.Name!, shiftToUpdate));
            _logger.LogInformation("Shift updated successfully");
        }
    }
}
