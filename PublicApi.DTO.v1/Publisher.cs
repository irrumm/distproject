using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class PublisherAdd
    {
        [MaxLength(32)] public string Name { get; set; } = default!;
    }

    public class Publisher
    {
        public Guid Id { get; set; }
        
        [MaxLength(32)] public string Name { get; set; } = default!;
    }
}