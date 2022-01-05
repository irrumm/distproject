using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class GameAwardMapper: BaseMapper<DAL.App.DTO.GameAward, Domain.App.GameAward>, IBaseMapper<DAL.App.DTO.GameAward, Domain.App.GameAward>
    {
        public GameAwardMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}