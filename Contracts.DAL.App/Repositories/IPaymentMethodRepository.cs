using System;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentMethodRepository : IBaseRepository<DALAppDTO.PaymentMethod>, IPaymentMethodRepositoryCustom<DALAppDTO.PaymentMethod>
    {
        
    }
    
    public interface IPaymentMethodRepositoryCustom<TEntity>
    {
        
    }
}