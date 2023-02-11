using MediatR;

namespace AW.Services.Product.Core.Handlers.StoreProductPhotos
{
    public class StoreProductPhotosCommand : IRequest
    {
        public StoreProductPhotosCommand(string targetFolder)
        {
            TargetFolder = targetFolder;
        }

        public string TargetFolder { get; private init; }
    }
}