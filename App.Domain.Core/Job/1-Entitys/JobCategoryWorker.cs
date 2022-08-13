using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.BaseData._3_AbstractEntities;
using App.Domain.Core.User.Worker._1_Entitys;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.Job._1_Entitys
{
    public class JobCategoryWorker : BaseEntity<int>
    {
        [NotMapped]
        public override DateTimeOffset? LastUpdateDateTime => IsDeleted ? DeleteDateTime : CreationDateTime;

        [ForeignKey(nameof(JobCategory))]
        public int JobCategoryId { get; set; }

        public virtual JobCategory? JobCategory { get; set; }

        [ForeignKey(nameof(Worker))]
        public int WorkerId { get; set; }

        public virtual Worker? Worker { get; set; }
    }
}