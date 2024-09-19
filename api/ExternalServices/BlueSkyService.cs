using System.Net.Http.Headers;
using AtScheduler.Contracts.Posts;
using AtScheduler.Contracts.Users;
using AtScheduler.ExternalServices.Dtos;
using AtScheduler.Services;
using Newtonsoft.Json;

namespace AtScheduler.ExternalServices;

public class BlueSkyService : IBlueSkyService
{
    private readonly HttpClient _client;
    private const string BaseUrl = "https://bsky.social/xrpc/";
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;
    
    public BlueSkyService(HttpClient client, IPostRepository postRepository, IUserRepository userRepository)
    {
        _client = client;
        _client.BaseAddress = new Uri(BaseUrl);
        _postRepository = postRepository;
        _userRepository = userRepository;
    }
    public async Task<SessionResponseDto> Authenticate(AuthenticationResquestDto payload)
    {
        var response = await _client.PostAsJsonAsync("com.atproto.server.createSession", payload);
        if (!response.IsSuccessStatusCode)
            return null;
        
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<SessionResponseDto>(responseString);
    }

    public async Task SendPost(int postId)
    {
        var post = await _postRepository.GetByIdAsync(postId);
        var user = await _userRepository.GetByIdAsync(post.UserId);
        var token = await Authenticate(new AuthenticationResquestDto(user.Handler, user.Password));
        
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.accessJwt);
        var payload = SendPostRequestDto.Create(user.Handler, post.Content);
        var response = await _client.PostAsJsonAsync("com.atproto.repo.createRecord", payload);
        response.EnsureSuccessStatusCode();
        
    }
}