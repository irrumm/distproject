using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGameInfoRepository : IBaseRepository<DALAppDTO.GameInfo>, IGameInfoRepositoryCustom<DALAppDTO.GameInfo>
    {

        
    }
    public interface IGameInfoRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllByTitleApiAsync(string title, bool noTracking = true);

        Task<IEnumerable<TEntity>> GetAllWithCountsAsync(bool noTracking = true);
        Task<TEntity?> FirstOrDefaultWithCountsAsync(Guid id, bool noTracking = true);
        
    }
}