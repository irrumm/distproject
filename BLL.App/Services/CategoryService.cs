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
    public class CategoryService: BaseEntityService<IAppUnitOfWork, ICategoryRepository, BLLAppDTO.Category, DALAppDTO.Category>, ICategoryService
    {
        public CategoryService(IAppUnitOfWork serviceUow, ICategoryRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new CategoryMapper(mapper))
        {
        }
    }
}