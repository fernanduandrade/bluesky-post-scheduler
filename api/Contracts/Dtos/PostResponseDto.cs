namespace AtScheduler.Contracts.Dtos;

public record PostResponseDto(
    int Id, string Content, string Timer, DateTime CreatedAt);