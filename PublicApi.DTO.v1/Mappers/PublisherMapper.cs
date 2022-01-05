using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class PublisherMapper: BaseMapper<PublicApi.DTO.v1.Publisher, BLL.App.DTO.Publisher>
    {
        public static BLL.App.DTO.Publisher MapToBll(PublicApi.DTO.v1.PublisherAdd publisher)
        {
            return new BLL.App.DTO.Publisher()
            {
                Name = publisher.Name
            };
        }

        public PublisherMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}