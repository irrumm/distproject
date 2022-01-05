using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class OrderLineRepository : BaseRepository<DAL.App.DTO.OrderLine, Domain.App.OrderLine, AppDbContext>, IOrderLineRepository
    {
        public OrderLineRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new OrderLineMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DAL.App.DTO.OrderLine>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
                .Include(ol => ol.Game)
                .Include(ol => ol.Orders)
                .Select(x => Mapper.Map(x));

            var res = await resQuery.ToListAsync();

            return res!;
        }
        
        public override async Task<DAL.App.DTO.OrderLine?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(ol => ol.Game)
                .Include(ol => ol.Orders);
                
            var res = Mapper.Map(await query.FirstOrDefaultAsync(f => f.Id == id));

            return res;
        }

        public async Task<IEnumerable<DTO.OrderLine>> GetAllByOrderApiAsync(Guid orderId, Guid userId, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking).Where(ol => ol.OrderId.Equals(orderId));

            var resQuery = query.Select(orderline => new DAL.App.DTO.OrderLine()
            {
                Id = orderline.Id,
                GameId = orderline.Game!.Id,
                GameInfoTitle = orderline.Game!.GameInfo!.Title,
                OrderId = orderline.Orders!.Id,
                GameInfoId = orderline.Game!.GameInfo!.Id
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<IEnumerable<DTO.OrderLine>> GetAllApiAsync(bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(orderline => new DAL.App.DTO.OrderLine()
            {
                Id = orderline.Id,
                GameId = orderline.Game!.Id,
                GameInfoTitle = orderline.Game!.GameInfo!.Title,
                OrderId = orderline.Orders!.Id,
                GameInfoId = orderline.Game!.GameInfo!.Id
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<DTO.OrderLine?> FirstOrDefaultApiAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);
            
            var resQuery = query.Select(orderline => new DAL.App.DTO.OrderLine()
            {
                Id = orderline.Id,
                GameId = orderline.Game!.Id,
                GameInfoTitle = orderline.Game!.GameInfo!.Title,
                OrderId = orderline.Orders!.Id,
                GameInfoId = orderline.Game!.GameInfo!.Id
            });
            var res = await resQuery.FirstOrDefaultAsync(e => e!.Id.Equals(id));

            return res;
        }
    }
}