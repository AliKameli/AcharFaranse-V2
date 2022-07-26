using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Common;
using App.Domain.Enums;

namespace App.Domain.Entities;

public class Job : BaseEntity<int>
{
    [Display(Name = "توضیحات")]
    public string? Description { get; set; }

    [Display(Name = "وضعیت کار")]
    public JobStatusEnum JobStatus { get; set; } = JobStatusEnum.RequestedByCostumer;

    [Display(Name = "اطلاعات پرداخت آنلاین")]
    public string? OnlinePaymentReceiptInfo { get; set; }

    [Display(Name = "پرداخت آنلاین")]
    public bool IsOnlinePayment { get; set; } = false;

    [Display(Name = "بسته شده")]
    public bool IsClosed { get; set; } = false;

    [Display(Name = "عکس دارد")]
    public bool IsPictureAttached { get; set; } = false;

    [Display(Name = "زمان درخواست شده مشتری برای شروع انجام کار")]
    public DateTimeOffset JobStartTimeRequestedByUserDateTime { get; set; }

    [Display(Name = "زمان قبول کار توسط کارمند")]
    public DateTimeOffset? JobAcceptedByWorkerDateTime { get; set; }

    [Display(Name = "زمان شروع کار توسط کارمند")]
    public DateTimeOffset? JobStartedByWorkerDateTime { get; set; }


    [Display(Name = "تاریخ بسته شدن کار")]
    public DateTimeOffset? JobClosedDateTime { get; set; }


    [Display(Name = "امتیاز مشتری به کارمند")]
    public byte? CostumerRatingForWorker { get; set; }

    [Display(Name = "امتیاز کارمند به مشتری")]
    public byte? WorkerRatingForCostumer { get; set; }

    [Display(Name = "هزینه کلی پیشنهادی مشتری")]
    public decimal? CostumerEstimatedFinalCost { get; set; }

    [Display(Name = "حق‌الزحمه کارمند")]
    public decimal WageCost { get; set; } = 0;

    [Display(Name = "هزینه وسایل و قطعات مصرفی")]
    public decimal MaterialCost { get; set; } = 0;

    [Display(Name = "سود شرکت")]
    public decimal CompanyProfit { get; set; } = 0;

    [Display(Name = "هزینه نهایی")]
    public decimal FinalCost { get; set; } = 0;

    [ForeignKey(nameof(Costumer))]
    [Display(Name = "شناسه مشتری")]
    public int CostumerId { get; set; }

    public virtual Costumer? Costumer { get; set; }

    [ForeignKey(nameof(JobCity))]
    [Display(Name = "شناسه شهر انجام کار")]
    public int JobCityId { get; set; }

    public virtual City? JobCity { get; set; }

    [ForeignKey(nameof(JobCategory))]
    [Display(Name = "شناسه دسته‌بندی کار")]
    public int JobCategoryId { get; set; }

    public virtual JobCategory? JobCategory { get; set; }

    [ForeignKey(nameof(CostumerAddress))]
    [Display(Name = "شناسه آدرس انجام کار")]
    public int CostumerAddressId { get; set; }

    public virtual CostumerAddress? CostumerAddress { get; set; }

    [ForeignKey(nameof(Worker))]
    [Display(Name = "شناسه کارمند")]
    public int? WorkerId { get; set; }

    public virtual Worker? Worker { get; set; }

    [Display(Name = "گزارشات")]
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

    [Display(Name = "تصاویر")]
    public virtual ICollection<JobPicture> JobPictures { get; set; } = new HashSet<JobPicture>();

    [Display(Name = "پیشنهادات هزینه توسط کارمندان")]
    public virtual ICollection<JobWorkerProposal> JobWorkerProposals { get; set; } = new HashSet<JobWorkerProposal>();

    [Display(Name = "کارمندان پیشنهاد داده")]
    public virtual ICollection<Worker> ProposedWorkers { get; set; } = new HashSet<Worker>();
}