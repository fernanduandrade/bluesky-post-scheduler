using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtScheduler.Contracts.Posts;

[Table("posts", Schema = "public")]
public class Post
{
    [Key]
    [Column("id")]
    [JsonPropertyName("Id")]
    public int Id { get; set; }
    
    [Column("content")]
    [JsonPropertyName("Content")]
    public string Content { get; set; }
    
    [Column("timer")]
    [JsonPropertyName("Timer")]
    public string Timer { get; set; }
    
    [Column("user_id")]
    [JsonPropertyName("UserId")]
    public int UserId { get; set; }
    
    [Column("created_at")]
    [JsonPropertyName("CreatedAt")]
    public DateTime CreatedAt { get; set; }
    public static Post Create(int userId, string content, string timer)
    {
        return new Post()
        {
            UserId = userId,
            Content = content,
            Timer = timer,
            CreatedAt = DateTime.Now
        };
    }
}