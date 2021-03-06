using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Award: DomainEntityId
    {
        [MaxLength(128)] public string Host { get; set; } = default!;
        
        [MaxLength(128)] public string Name { get; set; } = default!;

    }
}