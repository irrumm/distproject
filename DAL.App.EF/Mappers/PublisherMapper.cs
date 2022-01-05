using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class PublisherMapper: BaseMapper<DAL.App.DTO.Publisher, Domain.App.Publisher>, IBaseMapper<DAL.App.DTO.Publisher, Domain.App.Publisher>
    {
        public PublisherMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}