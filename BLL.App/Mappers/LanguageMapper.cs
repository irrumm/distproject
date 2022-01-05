using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class LanguageMapper: BaseMapper<BLL.App.DTO.Language, DAL.App.DTO.Language>, IBaseMapper<BLL.App.DTO.Language, DAL.App.DTO.Language>
    {
        public LanguageMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}