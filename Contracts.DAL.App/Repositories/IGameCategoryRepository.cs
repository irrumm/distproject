using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGameCategoryRepository : IBaseRepository<DALAppDTO.GameCategory>, IGameCategoryRepositoryCustom<DALAppDTO.GameCategory>
    {

    }
    
    public interface IGameCategoryRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllByGameApiAsync(Guid gameId, bool noTracking = true);
        Task<IEnumerable<TEntity>> GetAllApiAsync(bool noTracking = true);
        Task<TEntity?> FirstOrDefaultApiAsync(Guid id, bool noTracking = true);        
    }
}