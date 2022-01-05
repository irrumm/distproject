using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class GamePictureMapper: BaseMapper<BLL.App.DTO.GamePicture, DAL.App.DTO.GamePicture>, IBaseMapper<BLL.App.DTO.GamePicture, DAL.App.DTO.GamePicture>
    {
        public GamePictureMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}