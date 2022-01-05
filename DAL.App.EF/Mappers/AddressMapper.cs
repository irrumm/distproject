using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class AddressMapper: BaseMapper<DAL.App.DTO.Address, Domain.App.Address>, IBaseMapper<DAL.App.DTO.Address, Domain.App.Address>

    {
        public AddressMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}