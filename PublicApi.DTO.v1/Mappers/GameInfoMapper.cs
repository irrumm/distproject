using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class GameInfoMapper: BaseMapper<PublicApi.DTO.v1.GameInfo, BLL.App.DTO.GameInfo>
    {
        public static BLL.App.DTO.GameInfo MapToBll(PublicApi.DTO.v1.GameInfoAdd gameInfo)
        {
            return new BLL.App.DTO.GameInfo()
            {
                Title = gameInfo.Title,
                Description = gameInfo.Description,
                RentalCost = gameInfo.RentalCost,
                ReplacementCost = gameInfo.ReplacementCost,
                ProductCode = gameInfo.ProductCode,
                MainPictureUrl = gameInfo.MainPictureUrl,
                LanguageId = gameInfo.LanguageId,
                PublisherId = gameInfo.PublisherId 
            };
        }

        public GameInfoMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}