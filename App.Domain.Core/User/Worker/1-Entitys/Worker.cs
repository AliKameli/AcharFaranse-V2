using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.BaseData._3_AbstractEntities;
using App.Domain.Core.Job._1_Entitys;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.User.Worker._1_Entitys
{
    public class Worker : BaseUser
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

        public virtual ICollection<Job._1_Entitys.Job> WorkerJobs { get; set; } = new HashSet<Job._1_Entitys.Job>();

        public virtual ICollection<JobCategoryWorker> JobCategoryWorkers { get; set; } =
            new HashSet<JobCategoryWorker>();
    }
}