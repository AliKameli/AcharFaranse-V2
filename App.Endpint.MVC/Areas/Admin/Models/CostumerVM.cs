using System.ComponentModel.DataAnnotations;

namespace App.Endpoint.MVC.Areas.Admin.Models;

public class CostumerVM
{
    [Display(Name = "شناسه")]
    public int Id { get; init; }

    [Display(Name = "زمان ایجاد")]
    public DateTimeOffset CreationDateTime { get; init; }

    [Display(Name = "زمان آخرین بروزرسانی")]
    public DateTimeOffset? LastUpdateDateTime { get; init; }

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [EmailAddress]
    [Display(Name = "ایمیل")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [StringLength(100, ErrorMessage = "{0} حداقل {2} کاراکتر و حداکتر {1} باشد", MinimumLength = 3)]
    [DataType(DataType.Password)]
    [Display(Name = "رمز عبور")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [MaxLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    [Display(Name = "نام")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [MaxLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    [Display(Name = "نام خانوادگی")]
    public string LastName { get; set; } = string.Empty;

    [RegularExpression(@"(\+98)?\d+", ErrorMessage = "{0} باید عدد باشد")]
    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [MaxLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    [Display(Name = "شماره تلفن")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [RegularExpression("\\d+", ErrorMessage = "{0} باید عدد باشد")]
    [StringLength(10, ErrorMessage = "{0} باید {1} کاراکتر باشد")]
    [Display(Name = "شماره ملّی")]
    public string NationalSecurityId { get; set; } = string.Empty;

    [MaxLength(256, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    [Display(Name = "آدرس محل سکونت")]
    public string? HomeAddress { get; set; }

    [Display(Name = "تایید شده")]
    public bool IsConfirmed { get; set; } = false;

    [Display(Name = "زمان تایید")]
    public DateTimeOffset? ConfirmDateTime { get; set; }

    [Display(Name = "شهر محل سکونت")]
    public string? UserCityName { get; set; }

    [Display(Name = "شناسه شهر محل سکونت")]
    public int? UserCityId { get; set; }
}