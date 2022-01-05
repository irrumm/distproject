using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class LanguageMapper: BaseMapper<PublicApi.DTO.v1.Language, BLL.App.DTO.Language>
    {
        public static BLL.App.DTO.Language MapToBll(PublicApi.DTO.v1.LanguageAdd language)
        {
            return new BLL.App.DTO.Language()
            {
                Name = language.Name
            };
        }

        public LanguageMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}