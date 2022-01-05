using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class OrdersMapper: BaseMapper<DAL.App.DTO.Orders, Domain.App.Orders>, IBaseMapper<DAL.App.DTO.Orders, Domain.App.Orders>
    {
        public OrdersMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}