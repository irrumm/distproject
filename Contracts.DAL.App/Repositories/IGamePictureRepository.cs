using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGamePictureRepository : IBaseRepository<DALAppDTO.GamePicture>, IGamePictureRepositoryCustom<DALAppDTO.GamePicture>
    {

    }
    
    public interface IGamePictureRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllByGameApiAsync(Guid gameId, bool noTracking = true);
        
        Task<IEnumerable<TEntity>> GetAllApiAsync(bool noTracking = true);
        Task<TEntity?> FirstOrDefaultApiAsync(Guid id, bool noTracking = true);        
    }
}