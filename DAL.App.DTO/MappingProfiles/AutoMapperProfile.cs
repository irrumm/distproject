using AutoMapper;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Address, Domain.App.Address>().ReverseMap();
            CreateMap<Award, Domain.App.Award>().ReverseMap();
            CreateMap<Category, Domain.App.Category>().ReverseMap();
            CreateMap<Feedback, Domain.App.Feedback>().ReverseMap();
            CreateMap<Game, Domain.App.Game>().ReverseMap();
            CreateMap<GameAward, Domain.App.GameAward>().ReverseMap();
            CreateMap<GameCategory, Domain.App.GameCategory>().ReverseMap();
            CreateMap<GameInfo, Domain.App.GameInfo>().ReverseMap();
            CreateMap<GamePicture, Domain.App.GamePicture>().ReverseMap();
            CreateMap<Language, Domain.App.Language>().ReverseMap();
            CreateMap<OrderLine, Domain.App.OrderLine>().ReverseMap();
            CreateMap<Orders, Domain.App.Orders>().ReverseMap();
            CreateMap<PaymentMethod, Domain.App.PaymentMethod>().ReverseMap();
            CreateMap<Publisher, Domain.App.Publisher>().ReverseMap();
            CreateMap<AppUser, Domain.App.Identity.AppUser>().ReverseMap();
            CreateMap<AppRole, Domain.App.Identity.AppRole>().ReverseMap();
        }
    }
}