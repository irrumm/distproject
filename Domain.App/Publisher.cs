﻿using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Publisher: DomainEntityId
    {
        [MaxLength(32)] public string Name { get; set; } = default!;
    }
}