namespace AtScheduler.Contracts.Dtos;

public record SchedulerResponseDto(
    int Id, string Content, string Timer, DateTime CreatedAt);