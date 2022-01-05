using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class AwardMapper: BaseMapper<DAL.App.DTO.Award, Domain.App.Award>, IBaseMapper<DAL.App.DTO.Award, Domain.App.Award>
    {
        public AwardMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}