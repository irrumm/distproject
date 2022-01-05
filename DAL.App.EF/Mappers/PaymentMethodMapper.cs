using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class PaymentMethodMapper: BaseMapper<DAL.App.DTO.PaymentMethod, Domain.App.PaymentMethod>, IBaseMapper<DAL.App.DTO.PaymentMethod, Domain.App.PaymentMethod>
    {
        public PaymentMethodMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}