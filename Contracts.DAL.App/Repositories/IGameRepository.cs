using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IGameRepository : IBaseRepository<DALAppDTO.Game>, IGameRepositoryCustom<DALAppDTO.Game>
    {

    }
    
    public interface IGameRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllByGameApiAsync(Guid gameId, bool noTracking = true);
        Task<IEnumerable<TEntity>> GetAllAvailableByGameApiAsync(Guid gameId, bool available = true, bool noTracking = true);
        
        Task<IEnumerable<TEntity>> GetAllApiAsync(bool noTracking = true);
        Task<TEntity?> FirstOrDefaultApiAsync(Guid id, bool noTracking = true);
        Task<TEntity?> GetAvailableGameApiAsync(Guid gameId, bool noTracking = true);
    }
}