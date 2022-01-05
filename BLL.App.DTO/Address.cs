using System;
using System.ComponentModel.DataAnnotations;
using DAL.App.DTO;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Address: DomainEntityId
    {
        [MaxLength(64)] public string City { get; set; } = default!;

        [MaxLength(128)] public string Region { get; set; } = default!;

        [MaxLength(32)] public string ServiceProvider { get; set; } = default!;

        [MaxLength(255)] public string MachineLocation { get; set; } = default!;

    }
}