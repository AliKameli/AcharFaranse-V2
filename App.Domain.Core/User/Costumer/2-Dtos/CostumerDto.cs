using App.Domain.Core.BaseData._3_AbstractEntities;
using App.Domain.Core.User.Costumer._1_Entitys;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Core.User.Costumer._2_Dtos;

public class CostumerDto : BaseUserDto
{
    public int JobsRequestedCount { get; set; } = 0;
    public int JobsAcceptedByWorkersCount { get; set; } = 0;
    public int JobsFailedByWorkersCount { get; set; } = 0;
    public int JobsCanceledByCostumerCount { get; set; } = 0;
    public int JobsBeingDoneByWorkersCount { get; set; } = 0;
    public int JobsDoneSuccessfullyByWorkersCount { get; set; } = 0;

    [Precision(10, 1)]
    public decimal TotalWagePaid { get; set; } = 0;

    [Precision(10, 1)]
    public decimal TotalMaterialCostPaid { get; set; } = 0;

    [Precision(10, 1)]
    public decimal TotalMoneyPaid { get; set; } = 0;

    [Precision(10, 1)]
    public decimal TotalCompanyProfitEarnedFromCostumer { get; set; } = 0;

    [Range(0, 10)]
    public short RatingByWorkers { get; set; } = 0;

    public int RatingCount { get; set; } = 0;
}