using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class LanguageAdd
    {
        [MaxLength(3)] public string Name { get; set; } = default!;
    }

    public class Language
    {
        public Guid Id { get; set; }
        
        [MaxLength(3)] public string Name { get; set; } = default!;
    }
}