using AutoMapper;
using BLL.App.DTO.Identity;
using DAL.App.DTO;

namespace BLL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Address, DAL.App.DTO.Address>().ReverseMap();
            CreateMap<Award, DAL.App.DTO.Award>().ReverseMap();
            CreateMap<Category, DAL.App.DTO.Category>().ReverseMap();
            CreateMap<Feedback, DAL.App.DTO.Feedback>().ReverseMap();
            CreateMap<Game, DAL.App.DTO.Game>().ReverseMap();
            CreateMap<GameAward, DAL.App.DTO.GameAward>().ReverseMap();
            CreateMap<GameCategory, DAL.App.DTO.GameCategory>().ReverseMap();
            CreateMap<GameInfo, DAL.App.DTO.GameInfo>().ReverseMap();
            CreateMap<GamePicture, DAL.App.DTO.GamePicture>().ReverseMap();
            CreateMap<Language, DAL.App.DTO.Language>().ReverseMap();
            CreateMap<OrderLine, DAL.App.DTO.OrderLine>().ReverseMap();
            CreateMap<Orders, DAL.App.DTO.Orders>().ReverseMap();
            CreateMap<PaymentMethod, DAL.App.DTO.PaymentMethod>().ReverseMap();
            CreateMap<Publisher, DAL.App.DTO.Publisher>().ReverseMap();
            CreateMap<AppUser, DAL.App.DTO.Identity.AppUser>().ReverseMap();
            CreateMap<AppRole, DAL.App.DTO.Identity.AppRole>().ReverseMap();
        }
    }
}