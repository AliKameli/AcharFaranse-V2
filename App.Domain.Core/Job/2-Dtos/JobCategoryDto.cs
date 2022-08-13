using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.BaseData._3_AbstractEntities;

namespace App.Domain.Core.Job._2_Dtos
{
    public class JobCategoryDto : BaseEntityDto<int>
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string? Description { get; set; }

        [MaxLength(256)]
        public string? PictureFilePath { get; set; }

        [Precision(10, 1)]
        public decimal? EstimatedWageCost { get; set; }

        public int? ParentJobCategoryId { get; set; }
    }
}