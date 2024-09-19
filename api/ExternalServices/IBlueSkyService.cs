using AtScheduler.Contracts.Users;
using AtScheduler.ExternalServices.Dtos;

namespace AtScheduler.ExternalServices;

public interface IBlueSkyService
{
    Task<SessionResponseDto> Authenticate(AuthenticationResquestDto payload);
    Task SendPost(int postId);
}