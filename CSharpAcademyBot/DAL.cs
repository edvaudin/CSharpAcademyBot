using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CSharpAcademyBot
{
    public class DAL : IDisposable
    {
        protected MySqlConnection conn = null;

        public DAL()
        {
            conn = new MySqlConnection();
            conn.ConnectionString = Bot.Config.Database;
            TryOpenConnection(conn);
        }

        private static void TryOpenConnection(MySqlConnection conn)
        {
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool UserExists(long discordId)
        {
            string sql = "select * from users where discord_user_id = @discordId";
            var cmd = new MySqlCommand(sql, conn);
            AddParameter("@discordId", discordId, cmd);

            using (var reader = cmd.ExecuteReader())
            {
                return reader.HasRows;
            }
        }

        public void AddUser(string name, long discordId)
        {
            string sql = "INSERT INTO users (name, discord_user_id) VALUES (@name, @discordUserId)";
            var cmd = new MySqlCommand(sql, conn);
            AddParameter("@name", name, cmd);
            AddParameter("@discordUserId", discordId, cmd);
            cmd.ExecuteNonQuery();
        }

        public void AddUserRepRecord(long discordId)
        {
            string sql = "INSERT INTO user_reputations (users_id, reputation) VALUES ((SELECT users.id FROM users WHERE discord_user_id = @discordUserId), 0);";
            var cmd = new MySqlCommand(sql, conn);
            AddParameter("@discordUserId", discordId, cmd);
            cmd.ExecuteNonQuery();
        }

        public void AddRepForUser(long discordId, int repChange)
        {
            string sql = "UPDATE user_reputations SET reputation = (reputation + @repChange) WHERE users_id = (SELECT id FROM users WHERE discord_user_id = @discordUserId);";
            var cmd = new MySqlCommand(sql, conn);
            AddParameter("@repChange", repChange, cmd);
            AddParameter("@discordUserId", discordId, cmd);
            cmd.ExecuteNonQuery();
        }

        public List<ReputationRole> GetRoles()
        {
            string sql = "SELECT * FROM roles";
            var cmd = new MySqlCommand(sql, conn);
            return GetQueriedList(cmd, reader => new ReputationRole(reader));
        }

        public int GetUserReputation(long discordId)
        {
            string sql = "SELECT reputation FROM user_reputations WHERE users_id = (SELECT id FROM users WHERE discord_user_id = @discordUserId);";
            var cmd = new MySqlCommand(sql, conn);
            AddParameter("@discordUserId", discordId, cmd);
            int reputation = 0;
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    reputation = reader.GetInt32("reputation");
                }
            }
            return reputation;
        }

        protected static List<T> GetQueriedList<T>(MySqlCommand cmd, Func<MySqlDataReader, T> creator)
        {
            List<T> results = new List<T>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    try
                    {
                        results.Add(creator(reader));
                    }
                    catch (Exception ex) { Console.WriteLine(ex); }
                }
            }
            return results;
        }

        protected static void AddParameter<T>(string name, T value, MySqlCommand cmd)
        {
            MySqlParameter param = new MySqlParameter(name, MySqlTypeConverter.GetDbType(value.GetType()));
            param.Value = value;
            cmd.Parameters.Add(param);
        }

        public void Dispose()
        {
            if (conn != null) { conn.Close(); }
        }
    }
}
