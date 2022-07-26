namespace App.Domain.Entities;

public class JobCategoryWorker
{
    public int JobCategoryId { get; set; }

    public virtual JobCategory? JobCategory { get; set; }

    public int WorkerId { get; set; }

    public virtual Worker? Worker { get; set; }
}