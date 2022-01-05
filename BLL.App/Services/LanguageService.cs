using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class LanguageService: BaseEntityService<IAppUnitOfWork, ILanguageRepository, BLLAppDTO.Language, DALAppDTO.Language>, ILanguageService
    {
        public LanguageService(IAppUnitOfWork serviceUow, ILanguageRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new LanguageMapper(mapper))
        {
        }
    }
}