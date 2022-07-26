using System.ComponentModel.DataAnnotations;
using App.Domain.Common;

namespace App.Domain.Dtos;

public class CostumerAddressDto : BaseEntityDto<int>
{
    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [MaxLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    [Display(Name = "نام")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [StringLength(256, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} کاراکتر باشد", MinimumLength = 10)]
    [Display(Name = "آدرس کامل")]
    public string FullAddress { get; set; } = string.Empty;

    [MaxLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    [Display(Name = "مختصات روی نقشه")]
    public string? GpsCoordinates { get; set; }

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [StringLength(50, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} کاراکتر باشد", MinimumLength = 2)]
    [Display(Name = "نام پذیرنده")]
    public string ReceivingPersonFullName { get; set; } = string.Empty;

    [RegularExpression(@"(\+98)?\d+", ErrorMessage = "{0} باید عدد باشد")]
    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [StringLength(25, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} کاراکتر باشد", MinimumLength = 3)]
    [Display(Name = "شماره تلفن پذیرنده")]
    public string ReceivingPersonPhoneNumber { get; set; } = string.Empty;

    [Display(Name = "شهر")]
    public int CityId { get; set; }

    [Display(Name = "نام شهر")]
    public string CityName { get; set; } = string.Empty;

    [Display(Name = "شناسه مشتری")]
    public int CostumerId { get; set; }

    [Display(Name = "حذف شده")]
    public bool IsDeleted { get; set; } = false;
}