using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Orders: DomainEntityId, IDomainAppUser<AppUser>
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(7);
        
        public ICollection<OrderLine>? OrderLines { get; set; }
        
        [ForeignKey("Address")]
        public Guid? AddressId { get; set; }
        public Address? Address { get; set; }
        
        [ForeignKey("PaymentMethod")]
        public Guid? PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }

        [ForeignKey("AppUser")]
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

    }
}