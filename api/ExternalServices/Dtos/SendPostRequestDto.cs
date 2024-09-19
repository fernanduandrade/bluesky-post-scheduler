using System.Text.Json.Serialization;
using AtScheduler.Contracts.Users;

namespace AtScheduler.ExternalServices.Dtos;


public sealed record SendPostRequestDto
{
    [JsonPropertyName("repo")]
    public string Repo { get; init; }
    [JsonPropertyName("collection")]
    public string Collection { get; init; }
    [JsonPropertyName("record")]
    public RecordDto Record { get; init; }

    public static SendPostRequestDto Create(string userHandler, string content)
    {
        return new SendPostRequestDto()
        {
            Repo = userHandler,
            Collection = "app.bsky.feed.post",
            Record = new RecordDto(content, DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffK"))
        };
    }
}


public sealed record RecordDto(
    [property: JsonPropertyName("text")] string Text,
    [property: JsonPropertyName("createdAt")] string CreatedAt
    );