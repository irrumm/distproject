using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class AwardRepository : BaseRepository<DAL.App.DTO.Award, Domain.App.Award, AppDbContext>, IAwardRepository
    {
        public AwardRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new AwardMapper(mapper))
        {
        }
    }
}