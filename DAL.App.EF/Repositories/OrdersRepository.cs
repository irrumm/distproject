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
    public class OrdersRepository : BaseRepository<DTO.Orders, Domain.App.Orders, AppDbContext>, IOrdersRepository
    {
        public OrdersRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new OrdersMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DTO.Orders>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            
            if (!userId.Equals(default))
            {
                query = query.Where(f => f.AppUserId == userId);
            }

            var resQuery = query
                .Include(g => g.Address)
                .Include(g => g.AppUser)
                .Include(g => g.PaymentMethod)
                .Include(g => g.OrderLines)
                .ThenInclude(ol => ol.Game)
                .ThenInclude(ga => ga!.GameInfo)
                .Select(x => Mapper.Map(x));

            var res = await resQuery.ToListAsync();

            return res!;
        }
        
        public override async Task<DTO.Orders?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            
            if (!userId.Equals(default))
            {
                query = query.Where(f => f.AppUserId == userId);
            }

            query = query
                .Include(g => g.Address)
                .Include(g => g.AppUser)
                .Include(g => g.PaymentMethod)
                .Include(g => g.OrderLines)
                .ThenInclude(ol => ol.Game)
                .ThenInclude(ga => ga!.GameInfo);
                
            var res = Mapper.Map(await query.FirstOrDefaultAsync(f => f.Id == id));

            return res;
        }

        public Task<IEnumerable<DTO.Orders>> GetAllByOrderTimeApiAsync(DateTime start, DateTime end, Guid? userId, bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DTO.Orders>> GetAllByReturnTimeApiAsync(DateTime start, DateTime end, Guid? userId, bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DTO.Orders>> GetAllByUserApiAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking).Where(o => o.AppUserId.Equals(id));

            var resQuery = query.Select(order => new DTO.Orders()
            {
                Id = order.Id,
                AddressId = order.Address!.Id,
                AppUserId = order.AppUserId,
                OrderDate = order.OrderDate,
                ReturnDate = order.ReturnDate,
                PaymentMethodId = order.PaymentMethod!.Id,
                PaymentMethodDescription = order.PaymentMethod!.Description,
                OrderLinesCount = order.OrderLines!.Count,
                AddressLocation = order.Address!.City + ", " + order.Address!.Region + ", " + order.Address!.MachineLocation,
                AddressServiceProvider = order.Address!.ServiceProvider,
                AppUserEmail = order.AppUser!.Email
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<IEnumerable<DTO.Orders>> GetAllApiAsync(Guid userId, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            
            if (!userId.Equals(default))
            {
                query = query.Where(f => f.AppUserId == userId);
            }

            var resQuery = query.Select(order => new DTO.Orders()
            {
                Id = order.Id,
                AddressId = order.Address!.Id,
                AppUserId = order.AppUserId,
                OrderDate = order.OrderDate,
                ReturnDate = order.ReturnDate,
                PaymentMethodId = order.PaymentMethod!.Id,
                PaymentMethodDescription = order.PaymentMethod!.Description,
                OrderLinesCount = order.OrderLines!.Count,
                AddressLocation = order.Address!.City + ", " + order.Address!.Region + ", " + order.Address!.MachineLocation,
                AddressServiceProvider = order.Address!.ServiceProvider,
                AppUserEmail = order.AppUser!.Email
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<DTO.Orders?> FirstOrDefaultApiAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);
            
            var resQuery = query.Select(order => new DAL.App.DTO.Orders()
            {
                Id = order.Id,
                AddressId = order.Address!.Id,
                AppUserId = order.AppUserId,
                OrderDate = order.OrderDate,
                ReturnDate = order.ReturnDate,
                PaymentMethodId = order.PaymentMethod!.Id,
                PaymentMethodDescription = order.PaymentMethod!.Description,
                OrderLinesCount = order.OrderLines!.Count,
                AddressLocation = order.Address!.City + ", " + order.Address!.Region + ", " + order.Address!.MachineLocation,
                AddressServiceProvider = order.Address!.ServiceProvider,
                AppUserEmail = order.AppUser!.Email
            });
            var res = await resQuery.FirstOrDefaultAsync(e => e!.Id.Equals(id));

            return res;
        }
    }
}