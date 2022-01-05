using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IOrderLineRepository : IBaseRepository<DALAppDTO.OrderLine>, IOrderLineRepositoryCustom<DALAppDTO.OrderLine>
    {

    }
    
    public interface IOrderLineRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllByOrderApiAsync(Guid orderId, Guid userId, bool noTracking = true);

        Task<IEnumerable<TEntity>> GetAllApiAsync(bool noTracking = true);
        Task<TEntity?> FirstOrDefaultApiAsync(Guid id, bool noTracking = true);        
    }
}