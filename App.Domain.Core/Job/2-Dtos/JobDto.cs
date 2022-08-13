using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.BaseData._3_AbstractEntities;
using App.Domain.Core.Job._0_Enums;
using App.Domain.Core.User.Costumer._1_Entitys;
using App.Domain.Core.User.Worker._1_Entitys;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.Job._2_Dtos
{
    public class JobDto : BaseEntityDto<int>
    {
        [MaxLength(256)]
        public string? Description { get; set; }

        [MaxLength(256)]
        public string? CostumerEndNote { get; set; }

        [MaxLength(256)]
        public string? WorkerEndNote { get; set; }

        [MaxLength(256)]
        public string? OnlinePaymentReceiptInfo { get; set; }

        public bool IsClosed { get; set; } = false;
        public bool IsOnlinePayment { get; set; } = false;

        public DateTimeOffset? JobAcceptedByWorkerDateTime { get; set; }

        public DateTimeOffset? JobStartedByWorkerDateTime { get; set; }

        public DateTimeOffset? JobClosedDateTime { get; set; }

        public JobStatusEnum JobStatus { get; set; } = 0;

        [Range(0, 10)]
        public short? CostumerRatingForWorker { get; set; }

        [Range(0, 10)]
        public short? WorkerRatingForCostumer { get; set; }

        [Precision(10, 1)]
        public decimal? CostumerEstimatedFinalCost { get; set; }

        [Precision(10, 1)]
        public decimal WageCost { get; set; } = 0;

        [Precision(10, 1)]
        public decimal MaterialCost { get; set; } = 0;

        [Precision(10, 1)]
        public decimal FinalCost { get; set; } = 0;

        public int CostumerId { get; set; }

        public int? CostumerAddressId { get; set; }

        public int? WorkerId { get; set; }
    }
}