using System.ComponentModel.DataAnnotations;

namespace App.Endpoint.MVC.Models;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "ایمیل")]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "رمز عبور")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "من را به خاطر بسپار")]
    public bool RememberMe { get; set; } = false;
}