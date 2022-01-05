using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class AddressRepository : BaseRepository<DAL.App.DTO.Address, Domain.App.Address, AppDbContext>, IAddressRepository
    {
        public AddressRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new AddressMapper(mapper))
        {
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.Address>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
                .OrderBy(a => a.City)
                .ThenBy(a => a.Region)
                .ThenBy(a => a.MachineLocation)
                .Select(x => Mapper.Map(x));

            var res = await resQuery.ToListAsync();

            return res!;
        }
    }
}