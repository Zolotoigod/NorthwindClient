using System.Text.Json;

namespace MVCNorthwindClient
{
    public static class Defines
    {
        public static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
