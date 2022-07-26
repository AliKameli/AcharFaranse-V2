using System.ComponentModel.DataAnnotations;
using App.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Common;

public abstract class BaseUser : BaseEntity<int>
{
    [Display(Name = "نام")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "نام خانوادگی")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "شماره ملّی")]
    public string NationalId { get; set; } = string.Empty;

    [Display(Name = "آدرس محل سکونت")]
    public string? HomeAddress { get; set; }

    [Display(Name = "آدرس تصویر ذخیره شده")]
    public string? PictureFilePath { get; set; }

    [Display(Name = "تایید شده")]
    public bool IsConfirmed { get; set; } = false;

    [Display(Name = "زمان تایید")]
    public DateTimeOffset? ConfirmDateTime { get; set; }

    [Display(Name = "شناسه شهر محل سکونت")]
    public int UserCityId { get; set; }

    [Display(Name = "شهر محل سکونت")]
    public virtual City? UserCity { get; set; }

    public IdentityUser<int>? IdentityUser { get; set; }

    public override string ToString()
    {
        return (FirstName+" "+LastName);
    }
}