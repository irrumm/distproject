using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class OrderLineMapper: BaseMapper<BLL.App.DTO.OrderLine, DAL.App.DTO.OrderLine>, IBaseMapper<BLL.App.DTO.OrderLine, DAL.App.DTO.OrderLine>
    {
        public OrderLineMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}