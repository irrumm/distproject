
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
    public class AddressService: BaseEntityService<IAppUnitOfWork, IAddressRepository, BLLAppDTO.Address, DALAppDTO.Address>, IAddressService
    {
        public AddressService(IAppUnitOfWork serviceUow, IAddressRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new AddressMapper(mapper))
        {
        }
    }
}