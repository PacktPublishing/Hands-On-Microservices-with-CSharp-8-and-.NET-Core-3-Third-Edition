using AutoMapper;
using FlixOne.BookStore.UserService.Models.Identity;

namespace FlixOne.BookStore.UserService.Models.Mappings
{
    public class MapViewModelToAppUserProfile : Profile
    {
        public MapViewModelToAppUserProfile() => CreateMap<RegisterUserViewModel, AppUser>()
            .ForMember(user => user.UserName, map => map.MapFrom(vm => vm.EmailId))
            .ForMember(user => user.PhoneNumber, map => map.MapFrom(vm => vm.Mobile));
    }
}
