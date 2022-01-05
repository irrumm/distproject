using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class Message
    {
        public IList<string> Messages { get; set; } = new List<string>();
        
        public Message()
        {
            
        }

        public Message(params string[] messages)
        {
            Messages = messages;
        }
        
    }
}