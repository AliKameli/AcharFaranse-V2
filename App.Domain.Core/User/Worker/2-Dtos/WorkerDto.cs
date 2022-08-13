using App.Domain.Core.BaseData._3_AbstractEntities;
using App.Domain.Core.Job._1_Entitys;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Core.User.Worker._2_Dtos;

public class WorkerDto : BaseUserDto
{
    [MaxLength(256)]
    public string? Description { get; set; }

    public int JobsProposedCount { get; set; } = 0;
    public int JobsAcceptedCount { get; set; } = 0;
    public int JobsFailedByWorkerCount { get; set; } = 0;
    public int JobsCanceledByCostumersCount { get; set; } = 0;
    public int JobsDoingCount { get; set; } = 0;
    public int JobsDoneSuccessfullyCount { get; set; } = 0;

    [Precision(10, 1)]
    public decimal TotalWageEarned { get; set; } = 0;

    [Precision(10, 1)]
    public decimal TotalMaterialCostEarned { get; set; } = 0;

    [Precision(10, 1)]
    public decimal TotalMoneyEarned { get; set; } = 0;

    [Precision(10, 1)]
    public decimal TotalCompanyProfitEarnedFromWorker { get; set; } = 0;

    [Precision(10, 1)]
    public decimal MoneyOwedToCompany { get; set; } = 0;

    [Range(0, 100)]
    public short CompanySharePercentage { get; set; } = 0;

    [Range(0, 10)]
    public short RatingByCostumers { get; set; } = 0;

    public int RatingCount { get; set; } = 0;
}