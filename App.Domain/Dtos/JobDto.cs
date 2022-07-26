using System.ComponentModel.DataAnnotations;
using App.Domain.Common;
using App.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Dtos;

public class JobDto : BaseEntityDto<int>
{
    [MaxLength(256, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
    [Display(Name = "توضیحات")]
    public string? Description { get; set; }

    [MaxLength(256, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
    [Display(Name = "اطلاعات پرداخت آنلاین")]
    public string? OnlinePaymentReceiptInfo { get; set; }

    [Display(Name = "کار بسته شده")]
    public bool IsClosed { get; set; } = false;

    [Display(Name = "پرداخت آنلاین")]
    public bool IsOnlinePayment { get; set; } = false;

    [Display(Name = "عکس دارد")]
    public bool IsPictureAttached { get; set; } = false;

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [Display(Name = "زمان درخواست شده مشتری برای شروع انجام کار")]
    public DateTimeOffset JobStartTimeRequestedByUserDateTime { get; set; }

    [Display(Name = "زمان قبول کار توسط کارمند")]
    public DateTimeOffset? JobAcceptedByWorkerDateTime { get; set; }

    [Display(Name = "زمان شروع کار توسط کارمند")]
    public DateTimeOffset? JobStartedByWorkerDateTime { get; set; }

    [Display(Name = "تاریخ بسته شدن کار")]
    public DateTimeOffset? JobClosedDateTime { get; set; }

    [EnumDataType(typeof(JobStatusEnum))]
    [Display(Name = "وضعیت کار")]
    public JobStatusEnum JobStatus { get; set; } = JobStatusEnum.RequestedByCostumer;

    [Range(0, 10)]
    [Display(Name = "امتیاز مشتری به کارمند")]
    public byte? CostumerRatingForWorker { get; set; }

    [Range(0, 10)]
    [Display(Name = "امتیاز کارمند به مشتری")]
    public byte? WorkerRatingForCostumer { get; set; }

    [Precision(10, 1)]
    [Display(Name = "هزینه کلی پیشنهادی مشتری")]
    public decimal? CostumerEstimatedFinalCost { get; set; }

    [Precision(10, 1)]
    [Display(Name = "حق‌الزحمه کارمند")]
    public decimal WageCost { get; set; } = 0;

    [Precision(10, 1)]
    [Display(Name = "هزینه وسایل و قطعات مصرفی")]
    public decimal MaterialCost { get; set; } = 0;

    [Precision(10, 1)]
    [Display(Name = "سود شرکت")]
    public decimal CompanyProfit { get; set; } = 0;

    [Precision(10, 1)]
    [Display(Name = "هزینه نهایی")]
    public decimal FinalCost { get; set; } = 0;

    [Display(Name = "نام مشتری")]
    public string? CostumerName { get; set; }

    [Display(Name = "نام کارمند")]
    public string? WorkerName { get; set; }

    [Display(Name = "نام آدرس انجام کار")]
    public string? CostumerAddressName { get; set; }

    [Display(Name = "دسته‌بندی کار")]
    public string? JobCategoryName { get; set; }

    [Display(Name = "شهر")]
    public string? JobCityName { get; set; }

    [Display(Name = "شناسه مشتری")]
    public int CostumerId { get; set; }

    [Display(Name = "شناسه شهر انجام کار")]
    public int JobCityId { get; set; }

    [Display(Name = "شناسه دسته‌بندی کار")]
    public int JobCategoryId { get; set; }

    [Display(Name = "شناسه آدرس انجام کار")]
    public int CostumerAddressId { get; set; }

    [Display(Name = "شناسه کارمند")]
    public int? WorkerId { get; set; }

    [Display(Name = "شناسه گزارش مشتری")]
    public int? CostumerCommentId { get; set; }

    [Display(Name = "شناسه گزارش کارمند")]
    public int? WorkerCommentId { get; set; }
}