using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain.Base;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Orders: DomainEntityId, IDomainAppUser<AppUser>
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(7);
        
        public ICollection<OrderLine>? OrderLines { get; set; }
        
        public Guid AddressId { get; set; }
        public Address? Address { get; set; }
        
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public int OrderLinesCount { get; set; }
        public string PaymentMethodDescription { get; set; } = default!;
        
        public string AddressLocation { get; set; } = default!;
        public string AddressServiceProvider = default!;
        public string AppUserEmail { get; set; } = default!;
    }
}