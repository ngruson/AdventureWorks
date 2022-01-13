using AW.Services.Product.Core.Handlers.GetAllProductsWithPhotos;
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

        public StoreProductPhotosCommandHandler(IMediator mediator, ILogger<StoreProductPhotosCommandHandler> logger) =>
            (this.mediator, this.logger) = (mediator, logger);
        
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

        private static void WriteFile(string fileName, byte[] photo)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"Saving photo to {fileName}");

                try
                {
                    File.WriteAllBytes(fileName, photo);
                }
                catch (Exception ex)
                {
                    var foregroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERROR when trying to write {fileName}: {ex.Message}");
                    Console.ForegroundColor = foregroundColor;
                }
            }
        }
    }
}