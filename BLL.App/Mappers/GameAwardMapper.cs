using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class GameAwardMapper: BaseMapper<BLL.App.DTO.GameAward, DAL.App.DTO.GameAward>, IBaseMapper<BLL.App.DTO.GameAward, DAL.App.DTO.GameAward>
    {
        public GameAwardMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}