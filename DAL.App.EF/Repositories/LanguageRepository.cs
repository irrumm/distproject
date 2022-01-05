using System.Linq;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class LanguageRepository : BaseRepository<DAL.App.DTO.Language, Domain.App.Language, AppDbContext>, ILanguageRepository
    {
        public LanguageRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new LanguageMapper(mapper))
        {
        }
    }
}