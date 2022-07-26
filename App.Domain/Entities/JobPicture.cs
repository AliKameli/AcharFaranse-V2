using System.ComponentModel.DataAnnotations;
using App.Domain.Common;
using App.Domain.Enums;

namespace App.Domain.Entities;

public class JobPicture : BaseEntity<int>
{
    [Display(Name = "آدرس ذخیره سازی")]
    public string FileSavePath { get; set; } = string.Empty;

    [Display(Name = "توضیحات")]
    public string? Description { get; set; }

    [Display(Name = "شناسه کار")]
    public int JobId { get; set; }

    [Display(Name = "کار")]
    public virtual Job? Job { get; set; }

    [Display(Name = "نوع کاربر فرستنده")]
    public UserTypeEnum UserType { get; set; }

    [Display(Name = "شناسه مشتری فرستنده")]
    public int? CostumerId { get; set; }

    [Display(Name = "مشتری فرستنده")]
    public virtual Costumer? Costumer { get; set; }

    [Display(Name = "شناسه کارمند فرستنده")]
    public int? WorkerId { get; set; }

    [Display(Name = "کارمند فرستنده")]
    public virtual Worker? Worker { get; set; }

    [Display(Name = "تایید شده")]
    public bool IsConfirmed { get; set; } = false;
}