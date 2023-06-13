using System.ComponentModel.DataAnnotations;

namespace IssueTrackerApi.Models;

public record IssueCreateRequest
{
    [Required, MaxLength(20)]
    public string From { get; set; } = string.Empty;
    [Required]
    public string Issue { get; set; } = string.Empty;
}

public record IssueCreatedResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public string From { get; set; } = string.Empty;
    public string Issue { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ClosedAt { get; set; }
}
