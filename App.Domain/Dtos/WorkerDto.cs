using System.ComponentModel.DataAnnotations;
using App.Domain.Common;

namespace App.Domain.Dtos;

public class WorkerDto : BaseUserDto
{
    [MaxLength(256, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
    [Display(Name = "توضیحات تکمیلی")]
    public string? Description { get; set; }

    [Display(Name = "مجموع درآمد خالص کارمند")]
    public decimal TotalWageEarned { get; set; } = 0;

    [Display(Name = "مجموع سود شرکت از کارمند")]
    public decimal TotalCompanyProfitEarnedFromWorker { get; set; } = 0;

    [Display(Name = "میزان بدهکاری به شرکت")]
    public decimal MoneyOwedToCompany { get; set; } = 0;

    [Display(Name = "میانگین امتیاز کارمند")]
    public byte RatingByCostumers { get; set; } = 0;

    [Display(Name = "تعداد امتیازات")]
    public int RatingCount { get; set; } = 0;
}