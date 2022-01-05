using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class GamePictureMapper: BaseMapper<PublicApi.DTO.v1.GamePicture, BLL.App.DTO.GamePicture>
    {
        public static BLL.App.DTO.GamePicture MapToBll(PublicApi.DTO.v1.GamePictureAdd gamePicture)
        {
            return new BLL.App.DTO.GamePicture()
            {
                GameInfoId = gamePicture.GameInfoId,
                PictureUrl = gamePicture.PictureUrl  
            };
        }

        public GamePictureMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}