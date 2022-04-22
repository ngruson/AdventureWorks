using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class PersonNameViewModel : IMapFrom<NameFactory>
    {
        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Full name")]
        [Required]
        public string FullName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NameFactory, PersonNameViewModel>()
                .ReverseMap();
        }
    }
}