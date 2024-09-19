using System.Text.Json.Serialization;

namespace AtScheduler.ExternalServices.Dtos;

public sealed record SessionResponseDto(
    [property: JsonPropertyName("handler")] string Hander,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("did")] string Did,
    [property: JsonPropertyName("accessJwt")] string accessJwt
    );