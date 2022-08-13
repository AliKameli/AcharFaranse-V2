using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.BaseData._3_AbstractEntities;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.User.Costumer._1_Entitys
{
    public class Costumer : BaseUser
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

        [InverseProperty(nameof(Costumer))]
        public virtual ICollection<Job._1_Entitys.Job> CostumerJobs { get; set; } = new HashSet<Job._1_Entitys.Job>();

        public virtual ICollection<CostumerAddress> CostumerAddresses { get; set; } = new HashSet<CostumerAddress>();
    }
}