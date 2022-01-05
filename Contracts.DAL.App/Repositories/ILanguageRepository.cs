using System;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ILanguageRepository : IBaseRepository<DALAppDTO.Language>, ILanguageRepositoryCustom<DALAppDTO.Language>
    {
        
    }
    
    public interface ILanguageRepositoryCustom<TEntity>
    {
        
    }
}