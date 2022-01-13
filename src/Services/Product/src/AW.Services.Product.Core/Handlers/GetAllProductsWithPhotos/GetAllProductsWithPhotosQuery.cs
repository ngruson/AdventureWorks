using MediatR;
using System.Collections.Generic;

namespace AW.Services.Product.Core.Handlers.GetAllProductsWithPhotos
{
    public class GetAllProductsWithPhotosQuery : IRequest<List<ProductWithPhotoDto>>
    {
    }
}