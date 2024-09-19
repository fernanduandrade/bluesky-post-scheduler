namespace AtScheduler.Contracts.Dtos;

public sealed record PostRequestDto(
    int UserId, string Content, string Timer
    );