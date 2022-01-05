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
    public class AwardService: BaseEntityService<IAppUnitOfWork, IAwardRepository, BLLAppDTO.Award, DALAppDTO.Award>, IAwardService
    {
        public AwardService(IAppUnitOfWork serviceUow, IAwardRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new AwardMapper(mapper))
        {
        }
    }
}