using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class AwardMapper: BaseMapper<PublicApi.DTO.v1.Award, BLL.App.DTO.Award>
    {
        public static BLL.App.DTO.Award MapToBll(PublicApi.DTO.v1.AwardAdd award)
        {
            return new BLL.App.DTO.Award()
            {
                Host = award.Host,
                Name = award.Name
            };
        }

        public AwardMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}