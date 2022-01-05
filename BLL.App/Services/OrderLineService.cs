using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class OrderLineService: BaseEntityService<IAppUnitOfWork, IOrderLineRepository, BLLAppDTO.OrderLine, DALAppDTO.OrderLine>, IOrderLineService
    {
        public OrderLineService(IAppUnitOfWork serviceUow, IOrderLineRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new OrderLineMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.OrderLine>> GetAllByOrderApiAsync(Guid orderId, Guid userId, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllByOrderApiAsync(orderId, userId, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<IEnumerable<BLLAppDTO.OrderLine>> GetAllApiAsync(bool noTracking = true)
        {
            return (await ServiceRepository.GetAllApiAsync(noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<BLLAppDTO.OrderLine?> FirstOrDefaultApiAsync(Guid id, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultApiAsync(id, noTracking));
        }
    }
}