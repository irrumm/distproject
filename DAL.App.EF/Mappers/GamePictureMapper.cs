using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class GamePictureMapper: BaseMapper<DAL.App.DTO.GamePicture, Domain.App.GamePicture>, IBaseMapper<DAL.App.DTO.GamePicture, Domain.App.GamePicture>
    {
        public GamePictureMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}