using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class GameCategoryMapper: BaseMapper<PublicApi.DTO.v1.GameCategory, BLL.App.DTO.GameCategory>
    {
        public static BLL.App.DTO.GameCategory MapToBll(PublicApi.DTO.v1.GameCategoryAdd gameCategory)
        {
            return new BLL.App.DTO.GameCategory()
            {
                CategoryId = gameCategory.CategoryId,
                GameInfoId = gameCategory.GameInfoId
            };
        }

        public GameCategoryMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}