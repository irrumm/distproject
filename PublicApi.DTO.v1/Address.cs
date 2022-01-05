using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class AddressAdd
    {
        [MaxLength(64)] public string City { get; set; } = default!;

        [MaxLength(128)] public string Region { get; set; } = default!;

        [MaxLength(32)] public string ServiceProvider { get; set; } = default!;

        [MaxLength(255)] public string MachineLocation { get; set; } = default!;
    }

    public class Address
    {
        public Guid Id { get; set; }
        
        [MaxLength(64)] public string City { get; set; } = default!;

        [MaxLength(128)] public string Region { get; set; } = default!;

        [MaxLength(32)] public string ServiceProvider { get; set; } = default!;

        [MaxLength(255)] public string MachineLocation { get; set; } = default!;
    }
}