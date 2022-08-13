using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.BaseData._1_Entities;
using App.Domain.Core.BaseData._3_AbstractEntities;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.Job._1_Entitys
{
    [Index(nameof(Name), IsUnique = true)]
    public class JobCategory : BaseEntity<int>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string? Description { get; set; }

        [MaxLength(256)]
        public string? PictureFilePath { get; set; }

        [Precision(10, 1)]
        public decimal? EstimatedWageCost { get; set; }

        [ForeignKey(nameof(ParentJobCategory))]
        public int? ParentJobCategoryId { get; set; }

        public virtual JobCategory? ParentJobCategory { get; set; }

        [InverseProperty(nameof(ParentJobCategory))]
        public virtual ICollection<JobCategory> ChildrenJobCategories { get; set; } = new HashSet<JobCategory>();

        public virtual ICollection<JobCategoryWorker> JobCategoryWorkers { get; set; }

        public virtual ICollection<Job> Jobs { get; set; } = new HashSet<Job>();
    }
}