using AtScheduler.Common.Interfaces;
using AtScheduler.Contracts.Dtos;
using AtScheduler.Contracts.Users;
using AtScheduler.ExternalServices;
using AtScheduler.ExternalServices.Dtos;
using AtScheduler.Shared;

namespace AtScheduler.Services;

public interface IUserService
{
    Task<Result<UserResponseDto, Error>> Create(UserRequestDto user);
    Task<User> GetByIdAsync(int userId);
}

public class UserService(IBlueSkyService blueSkyService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : IUserService
{
    public async Task<Result<UserResponseDto, Error>> Create(UserRequestDto user)
    {
        var bsResponse = await blueSkyService.Authenticate(new AuthenticationResquestDto(user.Handler, user.Password));
        if(bsResponse is null)
            return new Error("User.AuthenticationFailed", "Failed to authenticate");

        var newUser = User.Create(user.Handler, user.Password, bsResponse.Did);
        userRepository.Add(newUser);

        await unitOfWork.CommitAsync();

        return new UserResponseDto(newUser.Id, newUser.Handler, newUser.InsertedAt);
    }
    
    public async Task<User> GetByIdAsync(int userId)
     => await userRepository.GetByIdAsync(userId);
}