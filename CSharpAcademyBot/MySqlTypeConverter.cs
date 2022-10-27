using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAcademyBot
{
    public static class MySqlTypeConverter
    {
        private static Dictionary<Type, MySqlDbType> typeMap;

        // Create and populate the dictionary in the static constructor
        static MySqlTypeConverter()
        {
            typeMap = new Dictionary<Type, MySqlDbType>();

            typeMap[typeof(string)] = MySqlDbType.VarChar;
            typeMap[typeof(char[])] = MySqlDbType.VarChar;
            typeMap[typeof(byte)] = MySqlDbType.Byte;
            typeMap[typeof(short)] = MySqlDbType.Int16;
            typeMap[typeof(int)] = MySqlDbType.Int32;
            typeMap[typeof(long)] = MySqlDbType.Int64;
            typeMap[typeof(bool)] = MySqlDbType.Bit;
            typeMap[typeof(DateTime)] = MySqlDbType.DateTime;
            typeMap[typeof(DateOnly)] = MySqlDbType.Date;
            typeMap[typeof(double)] = MySqlDbType.Float;
            typeMap[typeof(TimeSpan)] = MySqlDbType.Time;
        }

        // Non-generic argument-based method
        public static MySqlDbType GetDbType(Type giveType)
        {
            // Allow nullable types to be handled
            giveType = Nullable.GetUnderlyingType(giveType) ?? giveType;

            if (typeMap.ContainsKey(giveType))
            {
                return typeMap[giveType];
            }

            throw new ArgumentException($"{giveType.FullName} is not a supported .NET class");
        }

        public static MySqlDbType GetDbType<T>()
        {
            return GetDbType(typeof(T));
        }
    }
}
