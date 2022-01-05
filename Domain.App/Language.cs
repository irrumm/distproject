using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App
{
    public class Language: DomainEntityId
    {

        [MaxLength(3)] 
        public string Name { get; set; } = default!;

    }
}