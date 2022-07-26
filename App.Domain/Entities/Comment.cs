using System.ComponentModel.DataAnnotations;
using App.Domain.Common;
using App.Domain.Enums;

namespace App.Domain.Entities;

public class Comment : BaseEntity<int>
{
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

    [Display(Name = "شناسه کار")]
    public int JobId { get; set; }

    [Display(Name = "کار")]
    public virtual Job? Job { get; set; }

    [Display(Name = "متن گزارش")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "تایید شده")]
    public bool IsConfirmed { get; set; } = false;
}