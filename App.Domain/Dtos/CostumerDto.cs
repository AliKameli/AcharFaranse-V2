using System.ComponentModel.DataAnnotations;
using App.Domain.Common;

namespace App.Domain.Dtos;

public class CostumerDto : BaseUserDto
{
    [Display(Name = "مجموع پرداخت مشتری")]
    public decimal TotalMoneyPaid { get; set; } = 0;

    [Display(Name = "مجموع سود شرکت از مشتری")]
    public decimal TotalCompanyProfitEarnedFromCostumer { get; set; } = 0;

    [Display(Name = "میانگین امتیاز مشتری")]
    public byte RatingByWorkers { get; set; } = 0;

    [Display(Name = "تعداد امتیازات")]
    public int RatingCount { get; set; } = 0;
}