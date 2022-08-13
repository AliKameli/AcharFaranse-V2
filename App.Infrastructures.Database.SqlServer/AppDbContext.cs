using System.Linq.Expressions;
using App.Domain.Core.BaseData._1_Entities;
using App.Domain.Core.Job._1_Entitys;
using App.Domain.Core.User.AppIdentity._1_Entitys;
using App.Domain.Core.User.Costumer._1_Entitys;
using App.Domain.Core.User.Worker._1_Entitys;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Database.SqlServer;

public class AppDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, int>
{
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Job> Jobs { get; set; }
    public virtual DbSet<JobCategory> JobCategories { get; set; }
    public virtual DbSet<JobCategoryWorker> JobCategoryWorkers { get; set; }
    public virtual DbSet<CostumerAddress> CostumerAddresses { get; set; }
    public virtual DbSet<Costumer> Costumers { get; set; }
    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=.\SQL2019; Initial Catalog=AcharDb; Integrated Security=TRUE");
    }
}