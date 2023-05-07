using System.Xml;
using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.ProductModel
{
    public class ProductModelViewModel : IMapFrom<Infrastructure.Api.Product.Handlers.GetProductModels.ProductModel>
    {
        public string? Name { get; set; }

        public string? CatalogDescription { get; set; }

        public XmlDocument? Instructions { get; set; }
        public List<ProductModelDescriptionViewModel>? Descriptions { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Infrastructure.Api.Product.Handlers.GetProductModels.ProductModel, ProductModelViewModel>()
                .ForMember(_ => _.Instructions, opt => opt.Ignore())
                .ForMember(_ => _.Descriptions, opt => opt.Ignore());
            profile.CreateMap<Infrastructure.Api.Product.Handlers.GetProductModel.ProductModel, ProductModelViewModel>()
                .ForMember(_ => _.Instructions, opt => opt.MapFrom(src => CreateXmlDocument(src.Instructions)));
        }

        private static XmlDocument? CreateXmlDocument(string? instructions)
        {
            if (!string.IsNullOrEmpty(instructions))
            {
                var doc = new XmlDocument();

                try
                {
                    doc.LoadXml(instructions);
                }
                catch
                {
                    return null;
                }
                
                return doc;
            }
            return null;
        }
    }
}
