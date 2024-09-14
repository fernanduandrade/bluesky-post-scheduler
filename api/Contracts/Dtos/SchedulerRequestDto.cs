namespace AtScheduler.Contracts.Dtos;

public sealed record SchedulerRequestDto(
    int UserId, string Content, string Timer
    );