using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class GameCategoryMapper: BaseMapper<DAL.App.DTO.GameCategory, Domain.App.GameCategory>, IBaseMapper<DAL.App.DTO.GameCategory, Domain.App.GameCategory>
    {
        public GameCategoryMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}