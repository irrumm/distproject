using System;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICategoryRepository : IBaseRepository<DALAppDTO.Category>, ICategoryRepositoryCustom<DALAppDTO.Category>
    {
        
    }
    
    public interface ICategoryRepositoryCustom<TEntity>
    {
        
    }
}