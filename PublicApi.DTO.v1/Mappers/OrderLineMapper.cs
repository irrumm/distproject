using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class OrderLineMapper: BaseMapper<PublicApi.DTO.v1.OrderLine, BLL.App.DTO.OrderLine>
    {
        public static BLL.App.DTO.OrderLine MapToBll(PublicApi.DTO.v1.OrderLineAdd orderLine)
        {
            return new BLL.App.DTO.OrderLine()
            {
                GameId = orderLine.GameId,
                OrderId = orderLine.OrderId
            };
        }

        public OrderLineMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}