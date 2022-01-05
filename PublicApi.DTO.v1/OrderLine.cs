using System;

namespace PublicApi.DTO.v1
{
    public class OrderLineAdd
    {
        public Guid OrderId { get; set; }
        
        public Guid GameId { get; set; }
    }

    public class OrderLine
    {
        public Guid Id { get; set; }
        
        public string GameInfoTitle { get; set; } = default!;

        public Guid GameInfoId { get; set; } = default!;
        
        public Guid OrderId { get; set; }
        
        public Guid GameId { get; set; }
    }
}