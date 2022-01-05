using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class OrdersMapper: BaseMapper<BLL.App.DTO.Orders, DAL.App.DTO.Orders>, IBaseMapper<BLL.App.DTO.Orders, DAL.App.DTO.Orders>
    {
        public OrdersMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}