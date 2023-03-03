using Newtonsoft.Json;

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
    }
}
