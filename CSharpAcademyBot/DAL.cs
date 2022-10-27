using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
