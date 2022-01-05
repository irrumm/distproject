using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class AddressMapper: BaseMapper<PublicApi.DTO.v1.Address, BLL.App.DTO.Address>
    {
        public static BLL.App.DTO.Address MapToBll(PublicApi.DTO.v1.AddressAdd address)
        {
            return new BLL.App.DTO.Address()
            {
                City = address.City,
                MachineLocation = address.MachineLocation,
                ServiceProvider = address.ServiceProvider,
                Region = address.Region
            };
        }

        public AddressMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}