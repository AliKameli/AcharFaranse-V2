using System.ComponentModel.DataAnnotations;
using App.Domain.Common;

namespace App.Domain.Entities;

public class Worker : BaseUser
{
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

    [Display(Name = "کارها")]
    public virtual ICollection<Job> WorkerJobs { get; set; } = new HashSet<Job>();

    [Display(Name = "تخصص‌ها")]
    public virtual ICollection<JobCategory> JobCategories { get; set; } = new HashSet<JobCategory>();

    public virtual ICollection<JobCategoryWorker> JobCategoryWorkers { get; set; } = new HashSet<JobCategoryWorker>();


    /// <summary>
    ///     پیشنهادات هزینه کار
    /// </summary>
    [Display(Name = "پیشنهادات هزینه کار")]
    public virtual ICollection<JobWorkerProposal> JobWorkerProposals { get; set; } = new HashSet<JobWorkerProposal>();

    /// <summary>
    ///     کارهای پیشنهاد داده
    /// </summary>
    [Display(Name = "کارهای پیشنهاد داده")]
    public virtual ICollection<Job> JobsProposed { get; set; } = new HashSet<Job>();

    [Display(Name = "گزارشات")]
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

    [Display(Name = "عکس‌های فرستاده")]
    public virtual ICollection<JobPicture> JobPictures { get; set; } = new HashSet<JobPicture>();
}