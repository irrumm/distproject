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
    public class OrdersService: BaseEntityService<IAppUnitOfWork, IOrdersRepository, BLLAppDTO.Orders, DALAppDTO.Orders>, IOrdersService
    {
        public OrdersService(IAppUnitOfWork serviceUow, IOrdersRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new OrdersMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.Orders>> GetAllByOrderTimeApiAsync(DateTime start, DateTime end, Guid? userId, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllByOrderTimeApiAsync(start, end, userId, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<IEnumerable<BLLAppDTO.Orders>> GetAllByReturnTimeApiAsync(DateTime start, DateTime end, Guid? userId, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllByReturnTimeApiAsync(start, end, userId, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<IEnumerable<BLLAppDTO.Orders>> GetAllByUserApiAsync(Guid id, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllByUserApiAsync(id, noTracking)).Select(x => Mapper.Map(x))!;
        }
        
        public async Task<IEnumerable<BLLAppDTO.Orders>> GetAllApiAsync(Guid userId, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllApiAsync(userId, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<BLLAppDTO.Orders?> FirstOrDefaultApiAsync(Guid id, Guid userId, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultApiAsync(id, userId, noTracking));
        }

        public async Task<BLLAppDTO.Orders> CreateOrder(Guid orderId, List<Guid> gameIds)
        {
            foreach (var gameInfoId in gameIds)
            {
                var game = await ServiceUow.Games.GetAvailableGameApiAsync(gameInfoId);
                if (game != null)
                {
                    var bllGame = new DAL.App.DTO.Game()
                    {
                        Id = game!.Id,
                        Available = false,
                        GameInfoId = game!.GameInfoId
                    };
                    ServiceUow.Games.Update(bllGame);
                   
                    var bllOrderLine = new DAL.App.DTO.OrderLine()
                    {
                        GameId = game.Id,
                        OrderId = orderId
                    };
                    ServiceUow.OrderLines.Add(bllOrderLine);
                    await ServiceUow.SaveChangesAsync(); 
                }
            }
            return (await FirstOrDefaultApiAsync(orderId, default))!;
        }
    }
}