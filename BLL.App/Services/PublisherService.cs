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
    public class PublisherService: BaseEntityService<IAppUnitOfWork, IPublisherRepository, BLLAppDTO.Publisher, DALAppDTO.Publisher>, IPublisherService
    {
        public PublisherService(IAppUnitOfWork serviceUow, IPublisherRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new PublisherMapper(mapper))
        {
        }
    }
}