using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class GameInfoFilters
    {
        public List<Guid> Categories { get; set; } = new();
        public List<Guid> Publishers { get; set; } = new();

        public List<Guid> Languages { get; set; } = new();

        public int MinCost { get; set; }

        public int MaxCost { get; set; }
    }
}