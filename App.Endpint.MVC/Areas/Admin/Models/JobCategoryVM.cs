using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace App.Endpoint.MVC.Areas.Admin.Models;

public class JobCategoryVM
{
    [Display(Name = "شناسه")]
    public virtual int Id { get; init; }

    [Display(Name = "زمان ایجاد")]
    public DateTimeOffset CreationDateTime { get; init; }

    [Display(Name = "زمان آخرین بروزرسانی")]
    public virtual DateTimeOffset? LastUpdateDateTime { get; init; }

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [StringLength(100, ErrorMessage = "{0} باید حداقل {2} کاراکتر و حداکتر {1} باشد", MinimumLength = 3)]
    [Display(Name = "نام دسته‌بندی کار")]
    public string Name { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    [Display(Name = "توضیحات")]
    public string? Description { get; set; }

    [StringLength(256, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    [Display(Name = "آدرس تصویر ذخیره شده")]
    public string? PictureFilePath { get; set; }

    [Precision(10, 1)]
    [RegularExpression("\\d+", ErrorMessage = "{0} باید عدد باشد")]
    [Display(Name = "هزینه احتمالی")]
    public decimal? EstimatedWageCost { get; set; }

    [Display(Name = "شناسه دسته‌بندی بالایی")]
    public int? ParentJobCategoryId { get; set; }
}