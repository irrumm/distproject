using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class GameAwardMapper: BaseMapper<PublicApi.DTO.v1.GameAward, BLL.App.DTO.GameAward>
    {
        public static BLL.App.DTO.GameAward MapToBll(PublicApi.DTO.v1.GameAwardAdd gameAward)
        {
            return new BLL.App.DTO.GameAward()
            {
                YearWon = gameAward.YearWon,
                AwardId = gameAward.AwardId,
                GameInfoId = gameAward.GameInfoId
            };
        }

        public GameAwardMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}