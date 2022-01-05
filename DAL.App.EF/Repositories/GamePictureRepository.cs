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
    public class GamePictureRepository : BaseRepository<DAL.App.DTO.GamePicture, Domain.App.GamePicture, AppDbContext>, IGamePictureRepository
    {
        public GamePictureRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GamePictureMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DAL.App.DTO.GamePicture>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
                .Include(gc => gc.GameInfo)
                .Select(x => Mapper.Map(x));

            var res = await resQuery.ToListAsync();

            return res!;
        }
        
        public override async Task<DAL.App.DTO.GamePicture?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(gc => gc.GameInfo);
                
            var res = Mapper.Map(await query.FirstOrDefaultAsync(f => f.Id == id));

            return res;
        }

        public async Task<IEnumerable<DTO.GamePicture>> GetAllByGameApiAsync(Guid gameId, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking).Where(gp => gp.GameInfoId.Equals(gameId));

            var resQuery = query.Select(gamepicture => new DAL.App.DTO.GamePicture()
            {
                Id = gamepicture.Id,
                DateAdded = gamepicture.DateAdded,
                GameInfoId = gamepicture.GameInfo!.Id,
                GameInfoTitle = gamepicture.GameInfo!.Title,
                PictureUrl = gamepicture.PictureUrl
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<IEnumerable<DTO.GamePicture>> GetAllApiAsync(bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(gamepicture => new DAL.App.DTO.GamePicture()
            {
                Id = gamepicture.Id,
                DateAdded = gamepicture.DateAdded,
                GameInfoId = gamepicture.GameInfo!.Id,
                GameInfoTitle = gamepicture.GameInfo!.Title,
                PictureUrl = gamepicture.PictureUrl
            });
            var res = await resQuery.ToListAsync();

            return res;
        }

        public async Task<DTO.GamePicture?> FirstOrDefaultApiAsync(Guid id, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);
            
            var resQuery = query.Select(gamepicture => new DAL.App.DTO.GamePicture()
            {
                Id = gamepicture.Id,
                DateAdded = gamepicture.DateAdded,
                GameInfoId = gamepicture.GameInfo!.Id,
                GameInfoTitle = gamepicture.GameInfo!.Title,
                PictureUrl = gamepicture.PictureUrl
            });
            var res = await resQuery.FirstOrDefaultAsync(e => e!.Id.Equals(id));

            return res;
        }
    }
}