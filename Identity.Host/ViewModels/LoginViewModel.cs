using System.ComponentModel.DataAnnotations;

namespace Identity.Host.ViewModels;

public record LoginViewModel(
    [Required] string Username,
    [Required] string Password,
    bool RememberMe = false,
    string? ReturnUrl = null);
