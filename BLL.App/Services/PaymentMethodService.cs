using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class PaymentMethodService: BaseEntityService<IAppUnitOfWork, IPaymentMethodRepository, BLLAppDTO.PaymentMethod, DALAppDTO.PaymentMethod>, IPaymentMethodService
    {
        public PaymentMethodService(IAppUnitOfWork serviceUow, IPaymentMethodRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new PaymentMethodMapper(mapper))
        {
        }
    }
}