using Ardalis.GuardClauses;
using AutoMapper;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Admin.Mvc.ViewModels.Shift;
using AW.UI.Web.Infrastructure.Api.Shift.Handlers.CreateShift;
using AW.UI.Web.Infrastructure.Api.Shift.Handlers.DeleteShift;
using AW.UI.Web.Infrastructure.Api.Shift.Handlers.GetShift;
using AW.UI.Web.Infrastructure.Api.Shift.Handlers.GetShifts;
using AW.UI.Web.Infrastructure.Api.Shift.Handlers.UpdateShift;
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
            var shift = _mapper.Map<Infrastructure.Api.Shift.Handlers.CreateShift.Shift>(viewModel);

            _logger.LogInformation("Send command to add shift");
            await _mediator.Send(new CreateShiftCommand(shift));
            _logger.LogInformation("Command was succesfully executed");
        }

        public async Task DeleteShift(Guid objectId)
        {
            _logger.LogInformation("Deleting shift");
            await _mediator.Send(new DeleteShiftCommand(objectId));
            _logger.LogInformation("Shift successfully deleted");
        }

        private async Task<Infrastructure.Api.Shift.Handlers.GetShift.Shift> GetShift(Guid objectId)
        {
            _logger.LogInformation("Getting shift");
            var shift = await _mediator.Send(new GetShiftQuery(objectId));
            _logger.LogInformation("Retrieved shift");
            Guard.Against.Null(shift, _logger);

            return shift!;
        }

        public async Task<ShiftViewModel> GetDetail(Guid objectId)
        {
            var shift = await GetShift(objectId);
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
            var shift = await GetShift(viewModel.ObjectId);
            var shiftToUpdate = _mapper.Map<Infrastructure.Api.Shift.Handlers.UpdateShift.Shift>(shift);
            _mapper.Map(viewModel, shiftToUpdate);

            _logger.LogInformation("Updating shift");
            await _mediator.Send(new UpdateShiftCommand(shiftToUpdate));
            _logger.LogInformation("Shift updated successfully");
        }
    }
}
