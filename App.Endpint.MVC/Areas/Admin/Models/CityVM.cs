using System.ComponentModel.DataAnnotations;

namespace App.Endpoint.MVC.Areas.Admin.Models;

public class CityVM
{
    [Display(Name = "شناسه")]
    public virtual int Id { get; init; }

    [Display(Name = "زمان ایجاد")]
    public DateTimeOffset CreationDateTime { get; init; }

    [Display(Name = "زمان آخرین بروزرسانی")]
    public virtual DateTimeOffset? LastUpdateDateTime { get; init; }

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    [Display(Name = "نام استان")]
    public string StateName { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [StringLength(50, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    [Display(Name = "نام شهر")]
    public string CityName { get; set; } = string.Empty;
}