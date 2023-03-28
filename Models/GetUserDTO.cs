using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAcademyBot.Models
{
    public class GetUserDTO
    {
        public long DiscordId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}
