using System.ComponentModel.DataAnnotations;
using App.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Dtos;

public class JobCategoryDto : BaseEntityDto<int>
{
    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [StringLength(50, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} کاراکتر باشد", MinimumLength = 2)]
    [Display(Name = "نام دسته‌بندی کار")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(256, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
    [Display(Name = "توضیحات")]
    public string? Description { get; set; }

    [MaxLength(256, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
    [DataType(DataType.ImageUrl)]
    [Display(Name = "آدرس تصویر ذخیره شده")]
    public string? PictureFilePath { get; set; }

    [RegularExpression(@"\d{0,9}(\.\d?)?", ErrorMessage = "{0} باید عدد با حداثر ۹ رقم و ۱ اعشار باشد")]
    [Precision(10, 1)]
    [Display(Name = "هزینه احتمالی")]
    public decimal? EstimatedWageCost { get; set; }

    public int? ParentJobCategoryId { get; set; }

    [Display(Name = "گروه")]
    public string GroupPath { get; set; } = string.Empty;
}