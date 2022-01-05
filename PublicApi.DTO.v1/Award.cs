using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class AwardAdd
    {
        [MaxLength(128)] public string Host { get; set; } = default!;
        
        [MaxLength(128)] public string Name { get; set; } = default!;
    }

    public class Award
    {
        public Guid Id { get; set; }
        
        [MaxLength(128)] public string Host { get; set; } = default!;
        
        [MaxLength(128)] public string Name { get; set; } = default!;
    }
}