using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class GameInfoMapper: BaseMapper<BLL.App.DTO.GameInfo, DAL.App.DTO.GameInfo>, IBaseMapper<BLL.App.DTO.GameInfo, DAL.App.DTO.GameInfo>
    {
        public GameInfoMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}