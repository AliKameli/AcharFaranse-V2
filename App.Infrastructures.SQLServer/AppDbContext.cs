using App.Domain.Entities;
using App.Infrastructures.SQLServer.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.SQLServer;

public class AppDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; } = null!;
    public virtual DbSet<Comment> Comments { get; set; } = null!;
    public virtual DbSet<Costumer> Costumers { get; set; } = null!;
    public virtual DbSet<CostumerAddress> CostumerAddresses { get; set; } = null!;
    public virtual DbSet<Job> Jobs { get; set; } = null!;
    public virtual DbSet<JobCategory> JobCategories { get; set; } = null!;
    public virtual DbSet<JobCategoryWorker> JobCategoryWorkers { get; set; } = null!;
    public virtual DbSet<JobPicture> JobPictures { get; set; } = null!;
    public virtual DbSet<JobWorkerProposal> JobWorkerProposals { get; set; } = null!;
    public virtual DbSet<Worker> Workers { get; set; } = null!;

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    base.OnConfiguring(optionsBuilder);

    //    optionsBuilder.UseSqlServer(
    //        "Data Source=.\\SQL2019; Initial Catalog=AcharFaranseDb; Integrated Security=TRUE");
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CityConfig());
        modelBuilder.ApplyConfiguration(new CommentConfig());
        modelBuilder.ApplyConfiguration(new CostumerConfig());
        modelBuilder.ApplyConfiguration(new CostumerAddressConfig());
        modelBuilder.ApplyConfiguration(new JobConfig());
        modelBuilder.ApplyConfiguration(new JobCategoryConfig());
        modelBuilder.ApplyConfiguration(new JobCategoryWorkerConfig());
        modelBuilder.ApplyConfiguration(new JobPictureConfig());
        modelBuilder.ApplyConfiguration(new JobWorkerProposalConfig());
        modelBuilder.ApplyConfiguration(new WorkerConfig());
    }
}