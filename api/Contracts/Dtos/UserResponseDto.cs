namespace AtScheduler.Contracts.Dtos;

public sealed record UserResponseDto(
    int Id, string Handler, DateTime CreatedAt);