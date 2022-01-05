using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class OrderLineMapper: BaseMapper<DAL.App.DTO.OrderLine, Domain.App.OrderLine>, IBaseMapper<DAL.App.DTO.OrderLine, Domain.App.OrderLine>
    {
        public OrderLineMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}