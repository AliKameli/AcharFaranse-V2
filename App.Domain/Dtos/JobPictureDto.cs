using System.ComponentModel.DataAnnotations;
using App.Domain.Common;
using App.Domain.Enums;
using App.Domain.Extensions;
using Microsoft.AspNetCore.Http;

namespace App.Domain.Dtos;

public class JobPictureDto : BaseEntityDto<int>
{
    [Display(Name = "آدرس ذخیره سازی")]
    public string FileSavePath { get; set; } = string.Empty;

    [MaxLength(256, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
    [Display(Name = "توضیحات")]
    public string? Description { get; set; }

    [Display(Name = "شناسه کار")]
    public int JobId { get; set; }

    [Display(Name = "نوع کاربر فرستنده")]
    public UserTypeEnum UserType { get; set; }

    [Display(Name = "شناسه مشتری فرستنده")]
    public int? CostumerId { get; set; }

    [Display(Name = "شناسه کارمند فرستنده")]
    public int? WorkerId { get; set; }

    [Display(Name = "نام فرستنده")]
    public string? UserFullName { get; set; } = string.Empty;

    [Display(Name = "تایید شده")]
    public bool IsConfirmed { get; set; } = false;

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [DataType(DataType.Upload)]
    [FileValidationForPicture(errorMessage:"فرمت قابل قبول نیست")]
    [Display(Name = "عکس انتخابی")]
    public IFormFile? PictureFile { get; set; } = null;
}