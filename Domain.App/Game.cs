﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Game: DomainEntityId
    {
        public Guid? GameInfoId { get; set; }
        public GameInfo? GameInfo { get; set; }
        public bool Available { get; set; }

    }
}