using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class PaymentMethodMapper: BaseMapper<BLL.App.DTO.PaymentMethod, DAL.App.DTO.PaymentMethod>, IBaseMapper<BLL.App.DTO.PaymentMethod, DAL.App.DTO.PaymentMethod>
    {
        public PaymentMethodMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}