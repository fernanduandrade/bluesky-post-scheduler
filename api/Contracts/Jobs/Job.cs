using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtScheduler.Contracts.Jobs;

[Table("recurring_jobs", Schema = "public")]
public class Job
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("identifier")]
    public string JobId { get; set; }
    
    [Column("scheduler_id")]
    public int SchedulerId { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    public static Job Create(string jobId, int schedulerId)
    {
        return new Job()
        {
            JobId = jobId,
            SchedulerId = schedulerId
        };
    }
}