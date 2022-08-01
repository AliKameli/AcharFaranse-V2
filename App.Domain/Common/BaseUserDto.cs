using System.ComponentModel.DataAnnotations;
using App.Domain.Extensions;
using Microsoft.AspNetCore.Http;

namespace App.Domain.Common;

public abstract class BaseUserDto : BaseEntityDto<int>
{
    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [EmailAddress]
    [Display(Name = "ایمیل")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [StringLength(100, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} کاراکتر باشد", MinimumLength = 3)]
    [DataType(DataType.Password)]
    [Display(Name = "رمز عبور")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "رمز عبور و تکرار رمز عبور باید یکسان باشند")]
    [Display(Name = "تکرار رمز عبور")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [MaxLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    [Display(Name = "نام")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [MaxLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    [Display(Name = "نام خانوادگی")]
    public string LastName { get; set; } = string.Empty;

    [RegularExpression(@"(\+98)?\d+", ErrorMessage = "{0} باید عدد باشد")]
    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [StringLength(25, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} کاراکتر باشد", MinimumLength = 3)]
    [Display(Name = "شماره تلفن")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [RegularExpression("\\d+", ErrorMessage = "{0} باید عدد باشد")]
    [StringLength(10, ErrorMessage = "{0} باید {1} کاراکتر باشد")]
    [Display(Name = "شماره ملّی")]
    public string NationalId { get; set; } = string.Empty;

    [MaxLength(256, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    [Display(Name = "آدرس محل سکونت")]
    public string? HomeAddress { get; set; }

    [MaxLength(256, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
    [DataType(DataType.ImageUrl)]
    [Display(Name = "آدرس تصویر ذخیره شده")]
    public string? PictureFilePath { get; set; }

    [DataType(DataType.Upload)]
    [FileValidationForPicture("فرمت قابل قبول نیست")]
    [Display(Name = "تصویر")]
    public IFormFile? PictureFile { get; set; }

    [Display(Name = "تایید شده")]
    public bool IsConfirmed { get; set; } = false;

    [Display(Name = "زمان تایید")]
    public DateTimeOffset? ConfirmDateTime { get; set; }

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [Display(Name = "شناسه شهر محل سکونت")]
    public int CityId { get; set; }

    [Display(Name = "شهر محل سکونت")]
    public string CityName { get; set; } = string.Empty;
}