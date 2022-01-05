using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class OrdersAdd
    {
        public Guid AddressId { get; set; }
        
        public Guid PaymentMethodId { get; set; }

        public List<Guid> GameIds { get; set; } = new();
    }

    public class Orders
    {
        public Guid Id { get; set; }
        
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(7);
        
        public int OrderLinesCount { get; set; }
        public string PaymentMethodDescription { get; set; } = default!;
        public string AddressLocation { get; set; } = default!;
        public string AddressServiceProvider { get; set; } = default!;
        public string AppUserEmail { get; set; } = default!;
        public Guid AddressId { get; set; }
        
        public Guid PaymentMethodId { get; set; }
        public Guid AppUserId { get; set; }
    }
}