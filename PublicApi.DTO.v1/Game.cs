using System;

namespace PublicApi.DTO.v1
{
    public class GameAdd
    {
        public Guid GameInfoId { get; set; }
        
        public bool Available { get; set; }
    }

    public class Game
    {
        public Guid Id { get; set; }
        
        public string GameInfoTitle { get; set; } = default!;
        
        public Guid GameInfoId { get; set; }
        
        public bool Available { get; set; }
    }
}