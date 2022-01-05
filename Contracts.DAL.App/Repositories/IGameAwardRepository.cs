using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGameAwardRepository : IBaseRepository<DALAppDTO.GameAward>, IGameAwardRepositoryCustom<DALAppDTO.GameAward>
    {

    }
    
    public interface IGameAwardRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllByGameApiAsync(Guid gameId, bool noTracking = true);
        Task<IEnumerable<TEntity>> GetAllByAwardApiAsync(Guid awardId, bool noTracking = true);
        
        Task<IEnumerable<TEntity>> GetAllApiAsync(bool noTracking = true);
        Task<TEntity?> FirstOrDefaultApiAsync(Guid id, bool noTracking = true);        
    }
}