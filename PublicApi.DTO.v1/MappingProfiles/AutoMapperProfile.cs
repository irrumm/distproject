
using AutoMapper;

namespace PublicApi.DTO.v1.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BLL.App.DTO.Address, PublicApi.DTO.v1.Address>().ReverseMap();
            CreateMap<BLL.App.DTO.Award, PublicApi.DTO.v1.Award>().ReverseMap();
            CreateMap<BLL.App.DTO.Category, PublicApi.DTO.v1.Category>().ReverseMap();
            CreateMap<BLL.App.DTO.Feedback, PublicApi.DTO.v1.Feedback>().ReverseMap();
            CreateMap<BLL.App.DTO.Game, PublicApi.DTO.v1.Game>().ReverseMap();
            CreateMap<BLL.App.DTO.GameAward, PublicApi.DTO.v1.GameAward>().ReverseMap();
            CreateMap<BLL.App.DTO.GameCategory, PublicApi.DTO.v1.GameCategory>().ReverseMap();
            CreateMap<BLL.App.DTO.GameInfo, PublicApi.DTO.v1.GameInfo>().ReverseMap();
            CreateMap<BLL.App.DTO.GamePicture, PublicApi.DTO.v1.GamePicture>().ReverseMap();
            CreateMap<BLL.App.DTO.Language, PublicApi.DTO.v1.Language>().ReverseMap();
            CreateMap<BLL.App.DTO.OrderLine, PublicApi.DTO.v1.OrderLine>().ReverseMap();
            CreateMap<BLL.App.DTO.Orders, PublicApi.DTO.v1.Orders>().ReverseMap();
            CreateMap<BLL.App.DTO.PaymentMethod, PublicApi.DTO.v1.PaymentMethod>().ReverseMap();
            CreateMap<BLL.App.DTO.Publisher, PublicApi.DTO.v1.Publisher>().ReverseMap();
            CreateMap<BLL.App.DTO.Identity.AppUser, PublicApi.DTO.v1.Identity.AppUser>().ReverseMap();
            CreateMap<BLL.App.DTO.Identity.AppRole, PublicApi.DTO.v1.Identity.AppRole>().ReverseMap();
        }
    }
}