using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IFeedbackRepository : IBaseRepository<DALAppDTO.Feedback>, IFeedbackRepositoryCustom<DALAppDTO.Feedback>
    {

    }
    
    public interface IFeedbackRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllByGameApiAsync(Guid gameId, bool noTracking = true);
                
        Task<IEnumerable<TEntity>> GetAllByUserApiAsync(Guid user, bool noTracking = true);
        
        Task<IEnumerable<TEntity>> GetAllApiAsync(Guid userId, bool noTracking = true);
        Task<TEntity?> FirstOrDefaultApiAsync(Guid id, Guid userId, bool noTracking = true);
    }
}