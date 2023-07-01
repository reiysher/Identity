using System.ComponentModel.DataAnnotations;

namespace Identity.Infrastructure.Persistence.Settings;

internal class DatabaseSettings
{
    public const string SectionName = "DatabaseSettings";

    [Required]
    public string? ConnectionString { get; set; }

    [Required]
    public int? CommandTimeoutInSeconds { get; set; }
}
