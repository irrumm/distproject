using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class PaymentMethod: DomainEntityId
    {
        [MaxLength(32)] public string Description { get; set; } = default!;

    }
}