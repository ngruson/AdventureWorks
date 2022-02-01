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
        private readonly IMediator mediator;
        private readonly ILogger<StoreProductPhotosCommandHandler> logger;
        private readonly IFileWriter fileWriter;

        public StoreProductPhotosCommandHandler(
            IMediator mediator, 
            ILogger<StoreProductPhotosCommandHandler> logger,
            IFileWriter fileWriter
        ) => (this.mediator, this.logger, this.fileWriter) = (mediator, logger, fileWriter);
        
        public async Task<Unit> Handle(StoreProductPhotosCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving products from database");
            var products = await mediator.Send(new GetAllProductsWithPhotosQuery(), cancellationToken);

            foreach (var photo in products.SelectMany(_ => _.Photos))
            {
                string fileName = Path.Combine(request.TargetFolder, photo.ThumbnailPhotoFileName);

                WriteFile(Path.Combine(request.TargetFolder, photo.ThumbnailPhotoFileName), photo.ThumbNailPhoto);
                WriteFile(Path.Combine(request.TargetFolder, photo.LargePhotoFileName), photo.LargePhoto);
            }

            return Unit.Value;
        }

        private void WriteFile(string fileName, byte[] photo)
        {
            if (!fileWriter.FileExists(fileName))
            {
                logger.LogInformation("Saving photo to {FileName}", fileName);

                try
                {
                    fileWriter.WriteFile(fileName, photo);
                }
                catch (Exception ex)
                {
                    logger.LogError("ERROR when trying to write {FileName}: {Message}",
                        fileName, ex.Message
                    );
                }
            }
            else
                logger.LogWarning("{FileName} already exists", fileName);
        }
    }
}