using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class GameCategoryRepository : BaseRepository<DAL.App.DTO.GameCategory, Domain.App.GameCategory, AppDbContext>, IGameCategoryRepository
    {
        public GameCategoryRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GameCategoryMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DAL.App.DTO.GameCategory>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
                .Include(gc => gc.Category)
                .Include(gc => gc.GameInfo)
                .Select(x => Mapper.Map(x));

            var res = await resQuery.ToListAsync();

            return res!;
        }
        
        public override async Task<DAL.App.DTO.GameCategory?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(gc => gc.Category)
                .Include(gc => gc.GameInfo);
                
            var res = Mapper.Map(await query.FirstOrDefaultAsync(f => f.Id == id));

            return res;
        }


        public async Task<IEnumerable<GameCategory>> GetAllByGameApiAsync(Guid gameId, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking).Where(gc => gc.GameInfoId.Equals(gameId));

            var resQuery = query.Select(gamecategory => new DAL.App.DTO.GameCategory()
            {
                Id = gamecategory.Id,
                CategoryId = gamecategory.Category!.Id,
                CategoryName = gamecategory.Category!.Name,
                GameInfoId = gamecategory.GameInfo!.Id,
                GameInfoTitle = gamecategory.GameInfo!.Title
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<IEnumerable<DTO.GameCategory>> GetAllApiAsync(bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(gamecategory => new DAL.App.DTO.GameCategory()
            {
                Id = gamecategory.Id,
                CategoryId = gamecategory.Category!.Id,
                CategoryName = gamecategory.Category!.Name,
                GameInfoId = gamecategory.GameInfo!.Id,
                GameInfoTitle = gamecategory.GameInfo!.Title
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<DTO.GameCategory?> FirstOrDefaultApiAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);
            
            var resQuery = query.Select(gc => new DAL.App.DTO.GameCategory()
            {
                Id = gc.Id,
                CategoryId = gc.Category!.Id,
                CategoryName = gc.Category!.Name,
                GameInfoId = gc.GameInfo!.Id,
                GameInfoTitle = gc.GameInfo!.Title
                
            });
            var res = await resQuery.FirstOrDefaultAsync(e => e!.Id.Equals(id));

            return res;
        }
    }
}