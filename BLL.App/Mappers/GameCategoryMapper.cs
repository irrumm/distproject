using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class GameCategoryMapper: BaseMapper<BLL.App.DTO.GameCategory, DAL.App.DTO.GameCategory>, IBaseMapper<BLL.App.DTO.GameCategory, DAL.App.DTO.GameCategory>
    {
        public GameCategoryMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}