using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class CategoryAdd
    {
        [MaxLength(32)] public string Name { get; set; } = default!;
    }

    public class Category
    {
        public Guid Id { get; set; }
        
        [MaxLength(32)] public string Name { get; set; } = default!;
    }
}