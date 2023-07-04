using System.ComponentModel.DataAnnotations;

namespace Identity.Host.ViewModels;

public record AuthorizeViewModel(
    [Display(Name = "Application")]  string? ApplicationName,
    [Display(Name = "Scope")] string? Scope);
