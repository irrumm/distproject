using System;
using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class OrdersMapper: BaseMapper<PublicApi.DTO.v1.Orders, BLL.App.DTO.Orders>
    {
        public static BLL.App.DTO.Orders MapToBll(PublicApi.DTO.v1.OrdersAdd orders)
        {
            return new BLL.App.DTO.Orders()
            {
                AddressId = orders.AddressId,
                OrderDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays(7),
                PaymentMethodId = orders.PaymentMethodId
            };
        }

        public OrdersMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}