namespace ChatworkApi;

using System.Text.Json;
using System.Text.Json.Serialization;

internal static class Constants
{
    public static readonly JsonSerializerOptions JsonSerializerOption
            = new()
              {
                  NumberHandling         = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
                , DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                , WriteIndented          = true
              };

}