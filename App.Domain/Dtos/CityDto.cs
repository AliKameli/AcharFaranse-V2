using System.ComponentModel.DataAnnotations;
using App.Domain.Common;

namespace App.Domain.Dtos;

public class CityDto : BaseEntityDto<int>
{
    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
    [Display(Name = "نام شهر")]
    public string Name { get; set; } = string.Empty;
}