using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAcademyBot
{
    public struct ConfigJson
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
        [JsonProperty("prefix")]
        public string Prefix { get; private set; }
        [JsonProperty("database")]
        public string Database { get; private set; }
        [JsonProperty("test_server")]
        public ulong TestServer { get; private set; }
    }
}
