using App.AppServices;
using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Repo;
using App.Infrastructures.Repo;
using App.Infrastructures.SQLServer;
using Microsoft.AspNetCore.Identity;

namespace App.Endpoint.MVC;

public static class MyDependencyInjections
{
    public static IServiceCollection AddServicesCollection(
        this IServiceCollection services)
    {
        services.AddScoped<ICityRepo, CityRepo>();
        services.AddScoped<ICommentRepo, CommentRepo>();
        services.AddScoped<ICostumerRepo, CostumerRepo>();
        services.AddScoped<ICostumerAddressRepo, CostumerAddressRepo>();
        services.AddScoped<IJobRepo, JobRepo>();
        services.AddScoped<IJobCategoryRepo, JobCategoryRepo>();
        services.AddScoped<IJobPictureRepo, JobPictureRepo>();
        services.AddScoped<IJobWorkerProposalRepo, JobWorkerProposalRepo>();
        services.AddScoped<IWorkerRepo, WorkerRepo>();

        return services;
    }

    public static IServiceCollection AddAppServicesCollection(
        this IServiceCollection services)
    {
        services.AddScoped<ICityAppService, CityAppService>();
        services.AddScoped<ICommentAppService, CommentAppService>();
        services.AddScoped<ICostumerAppService, CostumerAppService>();
        services.AddScoped<ICostumerAddressAppService, CostumerAddressAppService>();
        services.AddScoped<IJobAppService, JobAppService>();
        services.AddScoped<IJobCategoryAppService, JobCategoryAppService>();
        services.AddScoped<IJobPictureAppService, JobPictureAppService>();
        services.AddScoped<IJobWorkerProposalAppService, JobWorkerProposalAppService>();
        services.AddScoped<IWorkerAppService, WorkerAppService>();

        return services;
    }

    public static IServiceCollection AddMyIdentity(
        this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(
                options =>
                {
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequiredLength = 2;
                })
            .AddEntityFrameworkStores<AppDbContext>();

        return services;
    }
}