using System.ComponentModel.DataAnnotations;
using App.Domain.Common;

namespace App.Domain.Entities;

public class Costumer : BaseUser
{
    [Display(Name = "مجموع پرداخت مشتری")]
    public decimal TotalMoneyPaid { get; set; } = 0;


    [Display(Name = "مجموع سود شرکت از مشتری")]
    public decimal TotalCompanyProfitEarnedFromCostumer { get; set; } = 0;


    [Display(Name = "میانگین امتیاز مشتری")]
    public byte RatingByWorkers { get; set; } = 0;

    [Display(Name = "تعداد امتیازات")]
    public int RatingCount { get; set; } = 0;

    public virtual ICollection<Job> CostumerJobs { get; set; } = new HashSet<Job>();

    public virtual ICollection<CostumerAddress> CostumerAddresses { get; set; } = new HashSet<CostumerAddress>();
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

    [Display(Name = "عکس‌های فرستاده")]
    public virtual ICollection<JobPicture> JobPictures { get; set; } = new HashSet<JobPicture>();
}