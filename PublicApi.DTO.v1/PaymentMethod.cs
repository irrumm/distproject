using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class PaymentMethodAdd
    {
        [MaxLength(32)] public string Description { get; set; } = default!;
    }

    public class PaymentMethod
    {
        public Guid Id { get; set; }
        
        [MaxLength(32)] public string Description { get; set; } = default!;
    }
}