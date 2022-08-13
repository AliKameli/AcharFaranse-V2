using App.Domain.Core.BaseData._3_AbstractEntities;

namespace App.Domain.Core.Job._2_Dtos;

public class JobCategoryWorkerDto : BaseEntityDto<int>
{
    public override DateTimeOffset? LastUpdateDateTime => IsDeleted ? DeleteDateTime : CreationDateTime;
    public int JobCategoryId { get; set; }
    public int WorkerId { get; set; }
}