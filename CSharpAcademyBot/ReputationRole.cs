using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAcademyBot
{
    public class ReputationRole
    {
        public int id = 0;
        public long discordId = 0;
        public string name = string.Empty;
        public int requirement = 0;

        public ReputationRole() { }
        public ReputationRole(MySqlDataReader reader)
        {
            id = reader.GetInt32("id");
            discordId = reader.GetInt64("discord_id");
            name = reader.GetString("name");
            requirement = reader.GetInt32("requirement");
        }
    }
}
