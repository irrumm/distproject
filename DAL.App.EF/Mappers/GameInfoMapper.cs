using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class GameInfoMapper: BaseMapper<DAL.App.DTO.GameInfo, Domain.App.GameInfo>, IBaseMapper<DAL.App.DTO.GameInfo, Domain.App.GameInfo>
    {
        public GameInfoMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}