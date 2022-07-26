using System.ComponentModel.DataAnnotations;
using App.Domain.Common;
using App.Domain.Enums;

namespace App.Domain.Dtos;

public class CommentDto : BaseEntityDto<int>
{
    [Display(Name = "نوع کاربر فرستنده")]
    public UserTypeEnum UserType { get; set; }

    [Display(Name = "شناسه مشتری فرستنده")]
    public int? CostumerId { get; set; }

    [Display(Name = "شناسه کارمند فرستنده")]
    public int? WorkerId { get; set; }


    [Display(Name = "نام فرستنده")]
    public string? UserFullName { get; set; } = string.Empty;

    [Display(Name = "شناسه کار")]
    public int JobId { get; set; }

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [MaxLength(256, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
    [Display(Name = "متن گزارش")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "تایید شده")]
    public bool IsConfirmed { get; set; } = false;
}