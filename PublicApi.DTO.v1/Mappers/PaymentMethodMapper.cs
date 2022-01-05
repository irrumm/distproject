using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class PaymentMethodMapper: BaseMapper<PublicApi.DTO.v1.PaymentMethod, BLL.App.DTO.PaymentMethod>
    {
        public static BLL.App.DTO.PaymentMethod MapToBll(PublicApi.DTO.v1.PaymentMethodAdd paymentMethod)
        {
            return new BLL.App.DTO.PaymentMethod()
            {
                Description = paymentMethod.Description
            };
        }

        public PaymentMethodMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}