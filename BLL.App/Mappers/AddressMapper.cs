using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class AddressMapper: BaseMapper<BLL.App.DTO.Address, DAL.App.DTO.Address>, IBaseMapper<BLL.App.DTO.Address, DAL.App.DTO.Address>

    {
        public AddressMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}