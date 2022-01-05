using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class GameMapper: BaseMapper<PublicApi.DTO.v1.Game, BLL.App.DTO.Game>
    {
        public static BLL.App.DTO.Game MapToBll(PublicApi.DTO.v1.GameAdd game)
        {
            return new BLL.App.DTO.Game()
            {
                Available = game.Available,
                GameInfoId = game.GameInfoId
            };
        }

        public GameMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}