using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Category: DomainEntityId
    {
        [MaxLength(32)] public string Name { get; set; } = default!;

    }
}