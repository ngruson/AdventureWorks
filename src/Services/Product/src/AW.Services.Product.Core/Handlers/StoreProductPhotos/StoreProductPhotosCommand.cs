using MediatR;

namespace AW.Services.Product.Core.Handlers.StoreProductPhotos
{
    public class StoreProductPhotosCommand : IRequest
    {
        public string TargetFolder { get; set; }
    }
}