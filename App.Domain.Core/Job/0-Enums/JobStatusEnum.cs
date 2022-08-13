using System.ComponentModel.DataAnnotations;

namespace App.Domain.Core.Job._0_Enums;

public enum JobStatusEnum
{
    [Display(Name = "درخواست شده توسط مشتری")]
    RequestedByCostumer = 0,

    [Display(Name = "قبول شده توسط کارمند")]
    AcceptedByWorker = 1,

    [Display(Name = "شروع شده توسط کارمند")]
    StartedByWorker = 2,

    [Display(Name = "با موفقیت تمام شده و منتظر پرداخت")]
    CompletedByWorkerAndWaitingForPayment = 3,
    [Display(Name = "هزینه پرداخت شده")] PaymentCompleted = 4,
    [Display(Name = "انصراف توسط مشتری")] CanceledByCostumer = 5,
    [Display(Name = "انصراف توسط کارمند")] FailedByWorker = 6
}