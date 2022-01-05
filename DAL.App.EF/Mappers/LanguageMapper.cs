using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class LanguageMapper: BaseMapper<DAL.App.DTO.Language, Domain.App.Language>, IBaseMapper<DAL.App.DTO.Language, Domain.App.Language>
    {
        public LanguageMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}