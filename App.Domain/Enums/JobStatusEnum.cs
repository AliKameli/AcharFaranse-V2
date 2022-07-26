using System.ComponentModel.DataAnnotations;

namespace App.Domain.Enums;

public enum JobStatusEnum : byte
{
    [Display(Name = "درخواست شده توسط مشتری")]
    RequestedByCostumer = 1,

    [Display(Name = "کارمند مشخص شده")] WorkerChosenByCostumer = 2,

    [Display(Name = "شروع شده توسط کارمند")]
    StartedByWorker = 3,

    [Display(Name = "با موفقیت تمام شده و منتظر پرداخت")]
    CompletedByWorkerAndWaitingForPayment = 4,

    [Display(Name = "هزینه پرداخت شده")] PaymentCompleted = 5,

    [Display(Name = "انصراف توسط مشتری")] CanceledByCostumer = 6,

    [Display(Name = "انصراف توسط کارمند")] FailedByWorker = 7
}