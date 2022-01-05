using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IOrdersRepository : IBaseRepository<DALAppDTO.Orders>, IOrdersRepositoryCustom<DALAppDTO.Orders>
    {

    }
    
    public interface IOrdersRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllByOrderTimeApiAsync(DateTime start, DateTime end, Guid? userId, bool noTracking = true);
        Task<IEnumerable<TEntity>> GetAllByReturnTimeApiAsync(DateTime start, DateTime end, Guid? userId, bool noTracking = true);

        Task<IEnumerable<TEntity>> GetAllByUserApiAsync(Guid id, bool noTracking = true);

        Task<IEnumerable<TEntity>> GetAllApiAsync(Guid userId, bool noTracking = true);
        Task<TEntity?> FirstOrDefaultApiAsync(Guid id, Guid userId, bool noTracking = true);        
    }
}