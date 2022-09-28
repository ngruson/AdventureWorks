using AW.Services.Product.Core.Handlers.GetAllProductsWithPhotos;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Product.Core.Handlers.StoreProductPhotos
{
    public class StoreProductPhotosCommandHandler : IRequestHandler<StoreProductPhotosCommand>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StoreProductPhotosCommandHandler> _logger;
        private readonly IFileHandler _fileWriter;

        public StoreProductPhotosCommandHandler(
            IMediator mediator, 
            ILogger<StoreProductPhotosCommandHandler> logger,
            IFileHandler fileWriter
        ) => (_mediator, _logger, _fileWriter) = (mediator, logger, fileWriter);
        
        public async Task<Unit> Handle(StoreProductPhotosCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving products from database");
            var products = await _mediator.Send(new GetAllProductsWithPhotosQuery(), cancellationToken);

            foreach (var photo in products.SelectMany(_ => _.Photos))
            {
                WriteFile(Path.Combine(request.TargetFolder, photo.ThumbnailPhotoFileName), photo.ThumbNailPhoto);
                WriteFile(Path.Combine(request.TargetFolder, photo.LargePhotoFileName), photo.LargePhoto);
            }

            return Unit.Value;
        }

        private void WriteFile(string fileName, byte[] photo)
        {
            if (!_fileWriter.FileExists(fileName))
            {
                _logger.LogInformation("Saving photo to {FileName}", fileName);

                try
                {
                    _fileWriter.WriteFile(fileName, photo);
                }
                catch (Exception ex)
                {
                    _logger.LogError("ERROR when trying to write {FileName}: {Message}",
                        fileName, ex.Message
                    );
                }
            }
            else
                _logger.LogWarning("{FileName} already exists", fileName);
        }
    }
}