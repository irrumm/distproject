using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class AwardMapper: BaseMapper<BLL.App.DTO.Award, DAL.App.DTO.Award>, IBaseMapper<BLL.App.DTO.Award, DAL.App.DTO.Award>
    {
        public AwardMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}